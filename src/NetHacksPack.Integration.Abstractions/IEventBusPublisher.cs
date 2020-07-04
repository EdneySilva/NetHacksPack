using NetHacksPack.Core;
using System.Threading;

namespace NetHacksPack.Integration.Abstractions
{
    public interface IEventBusPublisher
    {
        void Send(Event @event, CancellationToken cancellationToken);
    }

    public interface IEventBusPublisher<TContext> : IEventBusPublisher
    {
        void Send(TContext context, Event @event, CancellationToken cancellationToken);
    }
}
