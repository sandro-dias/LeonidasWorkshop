using Application.Database.Repository;
using Application.UseCases.Workshop.CreateWorkingDay.Input;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Application.UseCases.Workshop.CreateWorkingDay
{
    public class CreateWorkingDayUseCase : ICreateWorkingDayUseCase
    {
        private readonly IWorkshopRepository _workshopRepository;
        private readonly ILogger<CreateWorkingDayUseCase> _logger;

        public async Task ExecuteAsync(CreateWorkingDayInput input)
        {
            var workshop = await _workshopRepository.GetWorkshop(input.WorkshopId);
            if (workshop is null)
            {
                _logger.LogError("");
                return;
            }

            var newWorkingDay = WorkingDay.CreateWorkingDay(input.WorkshopId, input.Date, workshop.Workload);
            if (newWorkingDay.IsWeekendDay() || newWorkingDay.IsWorkingDayWithinFiveBusinessDays())
            {
                _logger.LogError("");
                return;
            }

            newWorkingDay.IsThursdayOrFriday();

            //TODO: criar camada de infra para salvar na tabela de Dias de trabalho
        }
    }
}
