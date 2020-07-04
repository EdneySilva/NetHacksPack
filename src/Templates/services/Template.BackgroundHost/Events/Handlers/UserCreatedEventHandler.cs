using System;
using System.Threading.Tasks;

namespace Template.BackgroundHost.Events.Handlers
{
    class UserCreatedEventHandler : IUserCreatedEventHandler
    {
        public Task Handle(UserCreated @event)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(@event));
           return Task.CompletedTask;
        }
    }
}
