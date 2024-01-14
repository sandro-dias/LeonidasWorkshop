using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class WorkingDayConfiguration : IEntityTypeConfiguration<WorkingDay>
    {
        public void Configure(EntityTypeBuilder<WorkingDay> builder)
        {
            builder.ToTable("DIADETRABALHO");

            builder.HasKey(x => x.WorkingDayId);
            builder.Property(x => x.WorkingDayId).HasColumnName("ID_DIA_DE_TRABALHO").HasColumnType("INT");
            builder.Property(x => x.WorkshopId).HasColumnName("ID_OFICINA").HasColumnType("INT");
            builder.Property(x => x.Date).HasColumnName("DATA").HasColumnType("DATETIME");
            builder.Property(x => x.AvailableWorkload).HasColumnName("CARGA_disponivel").HasColumnType("INT");
        }
    }
}
