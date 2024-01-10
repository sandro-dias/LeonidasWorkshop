using System;

namespace Domain.Entities
{
    public class Service
    {
        public long ServiceId { get; private set; }
        public long ClientId { get; private set; }
        public long WorkshopId { get; private set; }
        public DateTime Date { get; private set; }
        public ServiceWorkload Workload { get; private set; }
    }
}
