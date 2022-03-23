
using FakeItEasy.Configuration;
using FakeItEasy.Core;

namespace Mogger;

/// <summary>
/// Methods that extend the FakeItEasy configuration for <see cref="IMogger"/> assertions utility purposes.
/// </summary>
internal static class FakeItEasyConfigurationExtensions
{
    /// <summary>
    /// Gets the representation of any invocation of the <see cref="ILogger.Log{TState}(LogLevel, EventId, TState, Exception?, Func{TState, Exception?, string})"/> method.
    /// </summary>
    /// <param name="logger">The <see cref="IMogger"/> instance that invokes the method.</param>
    /// <returns>The representation of a log event invocation.</returns>
    internal static IArgumentValidationConfiguration<IVoidConfiguration> LogCall(this IMogger logger)
    {
        var predicate = (IFakeObjectCall call) =>
            call.Method.IsGenericMethod &&
            call.Method.Name == nameof(ILogger.Log);

        // TODO - Check IOutputWriter, 2nd parameter
        return A.CallTo(logger.Proxied).Where(predicate, _ => { });
    }

    /// <summary>
    /// Filters the log event invocation by the specified log level.
    /// </summary>
    /// <param name="call">The representation of the log event invocations.</param>
    /// <param name="logLevel">The log event level to filter by.</param>
    /// <returns>The configuration object.</returns>
    internal static IVoidConfiguration WithLogLevel(this IArgumentValidationConfiguration<IVoidConfiguration> call, LogLevel logLevel)
    {
        return call.WhenArgumentsMatch(args =>
        {
            return args.Get<LogLevel>(nameof(logLevel)).Equals(logLevel);
        });
    }
}
