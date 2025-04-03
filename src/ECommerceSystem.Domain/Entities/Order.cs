using ECommerceSystem.Domain.Enums;
using ECommerceSystem.Domain.Events;
using ECommerceSystem.Domain.ValueObjects;
using ECommerceSystem.Shared.Base;

namespace ECommerceSystem.Domain.Entities
{
    public class Order : Aggregate<OrderId>
    {
        public OrderStatusEnum Status { get; private set; }
        public Guid CustomerId { get; private set; }
        public decimal Total
        {
            get => Items.Sum(x => x.Price * x.Quantity);
            private set { }
        }

        private readonly List<OrderItem> _Items = new();
        public IReadOnlyList<OrderItem> Items => _Items.AsReadOnly();

        private Order()
        {
            Id = OrderId.Of(Guid.NewGuid());
            _Items = new List<OrderItem>();
        }

        public Order(List<OrderItem> itens, Guid customerId)
        {
            _Items = itens;
            CustomerId = customerId;
            CreatedAt = DateTime.Now;
            Status = OrderStatusEnum.PENDING;
        }

        public static Order Create(OrderId id, Guid customerId)
        {
            var order = new Order
            {
                Id = id,
                CustomerId = customerId,
                Status = OrderStatusEnum.PENDING
            };

            order.AddDomainEvent(new OrderCreatedEvent(order));

            return order;
        }

        public void Update(Guid customerId, OrderStatusEnum status)
        {
            CustomerId = customerId;
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

        public void AddItem(Guid productId, int quantity, decimal price)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            var orderItem = new OrderItem(Id, productId, quantity, price);
            _Items.Add(orderItem);
        }

        public void RemoveItem(Guid productId)
        {
            var orderItem = _Items.FirstOrDefault(x => x.ProductId == productId);
            if (orderItem is not null)
            {
                _Items.Remove(orderItem);
            }
        }
    }
}
