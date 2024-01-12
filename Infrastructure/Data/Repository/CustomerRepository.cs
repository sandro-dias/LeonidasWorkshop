using Application.Data.Repository;
using Domain.Entities;
using Infrastructure.Database.Context;

namespace Infrastructure.Data.Repository
{
    class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(WorkshopContext dbContext) : base(dbContext)
        {
        }
    }
}
