using NetHacksPack.Core;
using NetHacksPack.Integration.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace NetHacksPack.Integration.Azure
{
    public class AzureServiceBus : IEventBus
    {
        private readonly AzureServiceBusPublisher publisher;
        private readonly AzureServiceBusConsumer consumer;
        private readonly SubscriptionManager subscriptionManager;

        public AzureServiceBus(AzureServiceBusPublisher publisher, AzureServiceBusConsumer consumer, SubscriptionManager subscriptionManager)
        {
            this.publisher = publisher;
            this.consumer = consumer;
            this.subscriptionManager = subscriptionManager;
        }

        public void Connect(CancellationToken cancellationToken)
        {
            Task.WaitAll(this.consumer.Connect());
        }

        public Task ConnectAsync(CancellationToken cancellationToken)
        {
            return consumer.Connect();
        }

        public Task PublishAsync<T>(T @event) where T : Event
        {
            return publisher.Send(string.Empty, @event);
        }

        public void Publish<T>(T @event) where T : Event
        {
            Task.WaitAll(this.PublishAsync(@event));
        }

        public void Subscribe<TEventDesc, TEventHandler>()
            where TEventDesc : Event
            where TEventHandler : IEventHandler<TEventDesc>
        {
            var subscription = subscriptionManager.Subscribe<TEventDesc, TEventHandler>();
            consumer.UseSubscription(subscription);
        }
    }
}
