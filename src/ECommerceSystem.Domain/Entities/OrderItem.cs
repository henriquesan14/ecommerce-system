using ECommerceSystem.Domain.ValueObjects;
using ECommerceSystem.Shared.Base;

namespace ECommerceSystem.Domain.Entities
{
    public class OrderItem : Aggregate<OrderItemId>
    {
        public ProductId ProductId { get; private set; } = default!;
        public decimal Price { get; private set; } = default!;
        public int Quantity { get; private set; } = default!;
        public OrderId OrderId { get; private set; } = default!;

        internal OrderItem(OrderId orderId, ProductId productId, int quantity, decimal price)
        {
            Id = OrderItemId.Of(Guid.NewGuid());
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }
    }
}
