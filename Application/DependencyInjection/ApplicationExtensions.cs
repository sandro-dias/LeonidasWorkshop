using Application.UseCases.GetWorkshopWorkload;
using Application.UseCases.PostWorkshop;
using Application.UseCases.PostWorkshop.Input;
using Application.UseCases.PostWorkshop.Validator;
using Application.UseCases.Workshop.CreateWorkingDay;
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
            services.AddScoped<IPostWorkshopUseCase, PostWorkshopUseCase>();
            services.AddScoped<IGetWorkshopWorkloadUseCase, GetWorkshopWorkloadUseCase>();
            services.AddScoped<ICreateWorkingDayUseCase, CreateWorkingDayUseCase>();
        }

        internal static void AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<PostWorkshopInput>, PostWorkshopInputValidator>();
        }
    }
}
