using NetHacksPack.Database.Events.Constants;
using System;

namespace NetHacksPack.Database.Events
{
    public class DataUpdatedEvent<TData> : DataEventBase<TData>
    {
        public DataUpdatedEvent(TData data)
            : base(data, EventTypes.UPDATE)
        {
        }
    }
}
