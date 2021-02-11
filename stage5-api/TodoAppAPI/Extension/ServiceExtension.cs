using Domain.AggregatesModel.ItemListAggregate;
using Domain.AggregatesModel.TaskListAggregate.cs;
using Infrastracture;
using Infrastracture.Idempotency;
using Infrastracture.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TodoAppAPI.Application.Queries.ItemList;
using TodoAppAPI.Application.Queries.TaskList;

namespace TodoAppAPI.Extension
{
    public static class ServiceExtension
    {

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(o => { });
        }

        //public static void ConfigureLoggerService(this IServiceCollection services)
        //{
        //    services.AddSingleton<ILoggerManager, LoggerManager>();
        //}

        public static void ConfigureMysqlContext(this IServiceCollection services, IConfiguration config)
        {
            #region Initialization
            var connString = Environment.GetEnvironmentVariable("MYSQL_CONNECTIONSTRING");
            services.AddTransient<IDbConnection>(uow => new MySqlConnection(connString));
            #endregion
            #region EF Core
            //var connectionString = config["mysqlconnection:connectionString"];
            services.AddDbContext<ToDoAppDbContext>(o => o.UseMySql(connString));
            #endregion
        }
        public static void ConfigureRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<ITaskListQueries, TaskListQueries>();
            services.AddScoped<IItemListQueries, ItemListQueries>();

            services.AddScoped<ITaskListRepository, TaskListRepo>();
            services.AddScoped<IItemListRepository, ItemListRepo>();

            services.AddScoped<IRequestManager, RequestManager>();

            //services.AddDistributedMemoryCache();
            //services.AddScoped<ITaskListRepo, TaskListRepo>();
            //services.AddScoped<IItemListServices, ItemListServices>();
            //services.AddScoped<IItemListRepo, ItemListRepo>();
        }
    }
}
