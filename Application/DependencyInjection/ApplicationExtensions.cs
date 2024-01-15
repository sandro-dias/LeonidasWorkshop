using Application.UseCases.CreateWorkshop;
using Application.UseCases.CreateWorkshop.Input;
using Application.UseCases.CreateWorkshop.Validator;
using Application.UseCases.Customer.CreateCustomer;
using Application.UseCases.Customer.CreateCustomer.Input;
using Application.UseCases.Customer.CreateCustomer.Validator;
using Application.UseCases.GetWorkshopWorkload;
using Application.UseCases.Service.CreateService;
using Application.UseCases.Service.DeleteService;
using Application.UseCases.Service.GetServices;
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
            services.AddScoped<ICreateCustomerUseCase, CreateCustomerUseCase>();
            services.AddScoped<IGetServicesUseCase, GetServicesUseCase>();
            services.AddScoped<IDeleteServiceUseCase, DeleteServiceUseCase>();
        }

        internal static void AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<CreateWorkshopInput>, CreateWorkshopInputValidator>();
            services.AddTransient<IValidator<CreateCustomerInput>, CreateCustomerInputValidator>();
        }
    }
}
