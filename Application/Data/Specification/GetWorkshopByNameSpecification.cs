using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Data.Specification
{
    public class GetWorkshopByNameSpecification : Specification<Workshop>
    {
        public GetWorkshopByNameSpecification(string name)
        {
            Query
                .Where(c => c.Name == name);
        }
    }
}
