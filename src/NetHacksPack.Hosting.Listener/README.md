NetHacksPack.Hosting.Listener
=====================
This package is a standard implementation for a host that initializes Listeners in an EventBus

### Why use NetHacksPack.Hosting.Listener
---
Instead of concern about infrastructure and how handler errors about that, you can just configure what events your application will handler

#### How to use
---

It's very simple to use it, you will just inherite from the base class **EventBusListenerBackgroundService**, configure the events you would like to handle and add the services dependencies.

```c#



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
        // you can overrite the behaviour from the ExecuteAsync
        return base.ExecuteAsync(stoppingToken);
    }
    
    protected override void ConfigureEvents()
    {
        // here you will register your events handlers
        vthis.Subscribe<Events.UserCreated>();
    }
}

```
