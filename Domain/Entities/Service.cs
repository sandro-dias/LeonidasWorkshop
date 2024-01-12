using System;

namespace Domain.Entities
{
    public class Service : Entity
    {
        public Service() { }

        public long ServiceId { get; private set; }
        public long CustomerId { get; private set; }
        public long WorkshopId { get; private set; }
        public DateTime Date { get; private set; }
        public ServiceWorkload Workload { get; private set; }

        public static Service CreateService(long customerId, long workshopId, DateTime date, ServiceWorkload workload)
        {
            return new Service()
            {
                CustomerId = customerId,
                WorkshopId = workshopId,
                Date = date,
                Workload = workload
            };
        }
    }
}
