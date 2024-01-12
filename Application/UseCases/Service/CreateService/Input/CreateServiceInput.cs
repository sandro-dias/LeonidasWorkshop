using Domain.Entities;
using System;

namespace Application.UseCases.Service.CreateService.Input
{
    public class CreateServiceInput
    {
        public long WorkshopId { get; init; }
        public long CustomerId { get; init; }
        public DateTime Date { get; init; }
        public ServiceWorkload Workload { get; init; }
    }
}
