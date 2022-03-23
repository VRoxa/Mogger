
namespace Mogger;

/// <summary>
/// Representation of a mocked <see cref="ILogger"/> instance.
/// </summary>
public interface IMogger : ILogger
{
    /// <summary>
    /// Gets the proxied mock <see cref="ILogger"/> instance.
    /// </summary>
    /// <remarks>
    /// The proxied instance can be used to add extra customized
    /// behavior or custom invocations assertions.
    /// </remarks>
    public ILogger Proxied { get; }
}
