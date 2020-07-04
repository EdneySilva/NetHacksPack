using RabbitMQ.Client;
using System;

namespace NetHacksPack.Integration.RabbitMQ.IO
{
    public delegate void MessageStreamer(IModel model, Message message, string broker);

    public class MessageStream : IDisposable
    {
        private readonly string brokerName;
        private readonly IModel model;
        private readonly MessageStreamer messageWriting;
        private Message[] body;
        public Message[] Body => body;

        internal MessageStream(string brokerName, IModel model, MessageStreamer messageWriting)
        {
            this.brokerName = brokerName;
            this.model = model;
            body = new Message[0];
            this.messageWriting = messageWriting;
        }

        public void Write(byte[] message, IBasicProperties basicProperties, string eventName)
        {
            System.Array.Resize(ref body, body.Length + 1);
            body[body.Length - 1] = new Message(eventName, message, basicProperties);
            this.messageWriting(this.model, body[body.Length -1], this.brokerName);
        }

        public void Dispose()
        {
            Array.Clear(this.body, 0, this.body.Length);
            this.model.Close();
            this.model.Dispose();
        }
    }
}