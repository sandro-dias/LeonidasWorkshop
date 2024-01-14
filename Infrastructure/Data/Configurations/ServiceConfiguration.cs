using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("SERVICOS");

            builder.HasKey(x => x.ServiceId);
            builder.Property(x => x.ServiceId).HasColumnName("ID_SERVICO").HasColumnType("INT");
            builder.Property(x => x.Date).HasColumnName("DATA").HasColumnType("DATETIME");
            builder.Property(x => x.Workload).HasColumnName("CARGA").HasColumnType("INT");
            builder.Property(x => x.WorkshopId).HasColumnName("ID_OFICINA").HasColumnType("INT");
            builder.Property(x => x.CustomerId).HasColumnName("ID_CLIENTE").HasColumnType("INT");
        }
    }
}
