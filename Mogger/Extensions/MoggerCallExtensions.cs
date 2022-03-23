
using FakeItEasy.Configuration;

namespace Mogger;

/// <summary>
/// Methods that extend the <see cref="IMogger"/> to return representations of log invocations.
/// </summary>
public static class MoggerCallExtensions
{
    /// <summary>
    /// Gets the representation of any log event the given <see cref="IMogger"/> instance has ever invoked.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> mock.</param>
    /// <returns>The configuration object.</returns>
    public static IVoidConfiguration AnyLogEvent(this IMogger logger)
    {
        return logger
            .LogCall()
            .WithAnyArguments();
    }

    /// <summary>
    /// Gets the representation of any log event the given <see cref="IMogger"/> instance has ever invoked at the specified level.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> mock.</param>
    /// <param name="logLevel">The log level event to check for.</param>
    /// <returns>The configuration object.</returns>
    public static IVoidConfiguration AnyLogEventAt(this IMogger logger, LogLevel logLevel)
    {
        return logger
            .LogCall()
            .WithLogLevel(logLevel);
    }

    /// <summary>
    /// Gets the representation of any log event the given <see cref="IMogger"/> instance has ever invoked.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> mock.</param>
    /// <param name="predicate">The predicate that checks the conditions of the log event call to check.</param>
    /// <returns>The configuration object.</returns>
    public static IVoidConfiguration AnyLogEventThat(this IMogger logger, Func<ArgumentCollection, bool> predicate)
    {
        return logger
            .LogCall()
            .WhenArgumentsMatch(predicate);
    }
}
