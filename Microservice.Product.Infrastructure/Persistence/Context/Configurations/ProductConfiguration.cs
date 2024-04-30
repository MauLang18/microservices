using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entity = Microservice.Product.Domain.Entities;

namespace Microservice.Product.Infrastructure.Persistence.Context.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Entity.Product>
{
    public void Configure(EntityTypeBuilder<Entity.Product> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("ProductId");

        builder.Property(x => x.Code)
            .HasMaxLength(15);

        builder.HasIndex(x => x.Code)
            .IsUnique();

        builder.Property(x => x.Name)
            .HasMaxLength(25);

        builder.Property(x => x.UnitSalePrice)
            .HasPrecision(10,2);
    }
}