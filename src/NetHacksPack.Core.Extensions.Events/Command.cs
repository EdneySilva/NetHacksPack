using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetHacksPack.Core
{
    public class Command : Message, IRequest<bool>
    {
        public DateTime Timestamp { get; private set; }

        //public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }
    }
}
