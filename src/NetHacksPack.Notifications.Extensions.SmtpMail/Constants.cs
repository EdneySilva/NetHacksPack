namespace NetHacksPack.Notifications.Extensions.SmtpMail
{
    /// <summary>
    /// Class that defines the constants used by other parts
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Gets the from used by the mail
        /// </summary>
        public const string MAIL_FROM = "smtpMailFrom";
        /// <summary>
        /// Gets the title used by mail
        /// </summary>
        public const string MAIL_TITLE = "smtpMailTitle";
        /// <summary>
        /// Gets the cotent used by mail
        /// </summary>
        public const string MAIL_CONTENT = "smtpMailContent";
        /// <summary>
        /// Gets the recipients used by mail
        /// </summary>
        public const string MAIL_RECIPIENTS = "smtpMailRecipients";
        /// <summary>
        /// Gets the hidden recipients used by mail
        /// </summary>
        public const string MAIL_HIDDEN_RECIPIENTS = "smtpMailHiddenRecipients";
        /// <summary>
        /// Gets the copied recipients (CC) used by mail
        /// </summary>public const string MAIL_HIDDEN_RECIPIENTS = "smtpMailHiddenRecipients";
        public const string MAIL_COPIED_RECIPIENTS = "smtpMailCopiedRecipients";
        /// <summary>
        /// Gets if the body is html used by mail
        /// </summary>
        public const string MAIL_ISHTML = "smtpMailIsHtml";
    }
}