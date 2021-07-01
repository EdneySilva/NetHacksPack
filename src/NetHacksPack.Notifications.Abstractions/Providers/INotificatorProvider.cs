using NetHacksPack.Notifications.Abstractions.Notificators;
using System.Collections.Generic;

namespace NetHacksPack.Notifications.Abstractions.Providers
{
    /// <summary>
    /// Interface that defines wich notificator can handle the message
    /// </summary>
    public interface INotificatorProvider
    {
        /// <summary>
        /// Get all notificator contexts that can handle the notification
        /// </summary>
        /// <typeparam name="T">a generic type from the notification, should inherit from NetHacksPack.Notifications.Abstractions.Notification</typeparam>
        /// <param name="message">an object that contains the data and metadata used to send notifications</param>
        /// <returns>An enumerable with the notificator contexts that can handle the provided notification</returns>
        public IEnumerable<NotificatorContext> GetNotificators<T>(T message) where T : Notification;
    }
}
