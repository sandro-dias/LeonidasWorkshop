using System;
using System.Diagnostics.CodeAnalysis;

namespace Application.Services.CreateWorkingDay.Input
{
    [ExcludeFromCodeCoverage]
    public class CreateWorkingDayInput
    {
        public CreateWorkingDayInput(long workshopId, DateTime date)
        {
            WorkshopId = workshopId;
            Date = date;
        }

        public long WorkshopId { get; init; }
        public DateTime Date { get; init; }
    }
}
