using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Linq;

namespace Application.Data.Specification
{
    public class GetWorkingDayByDateRangeSpecification : Specification<WorkingDay>
    {
        public GetWorkingDayByDateRangeSpecification(long workshopId, DateTime initialDate, DateTime endDate)
        {
            Query
                .Where(c => c.WorkshopId == workshopId)
                .Where(x => x.Date.Date >= initialDate.Date)
                .Where(x => x.Date.Date < endDate.Date);
        }
    }
}
