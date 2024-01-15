using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("CLIENTE");

            builder.HasKey(x => x.CustomerId);
            builder.Property(x => x.CustomerId).HasColumnName("ID_CLIENTE").HasColumnType("INT");
            builder.Property(x => x.Name).HasColumnName("NOME").HasColumnType("VARCHAR");
            builder.Property(x => x.CPF).HasColumnName("CPF").HasColumnType("VARCHAR");
        }
    }
}
