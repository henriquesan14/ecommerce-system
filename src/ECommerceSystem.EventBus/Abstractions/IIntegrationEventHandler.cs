namespace ECommerceSystem.EventBus.Abstractions
{
    public interface IIntegrationEventHandler<TEvent> where TEvent : IntegrationEvent
    {
        Task Handle(TEvent @event);
    }
}
