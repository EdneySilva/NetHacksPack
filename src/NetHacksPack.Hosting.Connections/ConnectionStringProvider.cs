using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetHacksPack.Hosting.Abstractions.Providers;
using NetHacksPack.Hosting.Environment;

namespace NetHacksPack.Hosting.Connections
{
    class ConnectionStringProvider : IConnectionStringProvider
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<ConnectionStringProvider> logger;

        public ConnectionStringProvider(IConfiguration configuration, ILogger<ConnectionStringProvider> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        public string GetConnectionString(string connectionKey)
        {
            var value = this.configuration.GetConnectionString(connectionKey);
            if (!string.IsNullOrEmpty(value?.Trim()))
                return value;
            value = connectionKey.GetString(string.Empty);
            if(string.IsNullOrEmpty(value?.Trim()))
                logger?.LogError($"An error occurred to build the connection string with key [{connectionKey}]");
            return value;
        }

        public TConnectorOptions GetConnectorOptions<TConnectorOptions>(string connectionKey)
            where TConnectorOptions : new()
        {
            if (connectionKey.TryGetJson(out TConnectorOptions myConnectorOptions))
                return myConnectorOptions;

            object connectorOptions = new TConnectorOptions();
            var value = configuration.GetSection(connectionKey);
            if (value != null)
            {
                configuration.Bind(connectionKey, connectorOptions);
                return (TConnectorOptions)connectorOptions;
            }
            logger?.LogError($"An error occurred to build a connector by a connection string with key [{connectionKey}]");
            return default(TConnectorOptions);
        }
    }
}
