﻿using Application.Data;
using Application.Data.Repository;
using Application.Database.Repository;
using Infrastructure.Data.Repository;
using Infrastructure.Database;
using Infrastructure.Database.Context;
using Infrastructure.Database.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataBase(configuration);
            services.AddUnitOfWork();
            services.AddRepositories();
            return services;
        }

        private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IWorkshopRepository, WorkshopRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IWorkingDayRepository, WorkingDayRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            return services;
        }

        private static IServiceCollection AddDataBase(this IServiceCollection services, IConfiguration configuration)
        {
            //TODO: migrar projeto para o .NET 6.0
            services.AddDbContextPool<WorkshopContext>(
                (builder) =>
                {
                    builder.UseSqlServer(configuration.GetConnectionString("WorkshopContext"), sqlServerOptions => sqlServerOptions.CommandTimeout(600));
                });

            //services.AddDbContext<WorkshopContext>(x => x.UseInMemoryDatabase("Database"));
            //services.AddScoped<WorkshopContext, WorkshopContext>();
            return services;
        }
    }
}
