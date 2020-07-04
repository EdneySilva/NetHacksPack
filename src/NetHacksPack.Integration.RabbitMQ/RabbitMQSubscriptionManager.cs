using NetHacksPack.Core;
using NetHacksPack.Integration.Abstractions;
using NetHacksPack.Integration.RabbitMQ.Handler;
using NetHacksPack.Integration.RabbitMQ.IO;
using NetHacksPack.Integration.RabbitMQ.Providers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace NetHacksPack.Integration.RabbitMQ
{
    class RabbitMQSubscriptionManager : ISubscriptionManager<RabbitContext>
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IEventBusPublisher eventBusPublisher;
        private readonly MessageWriterProvider messageWriterProvider;
        private readonly IDictionary<string, ISubscription<RabbitContext>> subscriptions = new Dictionary<string, ISubscription<RabbitContext>>();
        
        public RabbitMQSubscriptionManager(
            IServiceProvider serviceProvider, 
            IEventBusPublisher eventBusPublisher, 
            MessageWriterProvider messageWriterProvider
        )
        {
            this.serviceProvider = serviceProvider;
            this.eventBusPublisher = eventBusPublisher;
            this.messageWriterProvider = messageWriterProvider;
        }

        public void AddSubscription<TEventDesc, TEventHandler>()
            where TEventDesc : Event
            where TEventHandler : IEventHandler<TEventDesc>
        {
            var type = typeof(TEventHandler);
            this.subscriptions.Add(
                this.GetEventKey<TEventDesc>(), 
                new RabbitMQSubscription<TEventDesc>(
                    type, 
                    this.serviceProvider,
                    this.serviceProvider.GetService<RabbitPipeline>()
                )
            );
        }

        public string GetEventKey<T>() where T : Event
        {
            return Event.GetEventName<T>();
        }

        public bool IsSubscribedOn(string eventName)
        {
            return this.subscriptions.ContainsKey(eventName);
        }

        public ISubscription<RabbitContext> GetSubscription(string eventName)
        {
            if (this.subscriptions.ContainsKey(eventName))
            {
                var subscription = this.subscriptions[eventName];
                return subscription;
            }
            if (this.subscriptions.ContainsKey(string.Empty))
                return this.subscriptions[string.Empty];
            return null;
        }
    }
}
