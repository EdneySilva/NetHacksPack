using Azure.Messaging.ServiceBus.Administration;
using Microsoft.Extensions.DependencyInjection;
using NetHacksPack.Core;
using NetHacksPack.Integration.Azure.Factories;
using System;
using System.Collections.Generic;


namespace NetHacksPack.Integration.Azure
{
    public class SubscriptionManager
    {
        private readonly AzureConnectionFactory azureConnectionFactory;
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly ServiceBusAdministrationClient azureServiceBusAdministrative;
        private readonly List<string> subscriptions = new List<string>();

        public SubscriptionManager(AzureConnectionFactory azureConnectionFactory, IServiceScopeFactory serviceScopeFactory)
        {
            this.azureConnectionFactory = azureConnectionFactory;
            this.serviceScopeFactory = serviceScopeFactory;
            this.azureServiceBusAdministrative = this.azureConnectionFactory.CreateAdminstrativeConnection();
        }

        public EventSubscription Subscribe<T, TEventHandler>() where T : Event
        {
            string eventName = Event.GetEventName<T>();
            subscriptions.Add(eventName);
            var processor = azureConnectionFactory.CreateClientProcessor(eventName);
            var subscription = new EventSubscription(eventName, serviceScopeFactory, typeof(TEventHandler), typeof(T));
            return subscription;
        }

        public void Unsubscribe()
        {
        }

        public void GetSubscriptionScope()
        {
            var scope = serviceScopeFactory.CreateScope();
            scope.ServiceProvider.GetService<IDictionary<string, Type>>();
        }
    }
}
