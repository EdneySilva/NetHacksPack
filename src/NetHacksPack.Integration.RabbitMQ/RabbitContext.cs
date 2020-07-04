using NetHacksPack.Integration.RabbitMQ.IO;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System;

namespace NetHacksPack.Integration.RabbitMQ
{
    public class RabbitContext : IDisposable
    {
        private readonly IModel currentChannel;
        private readonly MessageStream messageStream;
        private readonly IServiceScope serviceScope;

        public RabbitContext(string eventName, RabbitMessageSerializer serializer, IModel currentChannel, MessageStream messageStream, IServiceScope serviceScope)
        {
            this.EventName = eventName;
            this.Serializer = serializer;
            this.currentChannel = currentChannel;
            this.messageStream = messageStream;
            this.serviceScope = serviceScope;
            this.BasicPropertyCreator = () => currentChannel.CreateBasicProperties();
            this.ContextServiceProvider = serviceScope.ServiceProvider;
        }

        public RabbitContext(string eventName, RabbitMessageSerializer serializer, Message receivedMessage, IBasicProperties basicProperties, MessageStream messageStream, IServiceScope serviceScope)
        {
            this.EventName = eventName;
            this.Serializer = serializer;
            this.currentChannel = null;
            this.ReceivedMessage = receivedMessage;
            this.BasicPropertyCreator = () => basicProperties;
            this.messageStream = messageStream;
            this.serviceScope = serviceScope;
            this.ContextServiceProvider = serviceScope.ServiceProvider;
        }

        public IServiceProvider ContextServiceProvider { get; }

        public string EventName { get; }

        public RabbitMessageSerializer Serializer { get; }
        
        public Func<IBasicProperties> BasicPropertyCreator { get; }

        public string Message { get; set; }

        public Message ReceivedMessage { get; }

        public void Dispose()
        {
            this.messageStream.Dispose();
            this.serviceScope.Dispose();
            GC.Collect();
        }

        public void WriteMessageToPublish(byte[] message, IBasicProperties basicProperties)
        {
            this.messageStream.Write(message, basicProperties, this.EventName);
        }
    }
}