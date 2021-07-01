using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NetHacksPack.Notifications.Abstractions
{
    /// <summary>
    /// Class that represents a notifications, it contains the data and metadata necessary to handle a notification
    /// </summary>
    public class Notification
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="headers">contains all the metadata used to handle a notification</param>
        public Notification(Dictionary<string, string> headers)
        {
            Headers = headers;
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Gets or sets the notification Id
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// contains all the metadata used to handle a notification
        /// </summary>
        protected Dictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Check if the notification contains the metadata
        /// </summary>
        /// <param name="data">the name of the metadata</param>
        /// <returns>boolean indicating if the notificator contains the metadata</returns>
        public virtual bool ContainsMetadata(string data)
        {
            return this.Headers.ContainsKey(data);
        }

        /// <summary>
        /// Gets all headers metadatas from notification
        /// </summary>
        /// <returns>A dictionary key, value with all the headers from the notification</returns>
        public Dictionary<string, string> GetMetadatas()
        {
            return this.Headers;
        }

        /// <summary>
        /// Serialize the notification using the Notification Header informations to do that
        /// </summary>
        /// <returns>a string formatted as a Json</returns>
        public string Serialize()
        {
            return JsonConvert.SerializeObject(this.Headers);
        }
    }
}
