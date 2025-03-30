using ECommerceSystem.EventBus.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ECommerceSystem.EventBus.RabbitMQ
{
    public class RabbitMQEventBus : IEventBus, IAsyncDisposable
    {
        private readonly RabbitMQPersistentConnection _persistentConnection;
        private readonly IServiceProvider _serviceProvider;
        private readonly string _exchange = "";
        private IChannel? _channel;

        public RabbitMQEventBus(RabbitMQPersistentConnection persistentConnection, IServiceProvider serviceProvider)
        {
            _persistentConnection = persistentConnection;
            _serviceProvider = serviceProvider;
        }

        private async Task<IChannel> GetOrCreateChannelAsync()
        {
            if (_channel is null)
            {
                var connection = await _persistentConnection.GetConnectionAsync();
                _channel = await connection.CreateChannelAsync();
                //await _channel.ExchangeDeclareAsync(_exchange, ExchangeType.Fanout);
            }

            return _channel;
        }

        public async Task PublishAsync(IntegrationEvent @event, string queueName)
        {
            var channel = await GetOrCreateChannelAsync();

            await channel.QueueDeclareAsync(queueName, durable: true, exclusive: false, autoDelete: false);

            var message = JsonSerializer.Serialize(@event, new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                WriteIndented = true
            });
            var body = Encoding.UTF8.GetBytes(message);

            await channel.BasicPublishAsync(
                exchange: _exchange,
                routingKey: queueName,
                body: body
            );
        }

        public async Task SubscribeAsync<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var channel = await GetOrCreateChannelAsync();
            var queueName = typeof(T).Name;

            await channel.QueueDeclareAsync(queueName, durable: true, exclusive: false, autoDelete: false);
            await channel.QueueBindAsync(queueName, _exchange, "");

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var @event = JsonSerializer.Deserialize<T>(message);

                if (@event != null)
                {
                    using var scope = _serviceProvider.CreateScope();
                    var handler = scope.ServiceProvider.GetRequiredService<TH>();
                    await handler.Handle(@event);
                }

                await channel.BasicAckAsync(ea.DeliveryTag, false);
            };

            await channel.BasicConsumeAsync(queue: queueName, autoAck: false, consumer: consumer);
        }

        public async Task UnsubscribeAsync<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var channel = await GetOrCreateChannelAsync();
            var queueName = typeof(T).Name;
            await channel.QueueDeleteAsync(queueName);
        }

        public async ValueTask DisposeAsync()
        {
            if (_channel != null)
            {
                await _channel.DisposeAsync();
            }

            await _persistentConnection.DisposeAsync();
        }
    }
}
