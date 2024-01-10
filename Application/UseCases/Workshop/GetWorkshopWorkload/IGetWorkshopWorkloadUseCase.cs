using System.Threading.Tasks;

namespace Application.UseCases.GetWorkshopWorkload
{
    public interface IGetWorkshopWorkloadUseCase
    {
        Task<int> ExecuteAsync(int id);
    }
}
