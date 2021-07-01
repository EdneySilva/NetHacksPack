using NetHacksPack.Notifications.Abstractions;
using System.Collections.Generic;

namespace NetHacksPack.Notifications.Extensions.SmtpMail
{
    /// <summary>
    /// Class that implements a message that represents a SmtpMailNotification
    /// </summary>
    public class SmtpMailNotification : Notification
    {
        /// <summary>
        /// Gets or sets the from item
        /// </summary>
        public string From
        {
            get
            {
                return Headers[Constants.MAIL_FROM];
            }
            set
            {
                Headers[Constants.MAIL_FROM] = value ?? string.Empty;
            }
        }

        /// <summary>
        /// Gets or sets the title from the notification
        /// </summary>
        public string Title
        {
            get
            {
                return Headers[Constants.MAIL_TITLE];
            }
            set
            {
                Headers[Constants.MAIL_TITLE] = value ?? string.Empty;
            }
        }

        /// <summary>
        /// Gets or sets the content from the notification
        /// </summary>
        public string Content
        {
            get
            {
                return Headers[Constants.MAIL_CONTENT];
            }
            set
            {
                Headers[Constants.MAIL_CONTENT] = value ?? string.Empty;
            }
        }

        /// <summary>
        /// Gets or sets if the notification should render as html
        /// </summary>
        public bool IsHtml
        {
            get
            {
                return Headers[Constants.MAIL_CONTENT].Equals("true");
            }
            set
            {
                Headers[Constants.MAIL_CONTENT] = value ? "true" : "false";
            }
        }

        /// <summary>
        /// Gets or sets the recepients from the notification
        /// </summary>
        public string[] Recepients
        {
            get
            {
                return Headers[Constants.MAIL_RECIPIENTS].Length > 0 ? Headers[Constants.MAIL_RECIPIENTS].Split(',') : new string[0];
            }
            set
            {
                Headers[Constants.MAIL_RECIPIENTS] = value == null ? string.Empty : string.Join(',', value);
            }
        }

        /// <summary>
        /// Gets or sets the hidden recepients from the notification
        /// </summary>
        public string[] HiddenRecepients
        {
            get
            {
                return Headers[Constants.MAIL_HIDDEN_RECIPIENTS].Length > 0 ? Headers[Constants.MAIL_HIDDEN_RECIPIENTS].Split(',') : new string[0];
            }
            set
            {
                Headers[Constants.MAIL_HIDDEN_RECIPIENTS] = value == null ? string.Empty : string.Join(',', value);
            }
        }

        /// <summary>
        /// Gets or sets the Copied recepients from the notification
        /// </summary>
        public string[] CopiedRecepients
        {
            get
            {
                return Headers[Constants.MAIL_COPIED_RECIPIENTS].Length > 0 ? Headers[Constants.MAIL_COPIED_RECIPIENTS].Split(',') : new string[0];
            }
            set
            {
                Headers[Constants.MAIL_COPIED_RECIPIENTS] = value == null ? string.Empty : string.Join(',', value);
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public SmtpMailNotification()
            : base(new Dictionary<string, string>())
        {
            this.Headers.Add(Constants.MAIL_FROM, string.Empty);
            this.Headers.Add(Constants.MAIL_TITLE, string.Empty);
            this.Headers.Add(Constants.MAIL_CONTENT, string.Empty);
            this.Headers.Add(Constants.MAIL_RECIPIENTS, string.Empty);
            this.Headers.Add(Constants.MAIL_HIDDEN_RECIPIENTS, string.Empty);
            this.Headers.Add(Constants.MAIL_COPIED_RECIPIENTS, string.Empty);
            this.Headers.Add(Constants.MAIL_ISHTML, string.Empty);
        }

        /// <summary>
        /// Default constructor that intialize the headers
        /// </summary>
        /// <param name="headers">headers with standart values</param>
        public SmtpMailNotification(Dictionary<string, string> headers) : base(headers)
        {
        }
    }
}
