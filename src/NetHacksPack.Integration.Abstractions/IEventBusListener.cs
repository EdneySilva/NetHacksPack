using System.Threading;
using System.Threading.Tasks;

namespace NetHacksPack.Integration.Abstractions
{
    public delegate Task OnMessageReceivedHandler(object sender, EventMessage eventMessage);

    public delegate Task OnMessageReceivedHandler<T>(object sender, T eventMessageContext);

    public interface IEventBusListener
    {
        event OnMessageReceivedHandler OnMessageReceived;

        void StartListener(CancellationToken cancellationToken);
    }

    public interface IEventBusListener<T>
    {
        event OnMessageReceivedHandler<T> OnMessageReceived;

        void StartListener(CancellationToken cancellationToken);
    }
}
