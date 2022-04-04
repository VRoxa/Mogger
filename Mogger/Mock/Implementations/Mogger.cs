
using Xunit.Abstractions;

namespace Mogger;

/// <inheritdoc cref="IMogger"/>
public class Mogger : IMogger
{
    private readonly ITestOutputHelper _testOutputHelper;

    /// <summary>
    /// Initializes a new instance of the <see cref="Mogger"/> class.
    /// </summary>
    /// <param name="testOutputHelper">Class used to provide test output.</param>
    protected Mogger(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        Proxied = A.Fake<ILogger>();
    }

    /// <inheritdoc/>
    public ILogger Proxied { get; }

    /// <summary>
    /// Creates a new <see cref="IMogger"/> instance.
    /// </summary>
    /// <param name="testOutputHelper">Class used to provide test output.</param>
    /// <returns>An <see cref="IMogger"/> instance.</returns>
    public static IMogger Create(ITestOutputHelper testOutputHelper)
    {
        return new Mogger(testOutputHelper);
    }

    /// <inheritdoc/>
    public IDisposable BeginScope<TState>(TState state) => Proxied.BeginScope(state);

    /// <inheritdoc/>
    public bool IsEnabled(LogLevel logLevel) => Proxied.IsEnabled(logLevel);

    /// <inheritdoc/>
    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        var message = formatter(state, exception);
        var lvl = logLevel switch
        {
            LogLevel.Trace => "TRACE",
            LogLevel.Debug => "DEBUG",
            LogLevel.Information => "INFO",
            LogLevel.Warning => "WARN",
            LogLevel.Error => "ERROR",
            LogLevel.Critical => "FATAL",
            _ => string.Empty,
        };

        // TODO - Allow customized output template
        var formattedException = FormatException(exception);
        _testOutputHelper.WriteLine($"{DateTime.Now:HH:mm:ss.fff} {lvl,-5} [{Environment.CurrentManagedThreadId,-4}]: {message}{formattedException}");
        Proxied.Log(logLevel, eventId, state, exception, formatter);
    }

    private static string FormatException(Exception? ex)
    {
        // IOException: File not found
        //   at Example.MyService.Copy()
        return $"\n{ex?.GetType()?.Name}: {ex?.Message}\n{ex?.StackTrace}".TrimEnd('\n');
    }
}
