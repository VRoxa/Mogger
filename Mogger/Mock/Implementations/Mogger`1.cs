
using Xunit.Abstractions;

namespace Mogger;

/// <inheritdoc/>
public class Mogger<T> : Mogger, IMogger<T>
{
    private Mogger(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper)
    {
    }

    /// <summary>
    /// Creates a new <see cref="IMogger{T}"/> instance.
    /// </summary>
    /// <param name="testOutputHelper">Class used to provide test output.</param>
    /// <returns>An <see cref="IMogger{T}"/> instance.</returns>
    public static new IMogger<T> Create(ITestOutputHelper testOutputHelper)
    {
        return new Mogger<T>(testOutputHelper);
    }
}
