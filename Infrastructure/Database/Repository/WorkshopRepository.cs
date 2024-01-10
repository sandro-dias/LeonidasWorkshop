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

        public async Task<Workshop> GetWorkshopWorkload(long workshopId)
        {
            //TODO entender como ficar input e retorno das entidades de dominio/infra
            var workshop = await _context.Workshop.FindAsync(workshopId);
            return workshop;
        }

        public async Task<Workshop> InsertWorkshop(Workshop workshop)
        {
            var addedWorkshop = _context.Workshop.Add(workshop);
            await _context.SaveChangesAsync();
            return addedWorkshop.Entity;
        }
    }
}
