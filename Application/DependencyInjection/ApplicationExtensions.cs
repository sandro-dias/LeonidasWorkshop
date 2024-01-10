using Application.UseCases.GetWorkshopWorkload;
using Application.UseCases.PostWorkshop;
using Application.UseCases.PostWorkshop.Input;
using Application.UseCases.PostWorkshop.Validator;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddUseCases();
            services.AddValidators();
            return services;
        }

        internal static void AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<IPostWorkshopUseCase, PostWorkshopUseCase>();
            services.AddTransient<IGetWorkshopWorkloadUseCase, GetWorkshopWorkloadUseCase>();            
        }

        internal static void AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<PostWorkshopInput>, PostWorkshopInputValidator>();
        }
    }
}
