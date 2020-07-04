using NetHacksPack.Integration.Abstractions;
using NetHacksPack.Integration.RabbitMQ.IO;
using NetHacksPack.Integration.RabbitMQ.Providers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Threading;

namespace NetHacksPack.Integration.RabbitMQ
{
    class RabbitMQListener : IEventBusListener<RabbitContext>
    {
        private readonly RabbitMQConnection rabbitConnection;
        private readonly IServiceProvider serviceprovider;
        private CancellationToken cancellationToken;
        private MessageReaderProvider messageReaderProvider;
        private readonly RabbitMessageSerializer rabbitMessageSerializer;
        private ILogger<ISubscription> logger;
        private IModel currentChannel;
        private EventingBasicConsumer currentConsumer;

        public event OnMessageReceivedHandler<RabbitContext> OnMessageReceived;

        public RabbitMQListener(
            IConnectionProvider<RabbitMQConnection> connectionProvider,
            IServiceProvider serviceprovider,
            ILogger<ISubscription> logger,
            MessageReaderProvider messageReaderProvider,
            RabbitMessageSerializer rabbitMessageSerializer
        )
        {
            this.rabbitConnection = connectionProvider.GetConnection(null);
            this.serviceprovider = serviceprovider;
            this.logger = logger;
            this.messageReaderProvider = messageReaderProvider;
            this.rabbitMessageSerializer = rabbitMessageSerializer;
        }

        public void StartListener(CancellationToken cancellationToken)
        {
            this.cancellationToken = cancellationToken;
            try
            {
                this.rabbitConnection.OnReconnected += ConnectionFactory_Reconnected;
                this.CreateConsumer();
            }
            catch (Exception ex)
            {
                this.logger.LogInformation("An error occurred when try to connect EventBus the reconnection attemp will started: \n " + ex);
                InternalConsummer_ConsumerCancelled(this, new ConsumerEventArgs(new[] { string.Empty }));
            }
        }

        private void CreateConsumer()
        {
            this.currentChannel = this.rabbitConnection.Connect(this.cancellationToken, (channel, connectionInfo) =>
            {
                this.currentConsumer = new EventingBasicConsumer(channel);
                this.currentConsumer.Received += InternalConsummer_Received;
                this.currentConsumer.ConsumerCancelled += InternalConsummer_ConsumerCancelled;
                channel.BasicConsume(queue: connectionInfo.Queue, false, this.currentConsumer);
            });
        }

        #region Events
        private void ConnectionFactory_Reconnected(object sender, EventArgs e)
        {
            this.CreateConsumer();
        }

        private void InternalConsummer_Received(object sender, BasicDeliverEventArgs @event)
        {
            try
            {
                var eventHandler = this.OnMessageReceived;
                if (eventHandler != null)
                    eventHandler(
                        this,
                        new RabbitContext(
                            @event.RoutingKey,
                            rabbitMessageSerializer,
                            this.GetMessage(@event),
                            @event.BasicProperties,
                            this.GetMessageStream(@event),
                            this.serviceprovider.CreateScope()
                        )
                    );
                this.currentChannel.BasicAck(@event.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                this.HandleMessageReceivedException(ex, @event);
            }
        }
        
        private Message GetMessage(BasicDeliverEventArgs eventArgs)
        {
            return new Message(
                        eventArgs.RoutingKey,
                        eventArgs.Body.ToArray(),
                        eventArgs.BasicProperties,
                        this.messageReaderProvider(eventArgs)
                    );
        }

        private MessageStream GetMessageStream(BasicDeliverEventArgs eventArgs)
        {
            var channel = this.rabbitConnection.Connect(this.cancellationToken, (channel, connectionInfo) => { });
            return new MessageStream(eventArgs.Exchange, this.currentChannel, RabbitMQListener.MessageStreamer);
        }

        private static void MessageStreamer(IModel model, IO.Message message, string broaker)
        {
            model.BasicPublish(broaker, message.EventName, message.BasicProperties, message.Body);
        }

        private void HandleMessageReceivedException(Exception ex, BasicDeliverEventArgs @event)
        {
            this.logger.LogError(ex, "An error occurred when consume a message, this message will be requeued");
            ulong requeueCount = @event.DeliveryTag + 1;
            try
            {
                bool requeue = true;
                requeue = requeueCount <= 3;                
                this.currentChannel.BasicNack(@event.DeliveryTag, false, requeue);
                if (!requeue)
                    this.logger.LogCritical($"A message was discarted after {requeueCount} tentatives to proccess failed");
            }
            catch (Exception)
            {
                this.logger.LogCritical(ex, $"An error occurred when tried to requeue the message");
            }
        }

        private void InternalConsummer_ConsumerCancelled(object sender, ConsumerEventArgs e)
        {
            if (this.currentConsumer != null)
            {
                this.currentConsumer.Received -= InternalConsummer_Received;
                this.currentConsumer.ConsumerCancelled -= InternalConsummer_ConsumerCancelled;
            }

            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }
            this.rabbitConnection.Reconnect();
        }
        #endregion Events
    }
}
