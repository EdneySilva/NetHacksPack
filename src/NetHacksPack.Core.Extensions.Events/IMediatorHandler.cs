using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetHacksPack.Core.Extensions.Events
{
    public interface IMediatorHandler
    {
        Task PublishEvent<TEvent>(TEvent @event) where TEvent : ObjectEvent;

        Task<bool> SendCommand<TCommand>(TCommand command) where TCommand : Command;
    }
}
