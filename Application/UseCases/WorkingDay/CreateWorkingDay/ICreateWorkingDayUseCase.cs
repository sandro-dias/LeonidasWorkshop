using Application.UseCases.Workshop.CreateWorkingDay.Input;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.UseCases.Workshop.CreateWorkingDay
{
    public interface ICreateWorkingDayUseCase
    {
        Task<WorkingDay> ExecuteAsync(CreateWorkingDayInput input);
    }
}
