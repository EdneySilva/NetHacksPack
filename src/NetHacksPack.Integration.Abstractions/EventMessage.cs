using NetHacksPack.Integration.Abstractions.IO;

namespace NetHacksPack.Integration.Abstractions
{
    public struct EventMessage
    {
        public EventMessage(string eventName, IMessageReader messageReader)
        {
            EventName = eventName;
            MessageReader = messageReader;
        }

        public string EventName { get; }
        public IMessageReader MessageReader { get; }
    }
}
