using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TodoAppAPI.Extension;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using Serilog;
using MediatR;
using TodoAppAPI.Application.Behaviors;
using System;
using System.IO;
using TodoAppAPI.Authentication.Helpers;
using Infrastracture.DateTimeProvider;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;
using TodoAppAPI.Versioning;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using TodoAppAPI.Application.Commands.Idempotency;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using MassTransit;

namespace TodoAppAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;  
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCors();
            services.ConfigureIISIntegration();
            services.ConfigureMysqlContext(Configuration);
            services.ConfigureRepositoryServices();

            #region Authentication
            string keycloakPublicKey = File.ReadAllText(AppContext.BaseDirectory + (this.Configuration.GetValue<string>("PUBLIC-KEY-PATH") ?? @"/Authentication/Docs/Keycloak/public-keycloak.xml"));

            var issuerLink = Environment.GetEnvironmentVariable("JWT_ISSUER_KEYCLOAK");
            string clientName = Environment.GetEnvironmentVariable("CLIENT_NAME");

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("Authenticated", policy => policy.RequireAuthenticatedUser());
            });

            services.AddChassis(Configuration, config =>
            {
                config.AddAuth(new JWTAuthentication(), keycloakPublicKey, issuerLink, clientName);
            });
            #endregion

            #region Swagger
            services.AddApiVersioning(
                options =>
                {
                    options.ReportApiVersions = true;
                });

            services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(
                options =>
                {
                    options.OperationFilter<SwaggerDefaultValues>();
                    // Set the comments path for the Swagger JSON and UI.
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    options.IncludeXmlComments(xmlPath);

                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "Please enter your Bearer Token.",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer"
                    });

                    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                            },
                            new string[]{ }
                        }
                    });
                });
            #endregion

            #region HealthChecks
           // services.ConfigureHealthChecks(Configuration);
            #endregion

            #region Fluent Validation
            /*
            services.AddScoped(typeof(IValidatorInterceptor), typeof(ValidatorInterceptor));
            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
            */
            #endregion

            #region MediatR 
            //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddScoped(typeof(IRequestHandler<,>), typeof(IdempotentCommandHandler<,>));
            #endregion

            #region Configure Snake Case
            /*
            services.AddControllers()
                    .AddJsonOptions(
                options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy =
                        SnakeCaseNamingPolicy.Instance;
                });
            */
            #endregion

            #region Cors
            services.AddCors(o => o.AddPolicy("DisbursementCORSPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            #endregion

            #region Global Exception Filter
            //services.AddMvc(options => options.Filters.Add(typeof(GlobalExceptionFilter)));
            #endregion

            #region DateTime Provider
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();
            #endregion

            #region MassTransit
            var rHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST");
            var rVHost = Environment.GetEnvironmentVariable("RABBITMQ_VHOST");
            var rUser = Environment.GetEnvironmentVariable("RABBITMQ_USER");
            var rPass = Environment.GetEnvironmentVariable("RABBITMQ_PASS");

            //services.AddScoped<IIntegrationEvent, StatusPublisher>();
            services.AddMassTransit(busConfig =>
            {       
                busConfig.UsingRabbitMq((context, cfg) =>
                {
                    cfg.UseHealthCheck(context);

                    cfg.Host(rHost, rVHost, h =>
                    {
                        h.Username(rUser);
                        h.Password(rPass);
                    });

                });
            });
            services.AddMassTransitHostedService();
            #endregion

            services.AddControllersWithViews();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Exception Handler Middleware
            //app.UseMiddleware<ExceptionHandlerMiddleware>();
            #endregion

            #region swagger

            var subRoute = Environment.GetEnvironmentVariable("SUB_ROUTE");
            app.UsePathBase(subRoute);

            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    // build a swagger endpoint for each discovered API version
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"{subRoute}/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                    options.RoutePrefix = "";
                });

            #endregion
            app.UseRouting();

            app.UseCors("DisbursementCORSPolicy"); // Apply CORS before any middleware and UseMVC

            app.UseAuthentication();
            app.UseAuthorization();

            /*
            app.UseHealthChecksUI(option =>
            {
                option.UIPath = "/health-ui";
            });

                app.UseHealthChecks("/health", new HealthCheckOptions()
            {
                ResponseWriter = HealthCheckHelper.WriteResponses
            });
            */

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");

         
                endpoints.MapHealthChecks("/health-ui-response", new HealthCheckOptions()
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
              
            });

            app.UseSerilogRequestLogging();
        }
    }
}
