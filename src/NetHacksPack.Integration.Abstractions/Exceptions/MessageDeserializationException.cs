using System;

namespace NetHacksPack.Integration.Abstractions.Exceptions
{
    public class MessageDeserializationException : Exception
    {
        public MessageDeserializationException()
            : base("An error occurred when tried to deserialize the received message")
        {

        }

        public MessageDeserializationException(Exception innerException)
            : base("An error occurred when tried to deserialize the received message", innerException)
        {

        }

        public MessageDeserializationException(string message)
            : base(message)
        {

        }

        public MessageDeserializationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
