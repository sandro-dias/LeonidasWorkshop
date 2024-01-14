using Application.UseCases.CreateWorkshop;
using Application.UseCases.CreateWorkshop.Input;
using Application.UseCases.CreateWorkshop.Validator;
using Application.UseCases.GetWorkshopWorkload;
using Application.UseCases.Service.CreateService;
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
            services.AddScoped<ICreateWorkshopUseCase, CreateWorkshopUseCase>();
            services.AddScoped<IGetWorkshopWorkloadUseCase, GetWorkshopWorkloadUseCase>();
            services.AddScoped<ICreateServiceUseCase, CreateServiceUseCase>();
            services.AddScoped<ICreateWorkingDayUseCase, CreateWorkingDayUseCase>();
        }

        internal static void AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<CreateWorkshopInput>, CreateWorkshopInputValidator>();
        }
    }
}
