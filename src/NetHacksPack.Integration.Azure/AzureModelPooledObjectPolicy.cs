using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using NetHacksPack.Integration.Azure.Factories;
using System;
using System.Threading.Tasks;

namespace NetHacksPack.Integration.Azure
{
    public class AzureModelPooledObjectPolicy : IPooledObjectPolicy<ServiceBusSender>, IAsyncDisposable
    {
        private ServiceBusClient client;
        private readonly string topic;
        private readonly AzureConnectionFactory azureConnectionFactory;
        private readonly IOptions<AzureConnectionOptions> connnectionOptions;
        private static object @locker = new object();

        public AzureModelPooledObjectPolicy(string topic, AzureConnectionFactory azureConnectionFactory, IOptions<AzureConnectionOptions> connnectionOptions)
        {
            this.topic = topic;
            this.azureConnectionFactory = azureConnectionFactory;
            this.client = this.CreateConnection();
            this.connnectionOptions = connnectionOptions;
        }

        private ServiceBusClient CreateConnection()
        {
            return azureConnectionFactory.CreateClientConnection();
        }

        public ServiceBusSender Create()
        {
            lock (locker)
            {
                if (this.client.IsClosed)
                    this.client = this.CreateConnection();
            }

            var sender = client.CreateSender(topic);
            return sender;
        }

        public bool Return(ServiceBusSender obj)
        {
            if (obj == null)
                return false;
            if (!obj.IsClosed)
                return true;
            obj.DisposeAsync().GetAwaiter().GetResult();
            return false;
        }

        public ValueTask DisposeAsync()
        {
            if (this.client != null)
                return this.client.DisposeAsync();
            return new ValueTask(Task.CompletedTask);
        }
    }
}
