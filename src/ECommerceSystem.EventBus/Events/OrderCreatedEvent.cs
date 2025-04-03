using ECommerceSystem.EventBus.Abstractions;

namespace ECommerceSystem.EventBus.Events
{
    public class OrderCreatedEvent : IntegrationEvent
    {
        public Guid OrderId { get; set; }
        public decimal TotalAmount { get; set; }

        public OrderCreatedEvent(Guid orderId, decimal totalAmount)
        {
            OrderId = orderId;
            TotalAmount = totalAmount;
        }
    }
}
