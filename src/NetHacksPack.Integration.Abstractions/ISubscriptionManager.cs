using NetHacksPack.Core;

namespace NetHacksPack.Integration.Abstractions
{
    public interface ISubscriptionManager
    {
        string GetEventKey<T>() where T : Event;
        bool IsSubscribedOn(string eventName);
        void AddSubscription<TEventDesc, TEventHandler>()
            where TEventDesc : Event
            where TEventHandler : IEventHandler<TEventDesc>;
        ISubscription GetSubscription(string eventName);
    }

    public interface ISubscriptionManager<TContext>
    {
        string GetEventKey<T>() where T : Event;
        bool IsSubscribedOn(string eventName);
        void AddSubscription<TEventDesc, TEventHandler>()
            where TEventDesc : Event
            where TEventHandler : IEventHandler<TEventDesc>;
        ISubscription<TContext> GetSubscription(string eventName);
    }
}
