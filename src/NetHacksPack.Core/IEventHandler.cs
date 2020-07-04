using System;
using System.Threading.Tasks;

namespace NetHacksPack.Core
{
    public interface IEventHandler
    {
    }

    public interface IEventHandler<TEventDesc> : IEventHandler
    where TEventDesc : Event
    {
        Task Handle(TEventDesc @event);
    }

}
