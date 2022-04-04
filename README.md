# Mogger

[![NuGet](https://img.shields.io/nuget/v/Mogger?color=green)](https://www.nuget.org/packages/Mogger)

**Mogger** creates `ILogger` mocked instances that prints actual messages to the [xUnit][1]’s display output, with the `ITestouOutputHelper` abstraction.  
Invoke the `Create` method to instantiate a new `ILogger` mock.

```csharp
public class MyTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    
    public MyTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
    
    public void Method()
    {
        ILogger logger = Mogger.Create(_testOutputHelper);
        logger.LogInformation("Mogger's great");
    }
}
```

> Log event messages will be printed to the display output, following the output template below  
> `{Date:HH:mm:ss.fff} {Level, -5} [{ThreadId, -4}]: {Message}`  
> `{Exception.Type}: {Exception.Message}`  
> `{Exception.StackTrace}`

The `Create` method returns an `IMogger` instance (or `IMogger<>`, in case of the `Mogger<>.Create` method), which has an `ILogger` property called `Proxied`. The `Proxied` property returns the actual mocked logger instance, which can be customized, if necessary.

> The `LogInformation`, `LogWarning` , […] methods cannot be mocked (at least with *FakeItEasy*) because they are static extension methods of the `ILogger` interface. Trying to intercept them will cause an exception  
> `FakeItEasy.Configuration.FakeConfigurationException:`  
> `The current proxy generator can not intercept the specified method for the following reason:`  
> `- Extension methods can not be intercepted since they're static`.

### Logging assertions

There are two methods to assert log events, `MustHaveLogged` and `MustHaveNotLogged`. Both methods have some overloads to specify which kind of event the assertion has to look for.

```csharp
IMogger<MyService> logger = Mogger<MyService>.Create(_testOutputHelper);

// Asserts that one or more events have been raised.
logger.MustHaveLogged();

// Asserts that one or more events at the given level have been raised.
logger.MustHaveLogged(LogLevel.Information);

// Assers that one or more events that satisfies the predicate have been raised.
// 'args' is the ArgumentCollection of the invoked method.
logger.MustHaveLogged(args => args.Get<Exception>("exception") is ArgumentException);

// Asserts that no event has been raised.
logger.MustNotHaveLogged();

// Asserts that no event at the given level has been raised.
logger.MustNotHaveLogged(LogLevel.Information);

// Asserts that no event satisfying the predicate has been raised.
logger.MustNotHaveLogged(args => args.Get<Exception>("exception") is ArgumentException);
```

> On top of that, use the `AnyLogEvent`, `AnyLogEventAt` and `AnyLogEventThat` extension methods to get a log event call representation to use the [FakeItEasy][2] API to it.
>
> ```csharp
> public static class MyMoggerExtensions
> {
>     public static void MustHaveLoggedThreeTimesExactly(this IMockLogger logger)
>     {
>         logger
>             .AnyLogEvent()
>             .MustHaveHappened(3, Times.Exactly);
>     }
>     
>     public static void MustHaveLoggedAtInformationOnceOrMore(this IMockLogger logger)
>     {
>         logger
>             .AnyLogEventAt(LogLevel.Information)
>             .MustHaveHappened(1, Times.OrMore);
>     }
>     
>     public static void MustNotHaveLoggedErrorWithNullException(this IMockLogger logger)
>     {
>         logger.AnyLogEventThat(args =>
>         {
>             var level = args.Get<LogLevel>("logLevel");
>             var exception = args.Get<Exception>("exception");
>                 
>             return
>                 level.Equals(LogLevel.Error) &&
>                 exception is null;
>         })
>         .MustNotHaveHappened();
>     }
>     
>     public static void MustNotHaveLoggedEmptyMessage(this IMockLogger logger)
>     {
>         logger.AnyLogEventThat(args =>
>         {
>             var message = args.Get<object>("state")?.ToString();
>             return string.IsNullOrEmpty(message);
>         })
>         .MustNotHaveHappened();
>     }
> }
> ```
>
> Check the [`ILogger.Log` method][3] header to see what arguments the method has in order to implement your own log calls assertions.

[1]: https://xunit.net/	"About xUnit"
[2]: https://fakeiteasy.github.io/	"About FakeItEasy"
[3]: https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.logging.ilogger.log?view=dotnet-plat-ext-6.0#microsoft-extensions-logging-ilogger-log-1(microsoft-extensions-logging-loglevel-microsoft-extensions-logging-eventid-0-system-exception-system-func((-0-system-exception-system-string)))	"Microsoft.Extensions.Logging.ILogger.Log method reference"

