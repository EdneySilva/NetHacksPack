NetHacksPack.Hosting.Connection;
=====================
This package contains the implementation from the package NetHacksPack.Hosting.Abstraction, providing you an implementation for **IConnectionStringProvider**.

The connection provider uses **IConfiguration** and the **Environment Variables** to provide the requested connection configuration.

### Why use NetHacksPack.Hosting
---
This package improve the developer time, giving an implementation to provide any kind of connection configuration, without concerns about where it comes from.

#### How to use
---
Create services that require a connection provide

```c#

// create a dependency injector to inject in your container the resolver for your connection
namespace MyDependencyInjectionNamespace
{
  public static class MyDependencyInjector
  {
    public static IServiceCollection AddMyDbService(IServiceCollection services, Func<IConnectionStringProvider, string> myConnectionProvider)
    {
      services.AddSingleton(myConnectionProvider);
      // you can change your service life as you wish according your application
      services.AddScoped<IMyDatabaseService, MyDatabaseService>();
      return services;
    }
  }
}

// create a service that needs a connection string
namespace MyServiceNamespace
{
  public interface IMyDatabaseService
  {
    void ExecuteQuery(string query);
  }

  class MyDatabaseService : IMyDatabaseService
  {
    private readonly Func<IConnectionStringProvider, string> myConnectionProvider;
    private readonly IConnectionStringProvider connectionProvider;

    public MyDatabaseService(IConnectionStringProvider connectionProvider, Func<IConnectionStringProvider, string> myConnectionProvider)
    {
        this.myConnectionProvider = myConnectionProvider;
        this.connectionProvider = connectionProvider;
	 }

    public void ExecuteQuery(string query)
    {
        var myConnection = System.Data.SqlClient.SqlConnection(this.myConnectionProvider(this.connectionProvider));
        myConnection.Open();
        // do something ...
	 }
  }
}

```

Now you are will need register the dependency from the connection provider service and your services as well.

```c#
using MyDependencyInjectionNamespace;

namespace MyNamespaceApp
{
    class Startup
    {
        public void Configure(IServiceCollection services)
        {
            services.AddConnectionProvider();

            services.AddMyDbService((connectionProvider) => connectionProvider.GetConnectionString("myDbConnectionKey"));
        }
    }
}

```

Now in your Controller e.g.

```c#

using MyDependencyInjectionNamespace;
using MyServiceNamespace;

namespace MyNamespaceApp.Controllers
{
    public class MyController : Controller
    {
        private readonly IMyDatabaseService myDbService;

        public MyController(IMyDatabaseService myDbService)
        {
            this.myDbService = myDbService;
		}

        public IActionResult Index(IServiceCollection services)
        {
            this.myDbService.ExecuteQuery("INSERT INTO myTable(1, 'mydatavalue')");
        }
    }
}

```

To use a complex object as a connection option the implementation is pretty the same.
You will just need change the type of your Func, and consequently the for usage 

```c#

// a simple connector option
public class DbOptions
{
    public string Server { get; set; }
    public string Database { get; set; }
    public string User { get; set; }
    public string Password { get; set; }

    public override string ToString()
    {
        return $"data source={this.Server}; initial catalog={this.database}; user={this.User}; password={this.Password}";
	}
}

// As explained you just change the return type
public static IServiceCollection AddMyDbService(IServiceCollection services, Func<IConnectionStringProvider, DbOptions> myConnectionProvider);


class MyDatabaseService : IMyDatabaseService
{
    private readonly Func<IConnectionStringProvider, DbOptions> myConnectionProvider;
    // the constructor is the same above
    
    public void ExecuteQuery(string query)
    {
        var myConnectionOption = this.myConnectionProvider(this.connectionProvider)
        var myConnection = System.Data.SqlClient.SqlConnection(myConnectionOption.ToString());
        myConnection.Open();
        // do something ...
	}
}

// and to register

public void Configure(IServiceCollection services)
{
    services.AddConnectionProvider();

    services.AddMyDbService((connectionProvider) => connectionProvider.GetConnectorOptions<DbOptions>("myDbConnectionKey"));
}

```