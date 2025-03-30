using RabbitMQ.Client;

namespace ECommerceSystem.EventBus.RabbitMQ
{
    public class RabbitMQPersistentConnection
    {
        private readonly IConnectionFactory _connectionFactory;
        private IConnection? _connection;
        private readonly SemaphoreSlim _connectionLock = new(1, 1);

        public RabbitMQPersistentConnection(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public bool IsConnected => _connection is { IsOpen: true };

        public async Task<IConnection> GetConnectionAsync()
        {
            if (!IsConnected)
                await TryConnectAsync();

            return _connection!;
        }

        public async ValueTask DisposeAsync()
        {
            if (_connection != null)
            {
                await _connection.DisposeAsync();
            }
        }

        private async Task TryConnectAsync()
        {
            await _connectionLock.WaitAsync();
            try
            {
                if (!IsConnected)
                {
                    _connection = await _connectionFactory.CreateConnectionAsync();
                }
            }
            finally
            {
                _connectionLock.Release();
            }
        }
    }
}
