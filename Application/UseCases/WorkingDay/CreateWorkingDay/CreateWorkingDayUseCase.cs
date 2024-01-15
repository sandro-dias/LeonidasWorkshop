using Application.Data;
using Application.Data.Specification;
using Application.UseCases.Workshop.CreateWorkingDay.Input;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Application.UseCases.Workshop.CreateWorkingDay
{
    public class CreateWorkingDayUseCase : ICreateWorkingDayUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateWorkingDayUseCase> _logger;

        public CreateWorkingDayUseCase(IUnitOfWork unitOfWork, ILogger<CreateWorkingDayUseCase> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<WorkingDay> ExecuteAsync(CreateWorkingDayInput input)
        {
            var workshop = await _unitOfWork.WorkshopRepository.GetByIdAsync(input.WorkshopId);
            if (workshop is null)
            {
                _logger.LogError("{ClassName} The workshop does not exist on database.", nameof(CreateWorkingDayUseCase));
                return null;
            }

            var workingDay = await _unitOfWork.WorkingDayRepository
                                        .FirstOrDefaultAsync(new GetWorkingDayByWorkshopSpecification(input.WorkshopId, input.Date));
            if (workingDay is not null)
            {
                _logger.LogWarning("{ClassName} The working day already exists.", nameof(CreateWorkingDayUseCase));
                return workingDay;
            }

            workingDay = WorkingDay.CreateWorkingDay(input.WorkshopId, input.Date, workshop.Workload);
            if (workingDay.IsWeekendDay() || workingDay.IsWorkingDayWithinFiveBusinessDays())
            {
                _logger.LogError("{ClassName} The working day is not valid.", nameof(CreateWorkingDayUseCase));
                return null;
            }

            workingDay.IsThursdayOrFriday();
            workingDay = await _unitOfWork.WorkingDayRepository.AddAsync(workingDay);
            return workingDay;
        }
    }
}
