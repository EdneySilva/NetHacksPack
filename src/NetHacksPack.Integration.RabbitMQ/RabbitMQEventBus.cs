using NetHacksPack.Core;
using NetHacksPack.Integration.Abstractions;
using NetHacksPack.Integration.RabbitMQ.Handler;
using System.Threading;
using System.Threading.Tasks;

namespace NetHacksPack.Integration.RabbitMQ
{
    class RabbitMQEventBus : IEventBus
    {   
        private readonly ISubscriptionManager<RabbitContext> subscriptionManager;
        private readonly IEventBusListener<RabbitContext> eventBusListener;
        private readonly IEventBusPublisher eventBusPublisher;
        private CancellationToken cancellationToken;

        public RabbitMQEventBus(
            ISubscriptionManager<RabbitContext> subscriptionManager, 
            IEventBusListener<RabbitContext> eventBusListener,
            IEventBusPublisher eventBusPublisher
        )
        {
            this.subscriptionManager = subscriptionManager;
            this.eventBusListener = eventBusListener;
            this.eventBusPublisher = eventBusPublisher;
        }

        public void Connect(CancellationToken cancellationToken)
        {
            this.cancellationToken = cancellationToken;
            this.eventBusListener.OnMessageReceived += EventBusListener_OnMessageReceived;
            this.eventBusListener.StartListener(this.cancellationToken);
        }

        public Task ConnectAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                this.cancellationToken = cancellationToken;
                this.eventBusListener.OnMessageReceived += EventBusListener_OnMessageReceived;
                this.eventBusListener.StartListener(this.cancellationToken);
            });
        }

        private Task EventBusListener_OnMessageReceived(object sender, RabbitContext rabbitContext)
        {
            var subscription = this.subscriptionManager.GetSubscription(rabbitContext.EventName);
            if (subscription == null)
                return Task.CompletedTask;
            return subscription.Handler(rabbitContext, rabbitContext.ReceivedMessage.ToEventMessage());
        }

        public void Publish(Event @event)
        {
            this.eventBusPublisher.Send(@event, this.cancellationToken);
        }

        public void Subscribe<TEventDesc, TEventHandler>()
            where TEventDesc : Event
            where TEventHandler : IEventHandler<TEventDesc>
        {
            var eventName = this.subscriptionManager.GetEventKey<TEventDesc>();
            if (!this.subscriptionManager.IsSubscribedOn(eventName))
            {
                this.subscriptionManager.AddSubscription<TEventDesc, TEventHandler>();  
            }
        }
    }
}
