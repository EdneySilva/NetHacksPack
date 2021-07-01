using System.Threading.Tasks;

namespace NetHacksPack.Notifications.Abstractions
{
    /// <summary>
    /// This interface represents a client that use the providers to hub the notification across the notificators
    /// </summary>
    public interface INotificationClient
    {
        /// <summary>
        /// Send an async notification
        /// </summary>
        /// <typeparam name="T">a generic type from the notification, should inherit from NetHacksPack.Notifications.Abstractions.Notification</typeparam>
        /// <param name="message">an object that contains the data and metadata used to send notifications</param>
        /// <returns>a tasks used to send the notifications async</returns>
        Task SendAsync<T>(T message) where T : Notification;
    }
}
