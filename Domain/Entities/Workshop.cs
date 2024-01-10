using System;

namespace Domain.Entities
{
    public class Workshop
    {
        public Workshop() { }

        public Workshop(long workShopId, int workload, DateTime date, int availableWorkload)
        {
            WorkShopId = workShopId;
            Workload = workload;
            Date = date;
            AvailableWorkload = availableWorkload;
        }

        public static Workshop CreateWorkshop(string workShopName, int workload)
        {
            return new Workshop()
            {
                WorkShopName = workShopName,
                Workload = workload,
                Date = DateTime.Now,
                AvailableWorkload = workload
            };
        }

        public long WorkShopId { get; private set; }
        public string WorkShopName { get; private set; }
        public int Workload { get; private set; }
        public DateTime Date { get; private set; }
        public int AvailableWorkload { get; private set; }

        public bool IsThursday()
        {
            return Date.DayOfWeek == DayOfWeek.Thursday;
        }

        public bool IsFriday()
        {
            return Date.DayOfWeek == DayOfWeek.Friday;
        }

        public int UpdateAvailableWorkload(int serviceWorkload)
        {
            return AvailableWorkload -= serviceWorkload;
        }
    }
}
