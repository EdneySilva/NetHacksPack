using System;
using System.Collections.Generic;
using System.Text;

namespace NetHacksPack.Database.Extension.EFCore.Logging
{
    public delegate string AuditTableNameProvider();

    public class EventLog
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string EventUser { get; set; }
        public string EntityType { get; set; }
        public string EventType { get; set; }
        public string DataKeysValues { get; set; }
        public string OriginalValues { get; set; }
    }
}
