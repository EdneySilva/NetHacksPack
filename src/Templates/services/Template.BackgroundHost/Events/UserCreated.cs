using NetHacksPack.Core;
using System;
using System.ComponentModel;

namespace Template.BackgroundHost.Events
{
    [DisplayName("user-created")]
    class UserCreated : Event
    {
        public UserCreated(string name, Guid eventId = new Guid())
            : base(eventId == Guid.Empty ? Guid.NewGuid() : eventId) 
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
