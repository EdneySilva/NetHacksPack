using NetHacksPack.Core;

namespace NetHacksPack.Integration.Abstractions.IO
{
    public interface IMessageWriter<TPublishChannel>
    {
        void Write(TPublishChannel publishChannel, Event @event);
    }
}
