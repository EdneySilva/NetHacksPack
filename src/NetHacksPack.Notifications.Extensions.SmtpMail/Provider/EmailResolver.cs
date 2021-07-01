using NetHacksPack.Notifications.Abstractions;
using NetHacksPack.Notifications.Abstractions.Notificators;
using NetHacksPack.Notifications.Abstractions.Providers;

namespace NetHacksPack.Notifications.Extensions.SmtpMail.Provider
{
    /// <summary>
    /// Class that is responsible to check if the SmtpEmailNotificatorClient can handler the message
    /// </summary>
    class EmailResolver : INotificatorResolver
    {
        /// <summary>
        /// Instance of SmtpEmailNotificatorClient
        /// </summary>
        public INotificator Notificator { get; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="smtpEmailNotificatorClient">Instance of SmtpEmailNotificatorClient</param>
        public EmailResolver(SmtpEmailNotificatorClient smtpEmailNotificatorClient)
        {
            Notificator = smtpEmailNotificatorClient;
        }

        /// <summary>
        /// Check if the SmtpMailNotificator can handler the message 
        /// </summary>
        /// <typeparam name="T">a generic type from the notification, should inherit from NetHacksPack.Notifications.Abstractions.Notification</typeparam>
        /// <param name="message">an object that contains the data and metadata used to send notifications</param>
        /// <returns>boolean that indicates the message can be handled by the SmtpEmailNotificatorClient</returns>
        public bool CanHandler<T>(T message) where T : Notification
        {
            if (message is SmtpMailNotification)
                return true;
            if (!message.ContainsMetadata(Constants.MAIL_FROM))
                return false;
            if (!message.ContainsMetadata(Constants.MAIL_TITLE))
                return false;
            if (!message.ContainsMetadata(Constants.MAIL_CONTENT))
                return false;
            if (!message.ContainsMetadata(Constants.MAIL_RECIPIENTS))
                return false;
            return true;
        }
    }
}
