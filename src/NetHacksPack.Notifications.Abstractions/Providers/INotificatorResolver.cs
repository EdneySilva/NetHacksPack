using NetHacksPack.Notifications.Abstractions.Notificators;

namespace NetHacksPack.Notifications.Abstractions.Providers
{
    /// <summary>
    /// Interface that defines if the notificator can handler a notification
    /// </summary>
    public interface INotificatorResolver
    {
        /// <summary>
        /// Notificator used by the context that can handle the notification
        /// </summary>
        INotificator Notificator { get; }

        /// <summary>
        /// check if the notificator can handle the message
        /// </summary>
        /// <typeparam name="T">a generic type from the notification, should inherit from NetHacksPack.Notifications.Abstractions.Notification</typeparam>
        /// <param name="message">an object that contains the data and metadata used to send notifications</param>
        /// <returns>a boolean that defines if the notificator can handler the message</returns>
        bool CanHandler<T>(T message) where T : Notification;
    }
}
