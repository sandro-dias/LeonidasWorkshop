using Application.Data.Repository;
using Domain.Entities;
using Infrastructure.Database.Context;

namespace Infrastructure.Data.Repository
{
    public class WorkshopRepository : Repository<Workshop>, IWorkshopRepository
    {
        public WorkshopRepository(WorkshopContext dbContext) : base(dbContext)
        {
        }
    }
}
