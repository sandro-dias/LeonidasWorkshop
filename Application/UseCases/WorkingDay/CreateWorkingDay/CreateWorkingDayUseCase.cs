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

        public async Task ExecuteAsync(CreateWorkingDayInput input)
        {
            var workshop = await _unitOfWork.WorkshopRepository.GetByIdAsync(input.WorkshopId);
            if (workshop is null)
            {
                _logger.LogError("{ClassName} The workshop is does not exist on database.", nameof(CreateWorkingDayUseCase));
                return;
            }

            var newWorkingDay = WorkingDay.CreateWorkingDay(input.WorkshopId, input.Date, workshop.Workload);
            if (newWorkingDay.IsWeekendDay() || newWorkingDay.IsWorkingDayWithinFiveBusinessDays())
            {
                _logger.LogError("{ClassName} The working day is not valid.", nameof(CreateWorkingDayUseCase));
                return;
            }

            newWorkingDay.IsThursdayOrFriday();

            //TODO: criar camada de infra para salvar na tabela de Dias de trabalho
        }
    }
}
