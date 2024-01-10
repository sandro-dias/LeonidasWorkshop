using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Database.Repository
{
    public interface IWorkshopRepository
    {
        Task<Workshop> CreateWorkshop(Workshop workshop);
        Task<Workshop> GetWorkshop(long workShopId);
    }
}
