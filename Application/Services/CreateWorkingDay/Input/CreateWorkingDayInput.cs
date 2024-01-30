using System;
using System.Diagnostics.CodeAnalysis;

namespace Application.Services.CreateWorkingDay.Input
{
    [ExcludeFromCodeCoverage]
    public class CreateWorkingDayInput(long workshopId, int workshopWorkload, DateTime date)
    {
        public long WorkshopId { get; init; } = workshopId;
        public int WorkshopWorkload { get; init; } = workshopWorkload;
        public DateTime Date { get; init; } = date;
    }
}
