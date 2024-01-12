using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class WorkshopConfiguration : IEntityTypeConfiguration<Workshop>
    {
        public void Configure(EntityTypeBuilder<Workshop> builder)
        {
            builder.ToTable("WORKSHOP");

            builder.HasKey(x => x.WorkShopId);
            builder.Property(x => x.WorkShopId).HasColumnName("OFICINA_ID").HasColumnType("INT").IsRequired();
            //TODO: configurar Workshop e as demais entidades
        }
    }
}
