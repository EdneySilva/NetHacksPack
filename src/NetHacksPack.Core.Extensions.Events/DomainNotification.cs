using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetHacksPack.Core
{
    public class DomainNotification : Message, INotification
    {
        public DateTime Timestamp { get; private set; }
        public Guid Id { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }
        public int Version { get; private set; }

        public DomainNotification(string key, string value)
        {
            Timestamp = DateTime.Now;
            Id = Guid.NewGuid();
            Version = 1;
            Key = key;
            Value = value;
        }
    }
}
