using Application.Data;
using Application.Data.Specification;
using Application.UseCases.Service.CreateService.Input;
using Application.UseCases.Workshop.CreateWorkingDay;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Application.UseCases.Service.CreateService
{
    public class CreateServiceUseCase : ICreateServiceUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateServiceUseCase> _logger;
        private readonly ICreateWorkingDayUseCase _createWorkingDayUseCase;

        public async Task<Domain.Entities.Service> ExecuteAsync(CreateServiceInput input)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(input.CustomerId);
            if (customer == null)
            {
                _logger.LogError("");
                return null;
            }

            var workshop = await _unitOfWork.WorkshopRepository.GetByIdAsync(input.WorkshopId);
            if (workshop == null)
            {
                _logger.LogError("");
                return null;
            }

            var workingDay = await _unitOfWork.WorkingDayRepository
                                        .FirstOrDefaultAsync(new GetWorkingDayByWorkshopSpecification(input.WorkshopId, input.Date));

            workingDay ??= await _createWorkingDayUseCase.ExecuteAsync(new Workshop.CreateWorkingDay.Input.CreateWorkingDayInput(input.WorkshopId, input.Date));
            if (workingDay is null)
            {
                _logger.LogError("");
                return null;
            }

            if (!IsAvailableWorkloadGreaterThanServiceWorkload(workingDay.AvailableWorkload, (int)input.Workload))
                return null;

            var service = Domain.Entities.Service.CreateService(input.CustomerId, input.WorkshopId, input.Date, input.Workload);
            service = await _unitOfWork.ServiceRepository.AddAsync(service);
            return service;
        }

        private static bool IsAvailableWorkloadGreaterThanServiceWorkload(int availableWorkload, int serviceWorkload)
        {
            if (availableWorkload > serviceWorkload)
                return true;
            return false;
        }
    }
}
