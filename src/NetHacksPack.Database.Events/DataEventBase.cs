using NetHacksPack.Core;

namespace NetHacksPack.Database.Events
{
    public abstract class DataEventBase<TData> : ObjectEvent
    {
        public DataEventBase(TData data, string type)
        {
            Data = data;
            Type = type;
        }

        public TData Data { get; }
        public string Type { get; }
    }
}
