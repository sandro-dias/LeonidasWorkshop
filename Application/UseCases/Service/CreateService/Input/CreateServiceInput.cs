using Domain.Entities;
using System;

namespace Application.UseCases.Service.CreateService.Input
{
    public class CreateServiceInput(long workshopId, long customerId, DateTime date, ServiceWorkload workload)
    {
        public long WorkshopId { get; set; } = workshopId;
        public long CustomerId { get; set; } = customerId;
        public DateTime Date { get; set; } = date;
        public ServiceWorkload Workload { get; set; } = workload;
    }
}
