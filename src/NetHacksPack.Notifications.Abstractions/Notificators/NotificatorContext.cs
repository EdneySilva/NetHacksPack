using System;
using System.Threading.Tasks;

namespace NetHacksPack.Notifications.Abstractions.Notificators
{
    /// <summary>
    /// Represents the notificator context that can handle the notification
    /// </summary>
    /// <typeparam name="T">a generic type from the notification, should inherit from NetHacksPack.Notifications.Abstractions.Notification</typeparam>
    public struct NotificatorContext
    {
        /// <summary>
        /// Default constructor used to create the NotificatorContext object
        /// </summary>
        /// <param name="message">an object that contains the data and metadata used to send notifications</param>
        /// <param name="notificator">notificator used by the context that can handle the notification</param>
        public NotificatorContext(Notification message, INotificator notificator)
        {
            Message = message;
            Notificator = notificator;
        }

        /// <summary>
        /// The message used to notify
        /// </summary>
        public Notification Message { get; }

        /// <summary>
        /// Notificator used by the context that can handle the notification
        /// </summary>
        public INotificator Notificator { get; }

        /// <summary>
        /// Create the task that sends the notification
        /// </summary>
        /// <returns>A task that will handle async the notification send</returns>
        public async Task<NotificationResult> SendAsync()
        {
            try
            {
                await this.Notificator.SendAsync(Message);
                return NotificationResult.Success(this);
            }
            catch (Exception ex)
            {
                return NotificationResult.Fail(this, ex);
            }
        }
    }
}
