using NetHacksPack.Database.Events.Constants;

namespace NetHacksPack.Database.Events
{
    public class DataAddedEvent<TData> : DataEventBase<TData>
    {
        public DataAddedEvent(TData data)
            : base(data, EventTypes.INSERT)
        {
        }
    }
}
