using System;

namespace NetHacksPack.Integration.Abstractions.Exceptions
{
    public class MessageNullException : Exception
    {
        public MessageNullException()
            : base("The received message can not be null")
        {

        }

        public MessageNullException(string message)
            : base(message)
        {

        }
    }
}
