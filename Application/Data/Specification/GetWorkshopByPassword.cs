using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Data.Specification
{
    public class GetWorkshopByPassword : Specification<Workshop>
    {
        public GetWorkshopByPassword(string cNPJ, string password)
        {
            Query
                .Where(c => c.CNPJ == cNPJ)
                .Where(c => c.Password == password);
        }
    }
}
