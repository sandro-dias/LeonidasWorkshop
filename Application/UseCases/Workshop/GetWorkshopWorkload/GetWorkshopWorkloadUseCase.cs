using Application.Data;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Application.UseCases.GetWorkshopWorkload
{
    public class GetWorkshopWorkloadUseCase : IGetWorkshopWorkloadUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetWorkshopWorkloadUseCase> _logger;

        public GetWorkshopWorkloadUseCase(IUnitOfWork unitOfWork, ILogger<GetWorkshopWorkloadUseCase> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<int> ExecuteAsync(int workshopId)
        {
            //TODO: criar output para voltar os próximos 5 dias úteis
            var result = await _unitOfWork.WorkshopRepository.GetByIdAsync(workshopId);
            return result.Workload;
        }
    }
}
