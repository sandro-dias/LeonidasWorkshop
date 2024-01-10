using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Database.Repository
{
    public interface IServiceRepository
    {
        Task<Service> InsertWorkshop(Service workshop);
    }
}
