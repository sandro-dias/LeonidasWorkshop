using Application.UseCases.Workshop.GetWorkshopWorkload.Output;
using System.Threading.Tasks;

namespace Application.UseCases.GetWorkshopWorkload
{
    public interface IGetWorkshopWorkloadUseCase
    {
        Task<GetWorkshopWorkloadOutput> ExecuteAsync(long workshopId);
    }
}
