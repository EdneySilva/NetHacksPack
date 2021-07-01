using System.Threading.Tasks;

namespace NetHacksPack.Notifications.Abstractions.Notificators
{
    /// <summary>
    /// Interface that defines what is a notificator
    /// </summary>
    public interface INotificator
    {
        /// <summary>
        /// Sends a message by smtp client
        /// </summary>
        /// <typeparam name="T">a generic type from the notification, should inherit from NetHacksPack.Notifications.Abstractions.Notification</typeparam>
        /// <param name="message">an object that contains the data and metadata used to send notifications</param>
        /// <returns>a Task to execute async</returns>
        Task SendAsync<T>(T message) where T : Notification;
    }
}
