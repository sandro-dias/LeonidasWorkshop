using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class WorkingDay : Entity
    {
        private const double EndOfTheWeekSpreadOverload = 0.3;
        private const int BusinessDaysRange = 5;

        public WorkingDay() { }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
            if (Date.Date <= today)
                return true;

            var businessDays = GetBusinessDays(today, Date.Date);
            if (businessDays <= BusinessDaysRange)
                return false;

            return true;
        }

        public static double GetBusinessDays(DateTime startD, DateTime endD)
        {
            double calcBusinessDays =
                1 + ((endD - startD).TotalDays * 5 -
                (startD.DayOfWeek - endD.DayOfWeek) * 2) / 7;

            if (endD.DayOfWeek == DayOfWeek.Saturday) calcBusinessDays--;
            if (startD.DayOfWeek == DayOfWeek.Sunday) calcBusinessDays--;

            return calcBusinessDays;
        }

        public void IsThursdayOrFriday()
        {
            if (Date.DayOfWeek == DayOfWeek.Thursday || Date.DayOfWeek == DayOfWeek.Friday)
                AvailableWorkload += (int)(EndOfTheWeekSpreadOverload * AvailableWorkload);
        }

        public int LowerAvailableWorkload(int serviceWorkload)
        {
            return AvailableWorkload -= serviceWorkload;
        }

        public int IncreaseAvailableWorkload(int serviceWorkload)
        {
            return AvailableWorkload += serviceWorkload;
        }
    }
}
