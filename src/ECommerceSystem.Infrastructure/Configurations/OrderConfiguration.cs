using ECommerceSystem.Domain.Entities;
using ECommerceSystem.Domain.Enums;
using ECommerceSystem.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceSystem.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id).HasConversion(
                            orderId => orderId.Value,
                            dbId => OrderId.Of(dbId));


            builder.HasMany(o => o.Items)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId);


            builder.Property(o => o.Status)
                .HasDefaultValue(OrderStatusEnum.PENDING)
                .HasConversion(
                    s => s.ToString(),
                    dbStatus => (OrderStatusEnum)Enum.Parse(typeof(OrderStatusEnum), dbStatus));

            builder.Property(o => o.Total);
        }
    }
}
