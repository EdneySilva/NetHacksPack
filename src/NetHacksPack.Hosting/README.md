NetHacksPack.Hosting
=====================
This package contains the implementation from the package NetHacksPack.Hosting.Abstraction.
It helps you to easily create a Background application service already configured with the standard things it needs.

All project that use this package will Run a application with the standard configuration:

* The base path is the directory where the application is running (.exe|.dll)
* All the environment variables have the default prefixes configured by the constant NetHacksPack.Hosting.Abstractions.Constants.Prefixies.ENVIRONMENT_PREFIX
* A required appsettings.json file named by the configured constant NetHacksPack.Hosting.Abstractions.Constants.Prefixies.APPSETTINGS_JSON
* An optional appsetting.json file used to apply configurations according your environment, named by the configured constant NetHacksPack.Hosting.Abstractions.Constants.Prefixies.APPSETTINGS_ENV_JSON
* The application will replace automatically the parameter on the name by the environment name by the configured constant NetHacksPack.Hosting.Abstractions.Constants.Prefixies.ENVIRONMENT_NAME
* An optional appsetting.json file used to define configurations from database, named by the configured constant NetHacksPack.Hosting.Abstractions.Constants.Prefixies.APPSETTINGS_DATABASE_JSON
* An optional appsetting.json file used to define configurations from eventbus, named by the configured constant NetHacksPack.Hosting.Abstractions.Constants.Prefixies.APPSETTINGS_EVENT_BUS_JSON
* An optional appsetting.json file used to define configurations from log, named by the configured constant NetHacksPack.Hosting.Abstractions.Constants.Prefixies.APPSETTINGS_LOG_JSON
* An optional appsetting.json file used to define configurations from cache, named by the configured constant NetHacksPack.Hosting.Abstractions.Constants.Prefixies.APPSETTINGS_CACHE_JSON

### Why use NetHacksPack.Hosting
---
This package improve the developer time, giving to the project an already configured application that works without a lot of coding.
In the same time it doesn't take your control about the code, provide ways to handle your console as you need.

#### How to use
---
To create a simple background application with the standard configurations, use can just do:

```c#
 class Program
 {
    static async Task Main(string[] args)
    {
        await AppHost.Run(args);
    }
 }

```
But if you need apply a more specialized configuration, you can use the second parameter on the method Run:

```c#
class Program
{
    static async Task Main(string[] args)
    {
        await AppHost.Run(args, (options) =>
        {
            // here I'm configure the required application services
            options.ConfigureServices((context, services) =>
            {
                // do something
            });

            // here you can configure the host application as you wish
        });
    }
}

```
