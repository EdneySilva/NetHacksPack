using ECorp.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ECorp.Realtime.SignalR.Events
{
    public class RealtimeEventDescriptor : Event, IDictionary<string, object>
    {
        private readonly Dictionary<string, object> data = new Dictionary<string, object>();

        public RealtimeEventDescriptor()
            : base(string.Empty)
        {
        }

        public object this[string key]
        {
            get => this.data[key];
            set => this.data[key] = value;
        }

        //public override string EventName { get; set; } = string.Empty;

        public ICollection<string> Keys => this.data.Keys;

        public ICollection<object> Values => this.data.Values;

        public int Count => this.data.Count;

        public bool IsReadOnly => ((ICollection<KeyValuePair<string, object>>)this.data).IsReadOnly;

        public void Add(string key, object value)
        {
            this.data.Add(key, value);
        }

        public void Add(KeyValuePair<string, object> item)
        {
            this.data.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            this.data.Clear();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return this.data.Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return this.data.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<string, object>>)this.data).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return this.data.GetEnumerator();
        }

        public bool Remove(string key)
        {
            return this.data.Remove(key);
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            return ((ICollection<KeyValuePair<string, object>>)this.data).Remove(item);
        }

        public bool TryGetValue(string key, [MaybeNullWhen(false)] out object value)
        {
            return this.data.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.data.GetEnumerator();
        }
    }
}
