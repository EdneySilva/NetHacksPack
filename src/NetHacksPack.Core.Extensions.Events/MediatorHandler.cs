using MediatR;
using System.Threading.Tasks;

namespace NetHacksPack.Core
{
    class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator mediator;

        public MediatorHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task PublishEvent<TEvent>(TEvent @event) where TEvent : ObjectEvent
        {
            await mediator.Publish(@event);
            
        }

        public async Task<bool> SendCommand<TCommand>(TCommand command) where TCommand : Command
        {
            return await mediator.Send(command);
        }

        public Task<TResult> SendCommand<TResult>(Command<TResult> command)
        {
            return mediator.Send(command);
        }
    }
}
