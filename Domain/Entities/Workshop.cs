using System;

namespace Domain.Entities
{
    public class Workshop
    {
        public Workshop() { }

        public static Workshop CreateWorkshop(string workShopName, int workload)
        {
            return new Workshop()
            {
                WorkShopName = workShopName,
                Workload = workload
            };
        }

        public long WorkShopId { get; private set; }
        public string WorkShopName { get; private set; }
        public int Workload { get; private set; }
    }
}
