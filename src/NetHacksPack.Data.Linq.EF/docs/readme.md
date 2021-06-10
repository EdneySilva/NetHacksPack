## .NET NetHacksPack.Data.Linq.EF

This package contains the implementations that enables the software using IQueryable<T> interface straightforwardly as a readonly repository, injecting it on your services, controllers or handlers without a whole infrastructure layer's implementation.

### Usage
---

To use it in your project you can dowload diretly from Nuget:

```ps
dotnet add package NetHacksPack.Data.Linq.EF
```  

To be able to use this behaviour you need firstval configure this package in your project register the dependencies into your container

```csharp
public void ConfigureServices(IServiceCollection services)
{
  services.AddQueryableAsRepository();
}
```  

After that you can just use it inject the IQueryable<T> into the class (service, handler, controller, or any kind of layer) that you wish to query data
  
```csharp
public class MyService : IMyService
{
  private readonly IQueryable<Foo> readonlyRepository;
  
  public MyService(IQueryable<Foo> readonlyRepository)
  {
    this.readonlyRepository = readonlyRepository;
  }
  
  public IEnumerable<Foo> GetEnabledFoo()
  {
    this.readonlyRepository.Where(w => w.Enabled == true).ToList();
  }
}
```  

You can also use/inject it directly in your controller and it can be helpfull when you just want to read and return data only to the users
```csharp
public class MyFooController : ControllerBase
{
  private readonly IQueryable<Foo> readonlyRepository;
  
  public MyFooController(IQueryable<Foo> readonlyRepository)
  {
    this.readonlyRepository = readonlyRepository;
  }
  
  [HttpGet("{page?}/{pageSize?}")]
  public IEnumerable<Foo> Get([FromRoute(Name = "page")] int? page, [FromRoute(Name = "pageSize")] int? pageSize)
  {
      var items = this.readonlyRepository.Skip((pageSize ?? 0) * (page ?? 0)).Take(pageSize ?? 10).ToList();
      return items;
  }
}
```
