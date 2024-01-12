using Application.Data.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Data
{
    public interface IUnitOfWork
    {
        ICustomerRepository CustomerRepository { get; }
        IServiceRepository ServiceRepository { get; }
        IWorkshopRepository WorkshopRepository { get; }
        IWorkingDayRepository WorkingDayRepository { get; }
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}
