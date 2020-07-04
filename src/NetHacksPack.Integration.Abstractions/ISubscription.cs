using System.Threading.Tasks;

namespace NetHacksPack.Integration.Abstractions
{
    public interface ISubscription
    {
        Task Handler(EventMessage eventMessage);
    }

    public interface ISubscription<TContext>
    {
        Task Handler(TContext context, EventMessage eventMessage);
    }
}
