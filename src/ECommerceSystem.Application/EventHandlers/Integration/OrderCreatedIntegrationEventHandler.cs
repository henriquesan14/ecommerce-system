using ECommerceSystem.EventBus.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace ECommerceSystem.Application.EventHandlers.Integration
{
    public class OrderCreatedIntegrationEventHandler(ILogger<OrderCreatedIntegrationEventHandler> logger) : IConsumer<OrderCreatedIntegrationEvent>
    {
        public Task Consume(ConsumeContext<OrderCreatedIntegrationEvent> context)
        {
            logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);
            return Task.CompletedTask;
        }
    }
}
