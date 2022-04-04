using FakeItEasy;
using Microsoft.Extensions.Logging;
using Mogger.Tests.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Mogger.Tests;

public class MoggerCallExtensionsTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public MoggerCallExtensionsTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void OnLogging_WithService_Twice_CallExpression_Matches()
    {
        // Arrange
        var logger = Mogger<FakeService>.Create(_testOutputHelper);
        var service = new FakeService(logger);

        // Act
        service.DoWork();
        service.DoWork();

        // Assert
        logger.AnyLogEvent().MustHaveHappened(2, Times.Exactly);
    }

    [Fact]
    public void OnLogging_WithService_Twice_CallExpression_AtLevel_Matches()
    {
        // Arrange
        var logger = Mogger<FakeService>.Create(_testOutputHelper);
        var service = new FakeService(logger);

        // Act
        service.DoWork();
        service.DoWork();

        // Assert
        logger.AnyLogEventAt(LogLevel.Information).MustHaveHappened(2, Times.Exactly);
    }

    [Fact]
    public void OnLogging_WithService_Twice_CallExpression_WithPredicate_Matches()
    {
        // Arrange
        var logger = Mogger<FakeService>.Create(_testOutputHelper);
        var service = new FakeService(logger);

        // Act
        service.DoWork();
        service.DoWork();

        // Assert
        logger.AnyLogEventThat(args =>
        {
            var level = args.Get<LogLevel>("logLevel");
            return level is not LogLevel.Information;
        }).MustNotHaveHappened();
    }
}
