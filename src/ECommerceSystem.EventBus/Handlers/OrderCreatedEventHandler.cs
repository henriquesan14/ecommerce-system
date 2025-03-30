using ECommerceSystem.EventBus.Abstractions;
using ECommerceSystem.EventBus.Events;

namespace ECommerceSystem.EventBus.Handlers
{
    public class PedidoCriadoEventHandler : IIntegrationEventHandler<OrderCreatedEvent>
    {
        public Task Handle(OrderCreatedEvent @event)
        {
            Console.WriteLine($"📦 Pedido Criado: {@event.OrderId}");
            return Task.CompletedTask;
        }
    }
}
