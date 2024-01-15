using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Linq;

namespace Application.Data.Specification
{
    internal class GetTodayServicesByWorkshopIdSpecification : Specification<Service>
    {
        public GetTodayServicesByWorkshopIdSpecification(long workshopId, DateTime date)
        {
            Query
                .Where(c => c.WorkshopId == workshopId)
                .Where(c => c.Date.Date == date.Date);
        }
    }
}
