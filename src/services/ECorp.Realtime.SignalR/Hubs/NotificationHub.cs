using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECorp.Realtime.SignalR.Hubs
{
    public class NotificationHub : Hub
    {
        public NotificationHub()
        {
        }

        public override async Task OnConnectedAsync()
        {

            if (this.Context.GetHttpContext().Request.RouteValues.ContainsKey("eventName"))
            {
                await this.Groups.AddToGroupAsync(Context.ConnectionId, this.Context.GetHttpContext().Request.RouteValues["eventName"].ToString());
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (this.Context.GetHttpContext().Request.RouteValues.ContainsKey("eventName"))
            {
                await this.Groups.RemoveFromGroupAsync(Context.ConnectionId, this.Context.GetHttpContext().Request.RouteValues["eventName"].ToString());
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
