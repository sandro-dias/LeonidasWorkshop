using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class WorkshopConfiguration : IEntityTypeConfiguration<Workshop>
    {
        public void Configure(EntityTypeBuilder<Workshop> builder)
        {
            builder.ToTable("OFICINA");

            builder.HasKey(x => x.WorkShopId);
            builder.Property(x => x.WorkShopId).HasColumnName("ID_OFICINA").HasColumnType("INT");
            builder.Property(x => x.Workload).HasColumnName("CARGA").HasColumnType("INT");
            builder.Property(x => x.Name).HasColumnName("NOME").HasColumnType("VARCHAR");
            builder.Property(x => x.CNPJ).HasColumnName("CNPJ").HasColumnType("VARCHAR");
            builder.Property(x => x.Password).HasColumnName("SENHA").HasColumnType("VARCHAR");
        }
    }
}
