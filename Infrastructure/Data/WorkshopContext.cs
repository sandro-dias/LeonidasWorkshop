using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Database.Context
{
    public class WorkshopContext : DbContext
    {
        protected WorkshopContext() {}

        public WorkshopContext(DbContextOptions<WorkshopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Workshop> Workshop { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<WorkingDay> WorkingDay { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.UsePropertyAccessMode(PropertyAccessMode.Property);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
