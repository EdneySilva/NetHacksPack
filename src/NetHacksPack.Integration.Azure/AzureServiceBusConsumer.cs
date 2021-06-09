using Azure.Messaging.ServiceBus;
using NetHacksPack.Integration.Abstractions;
using NetHacksPack.Integration.Azure.Factories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetHacksPack.Integration.Azure
{
    public class AzureServiceBusConsumer : IDisposable
    {
        private Dictionary<string, EventSubscription> subscriptions = new Dictionary<string, EventSubscription>();
        private readonly AzureConnectionFactory azureConnectionFactory;
        private readonly IEventBusManager eventBus;

        public AzureServiceBusConsumer(AzureConnectionFactory azureConnectionFactory, IEventBusManager eventBus)
        {
            this.azureConnectionFactory = azureConnectionFactory;
            this.eventBus = eventBus;
        }

        public Task Connect()
        {
            subscriptions = new Dictionary<string, EventSubscription>();
            return Task.CompletedTask;
        }

        private Task Processor_ProcessErrorAsync(ProcessErrorEventArgs arg)
        {
            return eventBus.HandleError(
                new EventBusError(
                    Guid.NewGuid(),
                    arg.EntityPath,
                    DateTime.Now,
                    new AggregateException(arg.Exception)
                )
            );
        }

        public void UseSubscription(EventSubscription subscription)
        {
            var processor = azureConnectionFactory.CreateClientProcessor(subscription.EventName);
            processor.ProcessErrorAsync += Processor_ProcessErrorAsync;
            subscription.Connect(processor);
            subscriptions.Add(subscription.EventName, subscription);
            Task.WaitAll(processor.StartProcessingAsync());
        }

        public void Dispose()
        {
        }
    }
}
