using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace NetHacksPack.Integration.Azure
{
    public struct EventSubscription
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly Type eventHandlerType;
        private readonly Type eventHandlerDescType;
        private readonly MethodInfo handlerMethodInfo;
        public string EventName { get; }

        public EventSubscription(string eventName, IServiceScopeFactory serviceScopeFactory, Type eventHandlerType, Type eventHandlerDescType)
        {
            EventName = eventName;
            this.serviceScopeFactory = serviceScopeFactory;
            this.eventHandlerType = eventHandlerType;
            this.eventHandlerDescType = eventHandlerDescType;
            this.handlerMethodInfo = GetHandlerMethodInfo(eventHandlerType, eventHandlerDescType);
        }

        private static MethodInfo GetHandlerMethodInfo(Type eventHandlerType, Type eventHandlerDescType)
        {
            var type = eventHandlerType;
            var paramType = eventHandlerDescType;
            var method = type.GetMethod("Handle");
            return method;
        }

        internal void Connect(ServiceBusProcessor serviceBusProcessor)
        {
            serviceBusProcessor.ProcessMessageAsync += Processor_ProcessMessageAsync;
        }

        private async Task Processor_ProcessMessageAsync(ProcessMessageEventArgs arg)
        {
            var scope = this.serviceScopeFactory.CreateScope();
            try
            {
                await arg.CompleteMessageAsync(arg.Message);
                var handler = scope.ServiceProvider.GetRequiredService(eventHandlerType);
                var json = arg.Message.Body.ToString();
                var objeto = System.Text.Json.JsonSerializer.Deserialize(json, eventHandlerDescType);
                var task = (Task)handlerMethodInfo.Invoke(handler, new[] { objeto });
                await task;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                scope.Dispose();
            }
        }
    }
}
