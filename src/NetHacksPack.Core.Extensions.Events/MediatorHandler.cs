using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetHacksPack.Core.Extensions.Events
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
    }
}
