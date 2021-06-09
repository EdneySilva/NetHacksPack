using System;
using System.Threading.Tasks;

namespace NetHacksPack.Core
{
    public interface IMediatorHandler
    {
        Task PublishEvent<TEvent>(TEvent @event) where TEvent : ObjectEvent;

        [Obsolete("this method will be removed in the next versions, use the method SendCommand<TResult>(Command<TResult> command) that enables you define your result")]
        Task<bool> SendCommand<TCommand>(TCommand command) where TCommand : Command;

        Task<TResult> SendCommand<TResult>(Command<TResult> command);
    }
}
