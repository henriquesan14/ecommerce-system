namespace ECommerceSystem.EventBus.Events
{
    public record OrderCreatedIntegrationEvent(Guid OrderId, decimal TotalAmount) : IntegrationEvent
    {
    }
}
