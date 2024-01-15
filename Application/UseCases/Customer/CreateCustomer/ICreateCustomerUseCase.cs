using Application.UseCases.Customer.CreateCustomer.Input;
using System.Threading.Tasks;

namespace Application.UseCases.Customer.CreateCustomer
{
    public interface ICreateCustomerUseCase
    {
        Task<Domain.Entities.Customer> ExecuteAsync(CreateCustomerInput input);
    }
}
