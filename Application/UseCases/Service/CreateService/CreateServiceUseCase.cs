using Application.Data;
using Application.Services.CreateWorkingDay;
using Application.Services.CreateWorkingDay.Input;
using Application.UseCases.Service.CreateService.Input;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Application.UseCases.Service.CreateService
{
    public class CreateServiceUseCase(IUnitOfWork unitOfWork, ILogger<CreateServiceUseCase> logger, ICreateWorkingDayService createWorkingDayService) : ICreateServiceUseCase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<CreateServiceUseCase> _logger = logger;
        private readonly ICreateWorkingDayService _createWorkingDayUseCase = createWorkingDayService;

        public async Task<Domain.Entities.Service> ExecuteAsync(CreateServiceInput input)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(input.CustomerId);
            if (customer is null)
            {
                _logger.LogError("{ClassName} The customer does not exist on database", nameof(CreateServiceUseCase));
                return default;
            }

            var workshop = await _unitOfWork.WorkshopRepository.GetByIdAsync(input.WorkshopId);
            if (workshop is null)
            {
                _logger.LogError("{ClassName} The workshop does not exist on database", nameof(CreateServiceUseCase));
                return default;
            }

            var workingDay = await _createWorkingDayUseCase.CreateWorkingDay(new CreateWorkingDayInput(input.WorkshopId, workshop.Workload, input.Date));
            if (workingDay is null)
            {
                _logger.LogError("{ClassName} The service cannot be scheduled on this workingDay", nameof(CreateServiceUseCase));
                return default;
            }

            if (!IsAvailableWorkloadGreaterThanServiceWorkload(workingDay.AvailableWorkload, (int)input.Workload))
                return default;

            workingDay.LowerAvailableWorkload((int)input.Workload);
            var service = Domain.Entities.Service.CreateService(input.CustomerId, input.WorkshopId, input.Date, input.Workload);

            service = await _unitOfWork.ServiceRepository.AddAsync(service);
            await _unitOfWork.WorkingDayRepository.UpdateAsync(workingDay);
            await _unitOfWork.CommitAsync();

            return service;
        }

        public bool IsAvailableWorkloadGreaterThanServiceWorkload(int availableWorkload, int serviceWorkload)
        {
            if (availableWorkload >= serviceWorkload)
                return true;
            return false;
        }
    }
}
