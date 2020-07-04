using NetHacksPack.Core;
using NetHacksPack.Integration.Abstractions.IO;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetHacksPack.Integration.RabbitMQ.IO
{
    public delegate string RabbitMessageSerializer(Event @event);

    class MessageWriter : IMessageWriter<RabbitMessageBuilder>
    {
        private readonly IEnumerable<Action<IBasicProperties>> specifications;

        public MessageWriter(IEnumerable<Action<IBasicProperties>> specifications)
        {
            this.specifications = specifications;
        }

        internal static string EventStrigify(Event @event)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(@event);
        }

        public void Write(RabbitMessageBuilder publishChannel, Event data)
        {
            var message = SerializeEvent(data);
            var list = new List<Action<IBasicProperties>>(this.specifications);
            list.Add((properties) =>
            {
                properties.Priority = 5;
            });
            publishChannel.BuildMessage(message, list);
        }

        private byte[] SerializeEvent(Event @event)
        {
            var message = Newtonsoft.Json.JsonConvert.SerializeObject(@event);
            var data = Encoding.UTF8.GetBytes(message);
            return data;
        }
    }
}
