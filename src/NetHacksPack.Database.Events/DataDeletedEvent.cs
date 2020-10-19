using NetHacksPack.Database.Events.Constants;

namespace NetHacksPack.Database.Events
{
    public class DataDeletedEvent<TData> : DataEventBase<TData>
    {
        public DataDeletedEvent(TData data) 
            : base(data, EventTypes.DELETE)
        {
        }
    }
}
