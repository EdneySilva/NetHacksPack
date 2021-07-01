using NetHacksPack.Notifications.Abstractions.Notificators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetHacksPack.Notifications.Abstractions
{
    /// <summary>
    /// Class that represents the result from a notification
    /// </summary>
    public class NotificationResult
    {
        /// <summary>
        /// the instance of the notification context
        /// </summary>
        private readonly NotificatorContext notificatorContext;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="notificatorContext">the instance of the notification context</param>
        public NotificationResult(NotificatorContext notificatorContext)
        {
            this.notificatorContext = notificatorContext;
            NotificationId = notificatorContext.Message.Id;
            NotificationType = notificatorContext.Message.GetType().AssemblyQualifiedName;
        }

        /// <summary>
        /// Gets the notification id
        /// </summary>
        public Guid NotificationId { get; }

        /// <summary>
        /// Gets the notification type
        /// </summary>
        public string NotificationType { get; }

        /// <summary>
        /// Gets the notification result from the operation
        /// </summary>
        public Result Result { get; private set; }

        /// <summary>
        /// Gets the exceptions occurred on the operation
        /// </summary>
        public IEnumerable<Exception> Exceptions { get; private set; }

        /// <summary>
        /// Creates a Success NotificationResult
        /// </summary>
        /// <param name="notificatorContext">the instance of the notificator context</param>
        /// <returns>An instance of notification result</returns>
        public static NotificationResult Success(NotificatorContext notificatorContext)
        {
            var result = new NotificationResult(notificatorContext);
            result.Result = Result.Success;
            return result;
        }

        /// <summary>
        /// Creates a Fail NotificationResult
        /// </summary>
        /// <param name="notificatorContext">the instance of the notificator context</param>
        /// <param name="exceptions">the exceptions occurred on the operation</param>
        /// <returns>An instance of notification result</returns>
        public static NotificationResult Fail(NotificatorContext notificatorContext, params Exception[] exceptions)
        {
            var result = new NotificationResult(notificatorContext);
            result.Result = Result.Fail;
            result.Exceptions = exceptions;
            return result;
        }
    }
}
