using NetHacksPack.Integration.Abstractions;
using NetHacksPack.Integration.Abstractions.IO;
using NetHacksPack.Integration.RabbitMQ.IO;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetHacksPack.Integration.RabbitMQ.Handler
{
    public class RabbitHandlerContext
    {
        public string EventName { get; }
        public IMessageReader MessageReader { get; }
        public IMessageWriter<RabbitMessageBuilder> MessageWriteRules { get; }
        public IEventBusPublisher<RabbitMessageBuilder> Publisher { get; }
        public RabbitMessageBuilder MessageBuilder { get; }
        public string Body { get; }
    }
}
