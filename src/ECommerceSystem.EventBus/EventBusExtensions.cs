using ECommerceSystem.EventBus.Abstractions;
using ECommerceSystem.EventBus.RabbitMQ;
using RabbitMQ.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace ECommerceSystem.EventBus
{
    public static class EventBusExtensions
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMQConfig = configuration.GetSection("MessageBus");

            services.AddSingleton<IConnectionFactory, ConnectionFactory>(sp =>
            {
                return new ConnectionFactory()
                {
                    HostName = rabbitMQConfig["HostName"]!,
                };
            });

            services.AddSingleton<RabbitMQPersistentConnection>();
            services.AddSingleton<IEventBus, RabbitMQEventBus>();

            return services;
        }
    }
}
