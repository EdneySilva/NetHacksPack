using Microsoft.Extensions.Options;
using NetHacksPack.Notifications.Abstractions;
using NetHacksPack.Notifications.Abstractions.Notificators;
using System.Net.Mail;
using System.Threading.Tasks;

namespace NetHacksPack.Notifications.Extensions.SmtpMail
{
    /// <summary>
    /// Class that implements the notification by send smtp emails
    /// </summary>
    class SmtpEmailNotificatorClient : INotificator
    {
        /// <summary>
        /// options from smtp configurationsd
        /// </summary>
        private SmtpMailOptions smtpClientCreator;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="optionsMonitor">options from smtp configurationsd</param>
        public SmtpEmailNotificatorClient(IOptionsMonitor<SmtpMailOptions> optionsMonitor)
        {
            this.smtpClientCreator = optionsMonitor.CurrentValue;
        }

        /// <summary>
        /// Sends a message by smtp client
        /// </summary>
        /// <typeparam name="T">a generic type from the notification, should inherit from NetHacksPack.Notifications.Abstractions.Notification</typeparam>
        /// <param name="message">an object that contains the data and metadata used to send notifications</param>
        /// <returns>a Task to execute async</returns>
        public async Task SendAsync<T>(T message) where T : Notification
        {
            using var smtpclient = smtpClientCreator.ToSmtpClient();
            var notification = new SmtpMailNotification(message.GetMetadatas());
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(notification.From);

                mailMessage.To.Add(string.Join(',', notification.Recepients));
                if (notification.CopiedRecepients.Length > 0)
                    mailMessage.CC.Add(string.Join(',', notification.CopiedRecepients));
                mailMessage.Subject = notification.Title;
                mailMessage.IsBodyHtml = notification.IsHtml;
                mailMessage.Body = notification.Content;

                await smtpclient.SendMailAsync(mailMessage);
            }
        }
    }
}