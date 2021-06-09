using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.ObjectPool;
using NetHacksPack.Core;
using System.Text.Json;
using NetHacksPack.Integration.Azure.Factories;

namespace NetHacksPack.Integration.Azure
{
    public class AzureServiceBusPublisher
    {
        private readonly ServiceBusSenderPoolFactory serviceBusSenderPoolFactory;
        private readonly Dictionary<string, DefaultObjectPool<ServiceBusSender>> senderPool = new Dictionary<string, DefaultObjectPool<ServiceBusSender>>();
        private static object locker = new object();

        public AzureServiceBusPublisher(ServiceBusSenderPoolFactory serviceBusSenderPoolFactory)
        {
            this.senderPool = new Dictionary<string, DefaultObjectPool<ServiceBusSender>>();
            this.serviceBusSenderPoolFactory = serviceBusSenderPoolFactory;
        }

        public async Task Send<T>(string brokerName, T @event) where T : Event
        {
            ServiceBusSender serviceBusSender = null;
            try
            {
                serviceBusSender = this.GetOrCreatePool(@event.EventName).Get();
                var messageContent = JsonSerializer.Serialize(@event);
                var message = new ServiceBusMessage(messageContent);
                await serviceBusSender.SendMessageAsync(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                this.GetOrCreatePool(@event.EventName).Return(serviceBusSender);
            }
        }

        private DefaultObjectPool<ServiceBusSender> GetOrCreatePool(string eventName)
        {
            lock (locker)
            {
                if (!senderPool.ContainsKey(eventName))
                    senderPool.Add(eventName, serviceBusSenderPoolFactory.Create(eventName));
                return senderPool[eventName];
            }
        }
    }
}
