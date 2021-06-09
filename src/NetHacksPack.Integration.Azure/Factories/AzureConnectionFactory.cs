using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using Microsoft.Extensions.Options;

namespace NetHacksPack.Integration.Azure.Factories
{
    public class AzureConnectionFactory
    {
        private readonly IOptions<AzureConnectionOptions> azureOptions;

        public AzureConnectionFactory(IOptions<AzureConnectionOptions> azureOptions)
        {
            this.azureOptions = azureOptions;
        }

        public ServiceBusClient CreateClientConnection()
        {
            var client = new ServiceBusClient(azureOptions.Value.ConnectionString);
            return client;
        }

        public ServiceBusProcessor CreateClientProcessor(string eventName)
        {
            var options = new ServiceBusProcessorOptions
            {
                AutoCompleteMessages = azureOptions.Value.AutoComplete ?? false,
                MaxConcurrentCalls = azureOptions.Value.MaxConcurrent ?? 5
            };
            var connection = this.CreateClientConnection();
            var processor = connection.CreateProcessor(eventName, azureOptions.Value.Queue, options);
            return processor;
        }

        public ServiceBusAdministrationClient CreateAdminstrativeConnection()
        {
            ServiceBusAdministrationClient serviceBusAdministrationClient = new ServiceBusAdministrationClient(azureOptions.Value.ConnectionString);
            return serviceBusAdministrationClient;
        }
    }
}
