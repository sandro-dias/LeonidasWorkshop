using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.UseCases.Service.GetServices
{
    public interface IGetServicesUseCase
    {
        Task<IEnumerable<Domain.Entities.Service>> ExecuteAsync(long workshopId);
    }
}
