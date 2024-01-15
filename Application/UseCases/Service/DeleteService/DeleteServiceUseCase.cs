using Application.Data;
using Application.Data.Specification;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Application.UseCases.Service.DeleteService
{
    public class DeleteServiceUseCase : IDeleteServiceUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteServiceUseCase> _logger;

        public DeleteServiceUseCase(IUnitOfWork unitOfWork, ILogger<DeleteServiceUseCase> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task ExecuteAsync(long serviceId)
        {
            var service = await _unitOfWork.ServiceRepository.GetByIdAsync(serviceId);
            if (service == null)
            {
                _logger.LogError("{[ClassName]} The service does not exist on database", nameof(DeleteServiceUseCase));
                return;
            }

            var workingDay = await _unitOfWork.WorkingDayRepository.FirstOrDefaultAsync(new GetWorkingDayByWorkshopSpecification(service.WorkshopId, service.Date));
            workingDay.IncreaseAvailableWorkload((int)service.Workload);

            await _unitOfWork.ServiceRepository.DeleteAsync(service);
            await _unitOfWork.WorkingDayRepository.UpdateAsync(workingDay);
            await _unitOfWork.CommitAsync();
        }
    }
}
