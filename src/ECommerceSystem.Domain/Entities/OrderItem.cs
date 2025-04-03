using ECommerceSystem.Domain.ValueObjects;
using ECommerceSystem.Shared.Base;

namespace ECommerceSystem.Domain.Entities
{
    public class OrderItem : Aggregate<OrderItemId>
    {
        public Guid ProductId { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        public OrderId OrderId { get; private set; } = default!;

        internal OrderItem(OrderId orderId, Guid productId, int quantity, decimal price)
        {
            Id = OrderItemId.Of(Guid.NewGuid());
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }

        private OrderItem()
        {
            Id = OrderItemId.Of(Guid.NewGuid());
        }

        public void SetQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.");

            Quantity = quantity;
        }

    }
}
