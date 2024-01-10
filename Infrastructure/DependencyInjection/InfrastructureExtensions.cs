using Application.Database.Repository;
using Infrastructure.Database.Context;
using Infrastructure.Database.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddServices();
            services.AddDataBase();
            return services;
        }
        internal static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IWorkshopRepository, WorkshopRepository>();
        }

        internal static void AddDataBase(this IServiceCollection services)
        {
            services.AddDbContext<WorkshopContext>(x => x.UseInMemoryDatabase("Database"));
            services.AddScoped<WorkshopContext, WorkshopContext>();
        }
    }
}
