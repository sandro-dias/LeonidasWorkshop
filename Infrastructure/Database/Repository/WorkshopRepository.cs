using Application.Database.Entities;
using Application.Database.Repository;
using System.Threading.Tasks;

namespace Infrastructure.Database.Repository
{
    public class WorkshopRepository : IWorkshopRepository
    {
        private readonly WorkshopContext _context;

        public WorkshopRepository(WorkshopContext context)
        {
            _context = context;
        }
        public async Task<Workshop> InsertWorkshop(Workshop workshop)
        {
            _context.Workshop.Add(workshop);
            await _context.SaveChangesAsync();
            return workshop;
        }
    }
}
