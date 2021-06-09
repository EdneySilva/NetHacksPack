using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetHacksPack.Integration.Azure.Factories
{
    public class ServiceBusSenderPoolFactory : IDisposable
    {
        private readonly AzureConnectionFactory azureConnectionFactory;
        private readonly IOptions<AzureConnectionOptions> connnectionOptions;
        private readonly List<IAsyncDisposable> listIAsyncDisposable = new List<IAsyncDisposable>();

        public ServiceBusSenderPoolFactory(AzureConnectionFactory azureConnectionFactory, IOptions<AzureConnectionOptions> connnectionOptions)
        {
            this.azureConnectionFactory = azureConnectionFactory;
            this.connnectionOptions = connnectionOptions;
        }

        public DefaultObjectPool<ServiceBusSender> Create(string eventName, int maxinumNumber = 5)
        {
            var item = new AzureModelPooledObjectPolicy(eventName, azureConnectionFactory, connnectionOptions);
            listIAsyncDisposable.Add(item);
            return new DefaultObjectPool<ServiceBusSender>(item, maxinumNumber);
        }

        public void Dispose()
        {
            Task.WaitAll(listIAsyncDisposable.Select(s => s.DisposeAsync().AsTask()).ToArray());
        }
    }
}
