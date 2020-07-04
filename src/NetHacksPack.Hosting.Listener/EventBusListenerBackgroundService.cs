using NetHacksPack.Core;
using NetHacksPack.Integration.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NetHacksPack.Hosting.Listener
{
    public abstract class EventBusListenerBackgroundService : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<EventBusListenerBackgroundService> logger;
        private IEventBus eventBus;

        public EventBusListenerBackgroundService(
            IServiceProvider serviceProvider,
            ILogger<EventBusListenerBackgroundService> logger)
        {
            this.serviceProvider = serviceProvider;
            this.logger = logger;
            this.eventBus = serviceProvider.GetService<IEventBus>();
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                this.ConfigureEvents();
                return base.StartAsync(cancellationToken);
            });
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() =>
            {
                this.eventBus.Connect(stoppingToken);
            });
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }

        protected abstract void ConfigureEvents();

        protected EventBusListenerBackgroundService AddEventHandler<TEventDesc, TEventHandler>()
            where TEventDesc : Event
            where TEventHandler : IEventHandler<TEventDesc>
        {
            this.eventBus.Subscribe<TEventDesc, TEventHandler>();
            return this;
        }

        protected EventBusListenerBackgroundService Subscribe<TEventDesc>()
            where TEventDesc : Event
        {
            this.eventBus.Subscribe<TEventDesc, IEventHandler<TEventDesc>>();
            return this;
        }
    }
}
