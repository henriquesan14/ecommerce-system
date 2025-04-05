using ECommerceSystem.Domain.Entities;
using ECommerceSystem.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceSystem.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).HasConversion(
                            productId => productId.Value,
                            dbId => ProductId.Of(dbId));

            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
        }
    }
}
