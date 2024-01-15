using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Data.Specification
{
    public class GetCustomerByCPFSpecification : Specification<Customer>
    {
        public GetCustomerByCPFSpecification(string cpf)
        {
            Query
                .Where(c => c.CPF == cpf);
        }
    }
}
