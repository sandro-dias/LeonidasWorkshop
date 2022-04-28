using Application.UseCases.PostWorkshop.Input;
using System.Threading.Tasks;

namespace Application.UseCases.PostWorkshop
{
    public interface IPostWorkshopUseCase
    {
        Task ExecuteAsync(PostWorkshopInput input);
    }
}
