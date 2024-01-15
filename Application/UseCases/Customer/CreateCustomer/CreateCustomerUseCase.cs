using Application.Data;
using Application.Data.Specification;
using Application.UseCases.Customer.CreateCustomer.Input;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Application.UseCases.Customer.CreateCustomer
{
    public class CreateCustomerUseCase : ICreateCustomerUseCase
    {
        private readonly IValidator<CreateCustomerInput> _validator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateCustomerUseCase> _logger;

        public CreateCustomerUseCase(IValidator<CreateCustomerInput> validator, IUnitOfWork unitOfWork, ILogger<CreateCustomerUseCase> logger)
        {
            _validator = validator;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Domain.Entities.Customer> ExecuteAsync(CreateCustomerInput input)
        {
            var validationResult = _validator.Validate(input);
            if (!validationResult.IsValid)
            {
                _logger.LogError("[{ClassName}] The input returned an error: {Errors}", nameof(CreateCustomerUseCase), validationResult.Errors);
                return null;
            }

            var customer = await _unitOfWork.CustomerRepository.FirstOrDefaultAsync(new GetCustomerByCPFSpecification(input.CPF));
            if (customer != null)
            {
                _logger.LogWarning("[{ClassName}] The customer already exists on database", nameof(CreateCustomerUseCase));
                return customer;
            }

            customer = Domain.Entities.Customer.CreateCustomer(input.Name, input.CPF);
            await _unitOfWork.CustomerRepository.AddAsync(customer);
            await _unitOfWork.CommitAsync();
            return customer;
        }
    }
}
