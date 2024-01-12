using Application.Data.Repository;
using Domain.Entities;
using Infrastructure.Database.Context;

namespace Infrastructure.Data.Repository
{
    public class WorkingDayRepository : Repository<WorkingDay>, IWorkingDayRepository
    {
        public WorkingDayRepository(WorkshopContext dbContext) : base(dbContext)
        {
        }
    }
}
