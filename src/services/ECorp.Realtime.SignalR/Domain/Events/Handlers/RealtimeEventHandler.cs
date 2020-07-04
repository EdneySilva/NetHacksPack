using ECorp.Realtime.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace ECorp.Realtime.SignalR.Events.Handlers
{
    public class RealtimeEventHandler : IRealtimeEventHandler
    {
        private readonly IHubContext<NotificationHub> hubContext;
        public RealtimeEventHandler(IHubContext<NotificationHub> hubContext)
        {
            this.hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }

        public Task Handle(RealtimeEventDescriptor @event)
        {
            var data = @event;
            return hubContext.Clients.Group(@event.EventName).SendAsync("eventReceived", new
            {
                eventHandler = "RealtimeEventHandler"
            }, new
            {
                @event.EventName,
                @event,
                EventDate = DateTime.Now
                //@event.ApplicationId
            });
        }
    }
}
