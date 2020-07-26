using NetHacksPack.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ECorp.Events.Helper.Serialization
{
    public delegate void EventFieldSetter(object instance, object value);

    public struct FieldDescriptor
    {
        private readonly string name;
        private readonly Func<Event, object> getter;

        public FieldDescriptor(FieldInfo field, Func<Event, object> getter)
        {
            this.name = field.Name;
            Field = field;
            this.getter = getter;
        }

        public FieldInfo Field { get; }

        public void CloneValue(Event target, Event source)
        {
            this.Field.SetValue(target, getter(source));
        }
    }

    public class EventDeserializationProvider
    {
        Dictionary<string, FieldDescriptor> fields = new Dictionary<string, FieldDescriptor>();

        //static EventDeserializationProvider()
        //{
        //    var type = typeof(Event);
        //    var objfields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
        //    var eventIdField = objfields.First(w => w.Name.Contains("EventId"));
        //    fields.Add("EventId", new FieldDescriptor(eventIdField, (evt) => evt.EventName));
        //    var eventNameField = objfields.First(w => w.Name.Contains("EventName"));
        //    fields.Add("EventName", new FieldDescriptor(eventNameField, (evt) => evt.EventName));
        //    var eventTimeSpanField = objfields.First(w => w.Name.Contains("EventTimeSpan"));
        //    fields.Add("EventTimeSpan", new FieldDescriptor(eventTimeSpanField, (evt) => evt.EventName));
        //}

        public EventDeserializationProvider(Dictionary<string, FieldDescriptor> fields)
        {
            this.fields = fields;
        }

        public Event Deserialize(Event @event)
        {

            return @event;
        }
    }
}
