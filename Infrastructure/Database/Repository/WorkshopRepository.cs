using Application.Database.Repository;
using Domain.Entities;
using Infrastructure.Database.Context;
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

        public async Task<Workshop> GetWorkshop(long workshopId)
        {
            //TODO: refatorar para passar e buscar uma especificação que possa pegar o ID e a data
            var workshop = await _context.Workshop.FindAsync(workshopId);
            return workshop;
        }

        public async Task<Workshop> CreateWorkshop(Workshop workshop)
        {
            var addedWorkshop = _context.Workshop.Add(workshop);
            await _context.SaveChangesAsync();
            return addedWorkshop.Entity;
        }
    }
}
