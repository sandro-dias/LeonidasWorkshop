using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Linq;

namespace Application.Data.Specification
{
    public class GetWorkingDayByWorkshopSpecification : Specification<WorkingDay>
    {
        public GetWorkingDayByWorkshopSpecification(long workshopId, DateTime workingDay)
        {
            Query
                .Where(c => c.WorkshopId == workshopId)
                .Where(x => x.Date.Date == workingDay.Date);
        }
    }
}
