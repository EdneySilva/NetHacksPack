using System;
using System.Linq;

namespace NetHacksPack.Core
{
    public abstract class Event
    {
        private static Lazy<Type> displayNameAttributeType = new Lazy<Type>(() => typeof(System.ComponentModel.DisplayNameAttribute));


        public Event()
        {
            EventName = GetEventName(this.GetType());
            EventId = Guid.NewGuid();
            EventTimeSpan = DateTime.Now;
        }

        public Event(Guid eventId)
        {
            EventName = GetEventName(this.GetType());
            EventId = eventId;
            EventTimeSpan = DateTime.Now;
        }

        public Event(string eventName)
        {
            if (string.IsNullOrWhiteSpace(eventName))
                throw new ArgumentNullException(nameof(eventName));
            EventId = Guid.NewGuid();
            EventName = eventName;
            EventTimeSpan = DateTime.Now;
        }

        public Event(Guid eventId, DateTime eventTimeSpan)
        {
            EventName = GetEventName(this.GetType());
            EventId = eventId;
            EventTimeSpan = DateTime.Now;
        }

        public Event(string eventName, Guid? eventId, DateTime? eventTimeSpan)
        {
            EventName = eventName;
            EventId = eventId ?? Guid.NewGuid();
            EventTimeSpan = eventTimeSpan ?? DateTime.Now;
        }


        public Guid EventId { get; }

        public string EventName { get; }

        public byte EventPriority { get; private set; }

        public DateTime EventTimeSpan { get; }

        public virtual byte DefinePriority()
        {
            this.EventPriority = 0;
            return this.EventPriority;
        }

        public virtual Event Visit()
        {
            return this;
        }

        public static string GetEventName(Type eventType)
        {
            var attr = eventType.GetCustomAttributes(displayNameAttributeType.Value, true).FirstOrDefault() as System.ComponentModel.DisplayNameAttribute;
            if (attr != null)
            {
                return attr.DisplayName;
            }
            else
            {
                var value = string.Empty;
                foreach (var item in eventType.Name)
                {
                    if (char.IsUpper(item))
                    {
                        if (value.Length > 0)
                            value += "-";
                        value += char.ToLower(item);
                    }
                    else
                        value += item;
                }
                return value;
            }
        }

        public static string GetEventName<T>() where T : Event
        {
            return GetEventName(typeof(T));
        }
    }
}
