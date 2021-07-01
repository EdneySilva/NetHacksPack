using NetHacksPack.Notifications.Abstractions.Providers;
using System.Threading.Tasks;
using System.Linq;

namespace NetHacksPack.Notifications.Abstractions
{
    /// <summary>
    /// Implements a client that can handle the notification send
    /// </summary>
    public class NotificationClient : INotificationClient
    {
        /// <summary>
        /// The Notificator provieder used to obtains all the notificator context handlers
        /// </summary>
        private readonly INotificatorProvider notificatorProvider;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="notificatorProvider">The Notificator provieder used to obtains all the notificator context handlers</param>
        public NotificationClient(INotificatorProvider notificatorProvider)
        {
            this.notificatorProvider = notificatorProvider;
        }

        /// <summary>
        /// Send an async notification
        /// </summary>
        /// <typeparam name="T">a generic type from the notification, should inherit from NetHacksPack.Notifications.Abstractions.Notification</typeparam>
        /// <param name="message">an object that contains the data and metadata used to send notifications</param>
        /// <returns>a tasks used to send the notifications async</returns>
        public Task SendAsync<T>(T message) where T : Notification
        {
            var notificators = this.notificatorProvider.GetNotificators(message);
            var list = notificators.Select(context => context.SendAsync()).ToList();
            return Task.WhenAll(list);
        }
    }
}
