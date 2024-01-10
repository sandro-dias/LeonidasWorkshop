using System;
using System.Diagnostics.CodeAnalysis;

namespace Application.UseCases.Workshop.CreateWorkingDay.Input
{
    [ExcludeFromCodeCoverage]
    public class CreateWorkingDayInput
    {
        public int WorkshopId { get; init; }
        public DateTime Date { get; init; }
    }
}
