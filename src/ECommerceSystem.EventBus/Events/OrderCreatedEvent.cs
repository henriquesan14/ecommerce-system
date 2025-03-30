using ECommerceSystem.EventBus.Abstractions;

namespace ECommerceSystem.EventBus.Events
{
    public class OrderCreatedEvent : IntegrationEvent
    {
        public int OrderId { get; set; }
        public decimal TotalAmount { get; set; }

        public OrderCreatedEvent(int orderId, decimal totalAmount)
        {
            OrderId = orderId;
            TotalAmount = totalAmount;
        }
    }
}
