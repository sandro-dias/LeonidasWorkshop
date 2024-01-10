using Application.Database.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Application.UseCases.GetWorkshopWorkload
{
    public class GetWorkshopWorkloadUseCase : IGetWorkshopWorkloadUseCase
    {
        private readonly IWorkshopRepository _workshopRepository;
        private readonly ILogger<GetWorkshopWorkloadUseCase> _logger;

        public GetWorkshopWorkloadUseCase(IWorkshopRepository workshopRepository, ILogger<GetWorkshopWorkloadUseCase> logger)
        {
            _workshopRepository = workshopRepository;
            _logger = logger;
        }

        public async Task<int> ExecuteAsync(int workshopId)
        {
            //TODO: criar output para voltar os próximos 5 dias úteis
            var result = await _workshopRepository.GetWorkshop(workshopId);
            return result.Workload;
        }
    }
}
