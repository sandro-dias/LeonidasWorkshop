using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Database.Repository
{
    public interface IWorkshopRepository
    {
        Task<Workshop> InsertWorkshop(Workshop workshop);
        Task<Workshop> GetWorkshopWorkload(long workShopId);
    }
}
