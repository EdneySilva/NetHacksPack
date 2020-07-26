using System;
using System.Collections.Generic;
using System.Text;

namespace NetHacksPack.Core
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }

        protected abstract Result<Entity> Validate();
    }
}
