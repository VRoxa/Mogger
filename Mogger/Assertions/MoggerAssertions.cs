
using FakeItEasy.Configuration;

namespace Mogger;

/// <summary>
/// Methods that extend the <see cref="IMogger"/> interface and provide logging related assertions.
/// </summary>
public static class MoggerAssertions
{
    /// <summary>
    /// Asserts that the given <see cref="IMogger"/> instance hasn't logged any event.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> mock.</param>
    public static void MustNotHaveLogged(this IMogger logger)
    {
        logger
            .LogCall()
            .WithAnyArguments()
            .MustNotHaveHappened();
    }

    /// <summary>
    /// Asserts that the given <see cref="IMogger"/> instance hasn't logged any event at the specified level.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> mock.</param>
    /// <param name="logLevel">The log level event to check for.</param>
    public static void MustNotHaveLogged(this IMogger logger, LogLevel logLevel)
    {
        logger
            .LogCall()
            .WithLogLevel(logLevel)
            .MustNotHaveHappened();
    }

    /// <summary>
    /// Asserts that the given <see cref="IMogger"/> instance hasn't logged any event that satisfies the condition.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> mock.</param>
    /// <param name="predicate">The predicate that checks the conditions of the log event call to check.</param>
    public static void MustNotHaveLogged(this IMogger logger, Func<ArgumentCollection, bool> predicate)
    {
        logger
            .LogCall()
            .WhenArgumentsMatch(predicate)
            .MustNotHaveHappened();
    }

    /// <summary>
    /// Asserts that the given <see cref="IMogger"/> instance has logged any event at least once.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> mock.</param>
    public static void MustHaveLogged(this IMogger logger)
    {
        logger
            .LogCall()
            .WithAnyArguments()
            .MustHaveHappened();
    }

    /// <summary>
    /// Asserts that the given <see cref="IMogger"/> instance has logged any event at least once at the specified level.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> mock.</param>
    /// <param name="logLevel">The log level event to check for.</param>
    public static void MustHaveLogged(this IMogger logger, LogLevel logLevel)
    {
        logger
            .LogCall()
            .WithLogLevel(logLevel)
            .MustHaveHappened();
    }

    /// <summary>
    /// Asserts that the given <see cref="IMogger"/> instance has logged any event that satisfies the condition at least once.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> mock.</param>
    /// <param name="predicate">The predicate that checks the conditions of the log event call to check.</param>
    public static void MustHaveLogged(this IMogger logger, Func<ArgumentCollection, bool> predicate)
    {
        logger
            .LogCall()
            .WhenArgumentsMatch(predicate)
            .MustHaveHappened();
    }
}
