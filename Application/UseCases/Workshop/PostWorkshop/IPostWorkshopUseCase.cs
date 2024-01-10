using Application.UseCases.PostWorkshop.Input;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.UseCases.PostWorkshop
{
    public interface IPostWorkshopUseCase
    {
        Task<Workshop> ExecuteAsync(PostWorkshopInput input);
    }
}
