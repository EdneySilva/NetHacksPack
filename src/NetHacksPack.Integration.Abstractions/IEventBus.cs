using NetHacksPack.Core;
using System.Threading;
using System.Threading.Tasks;

namespace NetHacksPack.Integration.Abstractions
{
    public interface IEventBus
    {
        void Connect(CancellationToken cancellationToken);

        Task ConnectAsync(CancellationToken cancellationToken);

        void Publish(Event @event);

        void Subscribe<TEventDesc, TEventHandler>()
            where TEventDesc : Event
            where TEventHandler : IEventHandler<TEventDesc>;
    }
}
