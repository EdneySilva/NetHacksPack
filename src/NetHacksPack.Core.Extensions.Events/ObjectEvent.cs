using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetHacksPack.Core
{
    public class ObjectEvent : Message, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected ObjectEvent()
        {
            Timestamp = DateTime.Now;
        }
    }
}
