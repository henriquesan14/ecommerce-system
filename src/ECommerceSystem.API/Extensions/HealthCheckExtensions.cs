using RabbitMQ.Client;

namespace ECommerceSystem.API.Extensions
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddHealthChecksConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddNpgSql(configuration.GetConnectionString("DbConnection")!)
                .AddRabbitMQ(sp =>
                {
                    var hostName = configuration["MessageBus:HostName"] ?? "localhost";

                    var factory = new ConnectionFactory
                    {
                        HostName = hostName
                    };

                    return factory.CreateConnectionAsync(); // Criando a conexão dentro da factory
                }, name: "rabbitmq");

            return services;
        }
    }
}
