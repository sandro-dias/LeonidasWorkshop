using System.Threading.Tasks;

namespace Application.UseCases.Service.DeleteService
{
    public interface IDeleteServiceUseCase
    {
        Task ExecuteAsync(long serviceId);
    }
}
