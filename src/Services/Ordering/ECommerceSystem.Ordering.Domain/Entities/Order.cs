using ECommerceSystem.Domain.Enums;
using ECommerceSystem.Domain.Events;
using ECommerceSystem.Domain.ValueObjects;
using ECommerceSystem.Shared.Base;

namespace ECommerceSystem.Domain.Entities
{
    public class Order : Aggregate<OrderId>
    {
        private readonly List<OrderItem> _Items = new();
        public IReadOnlyList<OrderItem> Items => _Items.AsReadOnly();

        public CustomerId CustomerId { get; private set; } = default!;
        public Address ShippingAddress { get; private set; } = default!;
        public Payment Payment { get; private set; } = default!;
        public OrderStatusEnum Status { get; private set; } = OrderStatusEnum.PENDING;
        public decimal Total
        {
            get => Items.Sum(x => x.Price * x.Quantity);
            private set { }
        }

        public static Order Create(OrderId id, CustomerId customerId, Address shippingAddress, Payment payment)
        {
            var order = new Order
            {
                Id = id,
                CustomerId = customerId,
                ShippingAddress = shippingAddress,
                Payment = payment,
                Status = OrderStatusEnum.PENDING
            };

            order.AddDomainEvent(new OrderCreatedEvent(order));

            return order;
        }

        public void Update(Address shippingAddress, Payment payment, OrderStatusEnum status)
        {
            ShippingAddress = shippingAddress;
            Payment = payment;
            Status = status;

            AddDomainEvent(new OrderUpdatedEvent(this));
        }

        // Métodos de domínio
        public void Confirm()
        {
            if (Status == OrderStatusEnum.CANCELED)
                throw new InvalidOperationException("Não é possível confirmar um pedido cancelado.");

            Status = OrderStatusEnum.CONFIRMED;
        }

        public void Cancel()
        {
            if (Status == OrderStatusEnum.CONFIRMED)
                throw new InvalidOperationException("Não é possível cancelar um pedido confirmado.");

            Status = OrderStatusEnum.CANCELED;
        }

        public void AddItem(ProductId productId, int quantity, decimal price)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            var orderItem = new OrderItem(Id, productId, quantity, price);
            _Items.Add(orderItem);
        }

        public void RemoveItem(ProductId productId)
        {
            var orderItem = _Items.FirstOrDefault(x => x.ProductId == productId);
            if (orderItem is not null)
            {
                _Items.Remove(orderItem);
            }
        }
    }
}
