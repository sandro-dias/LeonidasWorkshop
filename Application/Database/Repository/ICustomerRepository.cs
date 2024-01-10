using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Database.Repository
{
    public interface ICustomerRepository
    {
        Task<Customer> InsertCustomer(Customer customer);
    }
}
