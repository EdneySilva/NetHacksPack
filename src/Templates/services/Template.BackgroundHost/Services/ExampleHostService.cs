using NetHacksPack.Hosting.Listener;
using NetHacksPack.Integration.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Template.BackgroundHost.Services
{
    class ExampleHostService : EventBusListenerBackgroundService
    {
        private readonly IEventBus eventBus;

        public ExampleHostService(IServiceProvider serviceProvider, ILogger<EventBusListenerBackgroundService> logger, IEventBus eventBus)
            : base(serviceProvider, logger)
        {
            this.eventBus = eventBus;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task.Run(() =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    Console.WriteLine("Enter your name");
                    this.eventBus.Publish(new Events.UserCreated(Console.ReadLine()));
                }
            }, stoppingToken);
            return base.ExecuteAsync(stoppingToken);
        }

        protected override void ConfigureEvents()
        {
            this.Subscribe<Events.UserCreated>();
        }
    }
}
