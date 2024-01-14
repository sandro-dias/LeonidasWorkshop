using Application.Data;
using Application.Data.Specification;
using Application.UseCases.Customer.CreateCustomer.Input;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Application.UseCases.Customer.CreateCustomer
{
    public class CreateCustomerUseCase : ICreateCustomerUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateCustomerUseCase> _logger;

        public CreateCustomerUseCase(IUnitOfWork unitOfWork, ILogger<CreateCustomerUseCase> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Domain.Entities.Customer> ExecuteAsync(CreateCustomerInput input)
        {
            //TODO: colocar validação de input
            var customer = await _unitOfWork.CustomerRepository.FirstOrDefaultAsync(new GetCustomerByCPFSpecification(input.CPF));
            if (customer != null)
            {
                _logger.LogWarning("");
                return customer;
            }

            customer = Domain.Entities.Customer.CreateCustomer(input.Name, input.CPF);
            await _unitOfWork.CustomerRepository.AddAsync(customer);
            await _unitOfWork.CommitAsync();
            return customer;
        }
    }
}
