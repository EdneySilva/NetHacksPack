using NetHacksPack.Integration.Abstractions.IO;
using RabbitMQ.Client.Events;

namespace NetHacksPack.Integration.RabbitMQ.Providers
{
    public delegate IMessageReader MessageReaderProvider(BasicDeliverEventArgs eventArgs);
}
