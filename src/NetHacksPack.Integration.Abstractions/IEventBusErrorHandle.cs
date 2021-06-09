using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetHacksPack.Integration.Abstractions
{
    public interface IEventBusErrorHandle
    {
        Task Handle(EventBusError busError);
    }
}
