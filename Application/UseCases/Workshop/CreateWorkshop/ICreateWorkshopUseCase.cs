using Application.UseCases.CreateWorkshop.Input;
using System.Threading.Tasks;

namespace Application.UseCases.CreateWorkshop
{
    public interface ICreateWorkshopUseCase
    {
        Task<Domain.Entities.Workshop> ExecuteAsync(CreateWorkshopInput input);
    }
}
