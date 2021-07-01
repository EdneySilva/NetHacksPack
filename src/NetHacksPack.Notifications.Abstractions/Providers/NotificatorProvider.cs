using NetHacksPack.Notifications.Abstractions.Notificators;
using System.Collections.Generic;
using System.Linq;

namespace NetHacksPack.Notifications.Abstractions.Providers
{
    /// <summary>
    /// Class that implements wich notificator can handle the message
    /// </summary>
    public class NotificatorProvider : INotificatorProvider
    {
        /// <summary>
        /// enumerable of notificators to resolve a message
        /// </summary>
        private readonly IEnumerable<INotificatorResolver> resolvers;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="resolvers">enumerable of notificators to resolve a message</param>
        public NotificatorProvider(IEnumerable<INotificatorResolver> resolvers)
        {
            this.resolvers = resolvers;
        }

        /// <summary>
        /// Get all notificator contexts that can handle the notification
        /// </summary> 
        /// <typeparam name="T">a generic type from the notification, should inherit from NetHacksPack.Notifications.Abstractions.Notification</typeparam>
        /// <param name="message">an object that contains the data and metadata used to send notifications</param>
        /// <returns>An enumerable with the notificator contexts that can handle the provided notification</returns>
        public IEnumerable<NotificatorContext> GetNotificators<T>(T message) where T : Notification
        {
            var notificators = resolvers.Where(w => w.CanHandler(message)).Select(s => new NotificatorContext(message, s.Notificator));
            return notificators;
        }
    }
}
