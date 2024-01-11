using Application.Data.Repository;
using Domain.Entities;
using Infrastructure.Database.Context;

namespace Infrastructure.Data.Repository
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        public ServiceRepository(WorkshopContext dbContext) : base(dbContext)
        {
        }
    }
}
