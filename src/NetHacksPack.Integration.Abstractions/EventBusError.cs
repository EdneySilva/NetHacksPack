using System;

namespace NetHacksPack.Integration.Abstractions
{
    public struct EventBusError
    {
        public Guid Id { get; }
        public string Details { get; set; }
        public DateTime CreatedAt { get; }
        public AggregateException Exceptions { get; }

        public EventBusError(Guid id, string details, DateTime createdAt, AggregateException exceptions)
        {
            Id = id;
            Details = details;
            CreatedAt = createdAt;
            Exceptions = exceptions;
        }
    }
}
