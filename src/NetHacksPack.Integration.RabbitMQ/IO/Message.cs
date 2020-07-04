using NetHacksPack.Integration.Abstractions;
using NetHacksPack.Integration.Abstractions.IO;
using RabbitMQ.Client;

namespace NetHacksPack.Integration.RabbitMQ.IO
{
    public class Message
    {
        private readonly IMessageReader messageReader;

        internal Message(string eventName, byte[] body, IBasicProperties basicProperties)
        {
            this.Body = body;
            this.BasicProperties = basicProperties;
            this.messageReader = null;
            this.EventName = eventName;
        }

        internal Message(string eventName, byte[] body, IBasicProperties basicProperties, IMessageReader messageReader)
            : this(eventName, body, basicProperties)
        {
            this.messageReader = messageReader;
        }

        public IBasicProperties BasicProperties { get; }
        public byte[] Body { get; set; }
        public string EventName { get; }
        
        public T ToEvent<T>()
        {
            if (this.messageReader == null)
                return default;
            return this.messageReader.Read<T>();
        }

        public EventMessage ToEventMessage()
        {
            if (this.messageReader == null)
                return default;
            return new EventMessage(this.EventName, this.messageReader);
        }
    }
}