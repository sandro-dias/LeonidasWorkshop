using Application.Data;
using Application.Data.Repository;
using Infrastructure.Data.Repository;
using Infrastructure.Database.Context;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WorkshopContext _context;

        private CustomerRepository _customerRepository;
        private WorkshopRepository _workshopRepository;
        private WorkingDayRepository _workingDayRepository;
        private ServiceRepository _serviceRepository;

        public UnitOfWork(WorkshopContext context)
        {
            _context = context;
        }

        public ICustomerRepository CustomerRepository
        {
            get { return _customerRepository ??= new CustomerRepository(_context); }
        }

        public IServiceRepository ServiceRepository
        {
            get { return _serviceRepository ??= new ServiceRepository(_context); }
        }

        public IWorkshopRepository WorkshopRepository
        {
            get { return _workshopRepository ??= new WorkshopRepository(_context); }
        }

        public IWorkingDayRepository WorkingDayRepository
        {
            get { return _workingDayRepository ??= new WorkingDayRepository(_context); }
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
