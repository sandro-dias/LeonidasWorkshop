using Application.UseCases.Workshop.CreateWorkingDay.Input;
using System.Threading.Tasks;

namespace Application.UseCases.Workshop.CreateWorkingDay
{
    public interface ICreateWorkingDayUseCase
    {
        Task ExecuteAsync(CreateWorkingDayInput input);
    }
}
