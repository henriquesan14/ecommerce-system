namespace ECommerceSystem.EventBus.Abstractions
{
    public interface IEventBus
    {
        Task PublishAsync(IntegrationEvent @event, string queueName);
        Task SubscribeAsync<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;
        Task UnsubscribeAsync<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;
    }
}
