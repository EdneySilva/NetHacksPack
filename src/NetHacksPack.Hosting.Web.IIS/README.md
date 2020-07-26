NetHacksPack.Hosting.Web.IIS
=====================
This package is a standard implementation for a host that initializes web application host with support to IIS

### Why use NetHacksPack.Hosting.Web.IIS
---
Istead of concern about infrastructure and how handler errors about that, you can just put a standar host up.
This host already be configurated with the following configurations:

    - The default path is set to the current executing assembly
    - A required default **appsettings.json** file, in this file you set all common configurations accross environments
    - An optional **appsettings.{ENV}.json** file, the ENV variable is replaced by the environment name, in this file you set all configurations by the environment
    - An optional **appsettings.database.json** file, in this file you set all configurations used by your database
    - An optional **appsettings.event-bus.json** file, in this file you set all configurations used by your event bus
    - An optional **appsettings.log.json** file, in this file you set all configurations used by your log tools
    - An optional **appsettings.cache.json** file, in this file you set all configurations used by your cache tools

Even this configurations being set when the host is up, you are still free to change the configurations for the server, and also you can set some configs passing paramenters to the application

#### How to use
---

It's very simple to use it, you will just to call the method to turn the server up

```c#

using NetHacksPack.Hosting.Web.IIS;

class Program
{    
    public static Task Main(string[] args)
    {        
        IISHost.Run(args);
    }
}

```

To take control about what is happen and customize the server configs, you can just use the second parameter on method run and apply your configurations

```c#

using NetHacksPack.Hosting.Web.IIS;

class Program
{    
    public static Task Main(string[] args)
    {        
        IISHost.Run(args, (option) => 
        {
            // config your server
            options.ConfigureServices((context, services) =>
            {
                
            });
        });
    }
}

```
