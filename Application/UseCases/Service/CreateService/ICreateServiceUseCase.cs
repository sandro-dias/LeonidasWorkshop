using Application.UseCases.Service.CreateService.Input;
using System.Threading.Tasks;

namespace Application.UseCases.Service.CreateService
{
    public interface ICreateServiceUseCase
    {
        Task<Domain.Entities.Service> ExecuteAsync(CreateServiceInput input);
    }
}
