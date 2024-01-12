using Domain.Entities;
using System.Collections.Generic;

namespace Application.UseCases.Workshop.GetWorkshopWorkload.Output
{
    public class GetWorkshopWorkloadOutput
    {
        public List<WorkingDay> WorkingDayList { get; set; }
    }
}
