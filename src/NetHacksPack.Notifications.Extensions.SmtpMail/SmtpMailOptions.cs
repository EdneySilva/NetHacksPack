using System.Net;
using System.Net.Mail;

namespace NetHacksPack.Notifications.Extensions.SmtpMail
{
    /// <summary>
    /// Class that describs a SmtpMailOptions used to create a SmtpClient
    /// </summary>
    public class SmtpMailOptions
    {
        /// <summary>
        /// Gets or sets the server port
        /// </summary>
        public int Port { get; set; }
        
        /// <summary>
        /// Gets or sets the host 
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the ssl enable configuration
        /// </summary>
        public bool EnableSsl { get; set; }

        /// <summary>
        /// Gets or sets the user to authenticate on the server
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Gets or sets the password to authenticate on the server
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the smtp delivery method
        /// </summary>
        public SmtpDeliveryMethod DeliveryMethod { get; set; }

        /// <summary>
        /// Creates a SmtpClient instance based on the configurations
        /// </summary>
        /// <returns>A SmtpClient instance</returns>
        public SmtpClient ToSmtpClient()
        {
            NetworkCredential credential = null;
            if (!(string.IsNullOrEmpty(User) && string.IsNullOrEmpty(Password)))
            {
                credential = new NetworkCredential(User, Password);
            }
            return new SmtpClient(this.Host, this.Port)
            {
                EnableSsl = this.EnableSsl,
                DeliveryMethod = this.DeliveryMethod,
                Credentials = credential,
                UseDefaultCredentials = credential == null                
            };
        }
    }
}
