using NetHacksPack.Core;
using NetHacksPack.Integration.Abstractions;
using NetHacksPack.Integration.RabbitMQ.Handler;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace NetHacksPack.Integration.RabbitMQ
{
    class RabbitMQSubscription<T> : ISubscription<RabbitContext>, IRabbitMessageHandler
        where T : Event
    {
        private readonly Type subscriptionType;
        private readonly IServiceProvider serviceProvider;
        private readonly ExecutionPipe receiveExecutionPipe;

        public RabbitMQSubscription(Type subscriptionType, IServiceProvider serviceProvider, RabbitPipeline rabbitPipeline)
        {
            this.subscriptionType = subscriptionType;
            this.serviceProvider = serviceProvider;
            this.receiveExecutionPipe = rabbitPipeline
                .Describe<T>()
                .Use(this)
                .Build(HandlerType.Receive);
        }

        public Task Handler(RabbitContext rabbitContext, EventMessage eventMessage)
        {
            return Task.Run(async () =>
            {
                using (rabbitContext)
                {
                    await this.receiveExecutionPipe.Execute(rabbitContext, eventMessage);
                }
            });
        }

        public Task HandlePublish(RabbitContext rabbitContext, RabbitMessageHandler next)
        {
            return next();
        }

        public Task HandleReceive(RabbitContext rabbitContext, RabbitMessageHandler next)
        {
            var service = rabbitContext.ContextServiceProvider.GetRequiredService(subscriptionType);
            var @event = rabbitContext.ReceivedMessage.ToEventMessage().MessageReader.Read<T>();
            return ((IEventHandler<T>)service).Handle(@event);
        }
    }
}