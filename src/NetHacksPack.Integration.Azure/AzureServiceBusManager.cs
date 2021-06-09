using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Collections;
using System.Collections.Generic;
using NetHacksPack.Integration.Abstractions;

namespace NetHacksPack.Integration.Azure
{
    class AzureServiceBusManager : IEventBusManager
    {
        private readonly IEnumerable<IEventBusErrorHandle> errorHandlers;

        public AzureServiceBusManager(IEnumerable<IEventBusErrorHandle> eventBusErrorHandles)
        {
            this.errorHandlers = eventBusErrorHandles;
        }

        public Task HandleError(EventBusError busError)
        {
            foreach (var item in errorHandlers)
            {
                item.Handle(busError);
            }
            return Task.CompletedTask;
        }
    }
}
