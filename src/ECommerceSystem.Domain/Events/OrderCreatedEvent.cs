using ECommerceSystem.Shared.Events;

namespace ECommerceSystem.Domain.Events
{
    public class OrderCreatedEvent : IDomainEvent
    {
        public int OrderId { get; }
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
