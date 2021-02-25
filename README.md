# MyLab.Log.Ctx

For .NET Core 3.1+

[![NuGet](https://img.shields.io/nuget/v/MyLab.Log.Ctx.svg)](https://www.nuget.org/packages/MyLab.Log.Ctx/)

Check out the latest changes in the [changelog](/changelog.md).

## Review

`MyLab.Log.Ctx` integrates in `.NET Core` built-in `DI` and provides tools to write logs which may be enriched by context parameters.

Based on [MyLab.Log.Dsl](https://github.com/mylab-log/log-dsl).

To integrate tools, please use extension method for `IServiceCollection`:

```c#
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
	    //....
	    
        services.AddLogCtx();	
        
        //....
    }
}
```

Using `IDslLogger` as `DI` injected dependency:

```c#
class Service
{
	IDslLogger _log;
	
	public Service(IDslLogger logger)
	{
		_log = logger;
	}
	
	public Service(IDslLogger<Service> logger)
	{
		_log = logger;
	}
	
	public void Foo()
	{
		_log.Action("Do Foo").Write();
	}
}
```

## Extensions

Log context extension is an object which implements `ILogContextExtension` interface. It uses to add context parameters in each log message.

#### Register extension

To register extensions use `registrar` parameter of method `AddLogCtx`:

```c#
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
	    //....
	    
        services.AddLogCtx(registrar => 
        		registrar.Register<TLogCtxExtension>()
        	);	
        
        //....
    }
}
```