using Application.Data;
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
            //TODO: melhorar retorno de erros para a controller
            //TODO: validar se o dia de trabalho já se existe antes de adicionar
            var workshop = await _unitOfWork.WorkshopRepository.GetByIdAsync(input.WorkshopId);
            if (workshop is null)
            {
                _logger.LogError("{ClassName} The workshop does not exist on database.", nameof(CreateWorkingDayUseCase));
                return null;
            }

            var newWorkingDay = WorkingDay.CreateWorkingDay(input.WorkshopId, input.Date, workshop.Workload);
            if (newWorkingDay.IsWeekendDay() || newWorkingDay.IsWorkingDayWithinFiveBusinessDays())
            {
                _logger.LogError("{ClassName} The working day is not valid.", nameof(CreateWorkingDayUseCase));
                return null;
            }

            newWorkingDay.IsThursdayOrFriday();
            newWorkingDay = await _unitOfWork.WorkingDayRepository.AddAsync(newWorkingDay);
            return newWorkingDay;
        }
    }
}
