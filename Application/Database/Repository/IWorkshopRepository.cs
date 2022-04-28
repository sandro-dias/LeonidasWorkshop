using Application.Database.Entities;
using System.Threading.Tasks;

namespace Application.Database.Repository
{
    public interface IWorkshopRepository
    {
        Task<Workshop> InsertWorkshop(Workshop workshop);
    }
}
