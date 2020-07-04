using NetHacksPack.Hosting.Abstractions.Providers;
using NetHacksPack.Integration.Abstractions;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace NetHacksPack.Integration.RabbitMQ.Providers
{
    class RabbitMQConnectionProvider : IConnectionProvider<RabbitMQConnection>
    {
        private readonly IConnectionStringProvider connectionStringProvider;
        private readonly ConnectionOptions defaultConnection;
        private readonly ILogger<RabbitMQConnection> logger;

        public RabbitMQConnectionProvider(IConnectionStringProvider connectionStringProvider, ConnectionOptions defaultConnection, ILogger<RabbitMQConnection> logger)
        {
            this.connectionStringProvider = connectionStringProvider;
            this.defaultConnection = defaultConnection;
            this.logger = logger;
        }

        protected IConnectionFactory GetConnectionFactory(ConnectionOptions connectionInfo)
        {
            IConnectionFactory connectionFactory = new ConnectionFactory
            {
                HostName = connectionInfo.Host,
                Port = connectionInfo.Port,
                UserName = connectionInfo.UserName,
                Password = connectionInfo.Password
            };
            if (!string.IsNullOrEmpty(connectionInfo.VirtualHost?.Trim()))
                connectionFactory.VirtualHost = connectionInfo.VirtualHost;
            return connectionFactory;
        }

        public RabbitMQConnection GetConnection(string connectionString = null)
        {
            ConnectionOptions connectionInfo = null;
            if (!string.IsNullOrEmpty(connectionString))
            {
                connectionInfo = this.connectionStringProvider.GetConnectorOptions<ConnectionOptions>(connectionString);
            }
            else
                connectionInfo = new ConnectionOptions();
            connectionInfo = this.defaultConnection.CopyAndReplace(defaultConnection);
            return new RabbitMQConnection(connectionInfo, this.GetConnectionFactory(connectionInfo), this.logger);
        }
    }
}
