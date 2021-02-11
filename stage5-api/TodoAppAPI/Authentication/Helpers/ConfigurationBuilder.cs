using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TodoAppAPI.Authentication.Helpers
{
    public class ConfigurationBuilder
    {
        private IServiceCollection _services;
        public ConfigurationBuilder(IServiceCollection services)
        {
            _services = services;
        }

        public void AddAuth(IAuthentication<JwtBearerOptions> authentication, string publicKeyXML, string issuer, string clientName)
        {
            authentication.AddAuth(_services, options =>
            {
                RsaSecurityKey issuerSigningKey = null;

                RSA publicRsa = RSA.Create();

                string publicKeyXml = publicKeyXML;

                publicRsa.FromXmlStringCustom(publicKeyXml);
                issuerSigningKey = new RsaSecurityKey(publicRsa);

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidIssuer = issuer,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = issuerSigningKey,
                    RequireSignedTokens = true,
                    ClockSkew = TimeSpan.FromHours(1),

                };

                options.RequireHttpsMetadata = false;

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {

                        Console.ForegroundColor = ConsoleColor.Red;
                        //Console.WriteLine(context.Exception.ToString());
                        //Console.WriteLine(DateTime.Now);

                        return Task.CompletedTask;
                    },
                    OnTokenValidated = ctx =>
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;


                        var identity = ctx.Principal.Identity;
                        var username = ctx.Principal.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;

                        var clientRoles = ctx.Principal.Claims.FirstOrDefault(c => c.Type == "resource_access") != null ?
                            ctx.Principal.Claims.FirstOrDefault(c => c.Type == "resource_access").Value : null;

                        if (clientRoles != null)
                        {
                            var resourceAccess = JsonConvert.DeserializeObject<ResourceAccess>(clientRoles);
                            var claims = new List<Claim>();

                            claims.Add(new Claim(ClaimTypes.Name, username));
                            //claims.Add(new Claim(ClaimTypes.NameIdentifier, userId));

                            if (resourceAccess != null && resourceAccess.disbursement != null)
                            {
                                foreach (var r in resourceAccess.disbursement.Roles)
                                {
                                    claims.Add(new Claim(ClaimTypes.Role, r));
                                }

                                var appIdentity = new ClaimsIdentity(claims);

                                ctx.Principal.AddIdentity(appIdentity);
                            }
                        }


                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        Console.ForegroundColor = ConsoleColor.Red;

                        var d = context;
                        Console.WriteLine(DateTime.Now);

                        return Task.CompletedTask;
                    }
                };
            });
        }
    }


    public class Disbursement
    {
        public string[] Roles { get; set; }
    }
    public class ResourceAccess
    {
        public Disbursement disbursement { get; set; }

    }


    public class RealmAccess
    {
        public List<string> Roles { get; set; }
    }
}
