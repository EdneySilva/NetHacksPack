using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetHacksPack.Core
{
    public class Command : Message, IRequest<bool>
    {
        public DateTime Timestamp { get; private set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }

    public class Command<TResult> : Message, IRequest<TResult>
    {
        public DateTime Timestamp { get; private set; }

        public Command()
        {

        }

        public virtual bool IsValid()
        {
            return true;
        }
    }
}
