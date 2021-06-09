using System.Threading.Tasks;

namespace NetHacksPack.Integration.Abstractions
{
    public interface IEventBusManager
    {
        Task HandleError(EventBusError busError);
    }
}
