using NetHacksPack.Core;
using System.Threading;
using System.Threading.Tasks;

namespace NetHacksPack.Integration.Abstractions
{
    public interface IEventBus
    {
        void Connect(CancellationToken cancellationToken);

        Task ConnectAsync(CancellationToken cancellationToken);

        void Publish<T>(T @event) where T : Event;

        Task PublishAsync<T>(T @event) where T : Event;

        void Subscribe<TEventDesc, TEventHandler>()
            where TEventDesc : Event
            where TEventHandler : IEventHandler<TEventDesc>;
    }
}
