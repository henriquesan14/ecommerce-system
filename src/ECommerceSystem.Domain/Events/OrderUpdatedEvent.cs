using ECommerceSystem.Domain.Entities;
using ECommerceSystem.Shared.Events;

namespace ECommerceSystem.Domain.Events
{
    public record OrderUpdatedEvent(Order order) : IDomainEvent;
}
