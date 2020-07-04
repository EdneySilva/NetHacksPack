using NetHacksPack.Core;
using NetHacksPack.Integration.Abstractions;
using NetHacksPack.Integration.RabbitMQ.Handler;
using NetHacksPack.Integration.RabbitMQ.IO;
using NetHacksPack.Integration.RabbitMQ.Providers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NetHacksPack.Integration.RabbitMQ
{
    class RabbitMQPublisher : IEventBusPublisher
    {
        private readonly ILogger<RabbitMQPublisher> logger;
        private readonly IServiceProvider serviceProvider;
        private readonly IConnectionProvider<RabbitMQConnection> connectionProvider;
        private readonly MessageWriterProvider messageWriterProvider;
        private readonly RabbitMessageSerializer rabbitMessageSerializer;
        private readonly RabbitPipeline pipeline;

        public RabbitMQPublisher(
            IConnectionProvider<RabbitMQConnection> connectionProvider, 
            MessageWriterProvider messageWriterProvider, 
            RabbitMessageSerializer rabbitMessageSerializer,
            RabbitPipeline pipeline,
            ILogger<RabbitMQPublisher> logger,
            IServiceProvider serviceProvider
        )
        {
            this.connectionProvider = connectionProvider;
            this.messageWriterProvider = messageWriterProvider;
            this.rabbitMessageSerializer = rabbitMessageSerializer;
            this.pipeline = pipeline;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }


        public void Send(Event @event, CancellationToken cancellationToken)
        {
            var eventPipeline = pipeline.Describe(@event.GetType().Name).Build(HandlerType.Publish);

            var connection = this.connectionProvider.GetConnection();
            using (var channel = connection.Connect(cancellationToken, (channel, connectionInfo) =>
            {
                channel.BasicReturn += Channel_BasicReturn;
            }))
            {
                try
                {
                    var stream = this.CreateStream(channel, connection);
                    var context = this.CreateContext(@event, channel, stream);

                    eventPipeline?.Execute(context, new EventMessage());
                    if (stream.Body.Length == 0)
                    {
                        context.WriteMessageToPublish(Encoding.UTF8.GetBytes(rabbitMessageSerializer(@event)), GetBasicProperties(channel, @event));
                        return;
                    }
                }
                catch (Exception ex)
                {
                    this.logger.LogError(ex, $"An error occurred when try to publish the event {@event.EventName}");
                }
            }
        }
        
        private MessageStream CreateStream(IModel channel, RabbitMQConnection connection)
        {
            return new MessageStream(connection.Options.BrokerName, channel, (model, message, broaker) =>
            {
                model.BasicPublish(broaker, message.EventName, message.BasicProperties, message.Body);
            });
        }

        private RabbitContext CreateContext(Event @event, IModel channel, MessageStream messageStream)
        {
            var context = new RabbitContext(
                @event.EventName,
                rabbitMessageSerializer,
                channel,
                messageStream,
                serviceProvider.CreateScope()
            );
            
            return context;
        }

        private void Channel_BasicReturn(object sender, BasicReturnEventArgs e)
        {
            this.logger.LogError($"The event {e.RoutingKey} can not be published.\n Response: {e.ReplyText}");
        }

        private IBasicProperties GetBasicProperties(IModel channel, Event eventDescriptor)
        {
            var properties = channel.CreateBasicProperties();
            properties.AppId = string.Empty;//applicationIdResolver?.Invoke();
            properties.MessageId = Guid.NewGuid().ToString("N");
            properties.Headers = new Dictionary<string, object>();
            properties.Priority = eventDescriptor.DefinePriority();
            return properties;
        }
    }
}
