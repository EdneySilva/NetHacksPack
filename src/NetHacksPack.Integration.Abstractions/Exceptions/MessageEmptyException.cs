using System;

namespace NetHacksPack.Integration.Abstractions.Exceptions
{
    public class MessageEmptyException : Exception
    {
        public MessageEmptyException()
            : base("The received message can not be empty")
        {

        }

        public MessageEmptyException(string message)
            : base(message)
        {

        }

        public MessageEmptyException(Exception exception)
            : base("The received message can not be empty", exception)
        {

        }
    }
}
