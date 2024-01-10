using System;

namespace Domain.Entities
{
    public class WorkingDay
    {
        private const double EndOfTheWeekSpreadOverload = 0.3;

        public WorkingDay() { }

        public long WorkingDayId { get; private set; }
        public long WorkshopId { get; private set; }
        public DateTime Date { get; private set; }
        public int AvailableWorkload { get; private set; }

        public static WorkingDay CreateWorkingDay(long workshopId, DateTime date, int availableWorkload)
        {
            return new WorkingDay()
            {
                WorkshopId = workshopId,
                Date = date,
                AvailableWorkload = availableWorkload
            };
        }

        public bool IsWeekendDay()
        {
            return (Date.DayOfWeek == DayOfWeek.Saturday || Date.DayOfWeek == DayOfWeek.Sunday);
        }

        public bool IsWorkingDayWithinFiveBusinessDays()
        {
            var today = DateTime.Today;

            return false;
        }

        public void IsThursdayOrFriday()
        {
            if (Date.DayOfWeek == DayOfWeek.Thursday || Date.DayOfWeek == DayOfWeek.Friday)
                AvailableWorkload += (int)EndOfTheWeekSpreadOverload * AvailableWorkload;
        }

        public int UpdateAvailableWorkload(int serviceWorkload)
        {
            return AvailableWorkload -= serviceWorkload;
        }
    }
}
