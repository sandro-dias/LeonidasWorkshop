using Application.Data;
using Application.Data.Specification;
using Application.Services.CreateWorkingDay;
using Application.Services.CreateWorkingDay.Input;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Infrastructure.Services.CreateWorkingDay
{
    public class CreateWorkingDayService(IUnitOfWork unitOfWork, ILogger<CreateWorkingDayService> logger) : ICreateWorkingDayService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<CreateWorkingDayService> _logger = logger;

        public async Task<Domain.Entities.WorkingDay> ExecuteAsync(CreateWorkingDayInput input)
        {
            var workshop = await _unitOfWork.WorkshopRepository.GetByIdAsync(input.WorkshopId);
            if (workshop is null)
            {
                _logger.LogError("{ClassName} The workshop does not exist on database.", nameof(CreateWorkingDayService));
                return null;
            }

            var workingDay = await _unitOfWork.WorkingDayRepository.FirstOrDefaultAsync(new GetWorkingDayByWorkshopSpecification(input.WorkshopId, input.Date));
            if (workingDay is not null)
                return workingDay;

            workingDay = Domain.Entities.WorkingDay.CreateWorkingDay(input.WorkshopId, input.Date, workshop.Workload);
            if (workingDay.IsWeekendDay() || workingDay.IsWorkingDayWithinFiveBusinessDays())
            {
                _logger.LogError("{ClassName} The working day is not valid.", nameof(CreateWorkingDayService));
                return null;
            }

            workingDay.IsThursdayOrFriday();
            workingDay = await _unitOfWork.WorkingDayRepository.AddAsync(workingDay);
            return workingDay;
        }
    }
}
