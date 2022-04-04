using Microsoft.Extensions.Logging;
using Mogger.Tests.Service;
using Xunit;
using Xunit.Abstractions;

namespace Mogger.Tests;

public class MoggerAssertionsTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public MoggerAssertionsTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void OnLogging_WithService_AtInfo_Log_IsCalled()
    {
        // Arrange
        var logger = Mogger<FakeService>.Create(_testOutputHelper);
        var service = new FakeService(logger);

        // Act
        service.DoWork();

        // Assert
        logger.MustHaveLogged(LogLevel.Information);
    }

    [Fact]
    public void OnLogging_WithService_Log_IsCalled()
    {
        // Arrange
        var logger = Mogger<FakeService>.Create(_testOutputHelper);
        var service = new FakeService(logger);

        // Act
        service.DoWork();

        // Assert
        logger.MustHaveLogged();
    }

    [Fact]
    public void OnLogging_WithService_MatchingPredicate_IsSatisfied()
    {
        // Arrange
        var logger = Mogger<FakeService>.Create(_testOutputHelper);
        var service = new FakeService(logger);

        // Act
        service.DoWork();

        // Assert
        logger.MustHaveLogged(args =>
        {
            var message = args.Get<object>("state");
            return message?.ToString() == "Doing some work...";
        });
    }

    [Fact]
    public void OnLogging_WithService_MismatchingPredicate_IsNotSatisfied()
    {
        // Arrange
        var logger = Mogger<FakeService>.Create(_testOutputHelper);
        var service = new FakeService(logger);

        // Act
        service.DoWork();

        // Assert
        logger.MustNotHaveLogged(args =>
        {
            var message = args.Get<object>("state");
            return message?.ToString() == "Definitely not the same message";
        });
    }

    [Theory]
    [InlineData(LogLevel.Trace)]
    [InlineData(LogLevel.Debug)]
    [InlineData(LogLevel.Warning)]
    [InlineData(LogLevel.Error)]
    [InlineData(LogLevel.Critical)]
    public void OnLogging_WithService_NotAtInfo_Log_IsNotCalled(LogLevel level)
    {
        // Arrange
        var logger = Mogger<FakeService>.Create(_testOutputHelper);
        var service = new FakeService(logger);

        // Act
        service.DoWork();

        // Assert
        logger.MustNotHaveLogged(level);
    }

    [Fact]
    public void OnNotLogging_WithService_Log_IsNotCalled()
    {
        // Arrange
        var logger = Mogger<FakeService>.Create(_testOutputHelper);
        var service = new FakeService(logger);

        // Act
        service.DoNothing();

        // Assert
        logger.MustNotHaveLogged();
    }
}