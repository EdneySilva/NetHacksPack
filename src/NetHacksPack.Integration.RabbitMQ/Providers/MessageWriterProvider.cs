using NetHacksPack.Integration.Abstractions.IO;
using NetHacksPack.Integration.RabbitMQ.IO;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;

namespace NetHacksPack.Integration.RabbitMQ.Providers
{
    public delegate IMessageWriter<RabbitMessageBuilder> MessageWriterProvider(IEnumerable<Action<IBasicProperties>> specifications);
}
