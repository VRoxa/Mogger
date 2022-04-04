using FakeItEasy;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;
using Mogger.Tests.Service;

namespace Mogger.Tests;

public class MoggerTests
{
    [Fact]
    public void OnLogging_Xunit_TestOutputHelper_IsUsed()
    {
        // Arrange
        var fakeOutputHelper = A.Fake<ITestOutputHelper>();
        ILogger sut = Mogger.Create(fakeOutputHelper);

        // Act
        sut.LogInformation("Testing");

        // Assert
        A.CallTo(() => fakeOutputHelper.WriteLine(A<string>.That.Contains("Testing")))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void OnLogging_Generic_Xunit_TestOutputHelper_IsUsed()
    {
        // Arrange
        var fakeOutputHelper = A.Fake<ITestOutputHelper>();
        ILogger sut = Mogger<MoggerTests>.Create(fakeOutputHelper);

        // Act
        sut.LogInformation("Testing");

        // Assert
        A.CallTo(() => fakeOutputHelper.WriteLine(A<string>.That.Contains("Testing")))
            .MustHaveHappenedOnceExactly();
    }
}
