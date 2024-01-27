using Application.Services.CreateWorkingDay.Input;
using System.Threading.Tasks;

namespace Application.Services.CreateWorkingDay
{
    public interface ICreateWorkingDayService
    {
        Task<Domain.Entities.WorkingDay> ExecuteAsync(CreateWorkingDayInput input);
    }
}
