NetHacksPack.Hosting.Abstractions
=====================
This package contains the abstractions from providers to help build feature to background services applications

### Providers
---
This interfaces will provide functionalities directly to the application and the infrastructure layers.

#### IConnectionStringProvider
This interface abstract methods, provide the data connection requested by the object witch will connect in a database or a event bus e.g

You can implement this interface choose how you will handle requests for connection data.
It means that you can decide where you will store and how you will decode the connection object

For a simple operations you can use the method GetConnectionString that will help you to retrieve the connection as a string without any kind of deserialization.

```c#
IConnectionStringProvider myConnectionProvider = ...
var connectionString = myConnectionProvider.GetConnectionString("myConnectionKey");
var connection = new MyDbConnection(connectionString);
```
But if you need a more specialized object, you can use GetConnectorOptions to provide a complex metadata

```c#
public class MyConnectorOptions
{
	public int Port { get; set; }
	public string Server { get; set; }
	public string User { get; set; }
	public string Password { get; set; }
}
IConnectionStringProvider myConnectionProvider = ...
var myConnector = myConnectionProvider.GetConnectorOptions<MyConnectorOptions>("myConnectionKey");
```

### Constants
---
The constants contains common values to the application

#### Prefixies
* Environament variables:

```c#
// a default prefix to the application variables
public const string ENVIRONMENT_PREFIX = "NETCORE_";

// a variable to indicate the environment the applicatino is running
public const string ENVIRONMENT_NAME = "ASPNETCORE_ENVIRONMENT";
```

* Application configuration file names

```c#
// the name from the default application setting file
public const string APPSETTINGS_JSON = "appsettings.json";

// the name from the default application setting file based on the environment the applicatin is running replaced on {0}
public const string APPSETTINGS_ENV_JSON = "appsettings.{0}.json";

// the name from a application setting file to store data to database
public const string APPSETTINGS_DATABASE_JSON = "appsettings.database.json";

// the name from a application setting file to store data to eventbus
public const string APPSETTINGS_EVENT_BUS_JSON = "appsettings.event-bus.json";

// the name from a application setting file to store data to log
public const string APPSETTINGS_LOG_JSON = "appsettings.log.json";

// the name from a application setting file to store data to cache
public const string APPSETTINGS_CACHE_JSON = "appsettings.cache.json";
```