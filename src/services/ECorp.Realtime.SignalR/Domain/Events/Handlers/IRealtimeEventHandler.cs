using ECorp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECorp.Realtime.SignalR.Events.Handlers
{
    interface IRealtimeEventHandler : IEventHandler<RealtimeEventDescriptor>
    {
    }
}
