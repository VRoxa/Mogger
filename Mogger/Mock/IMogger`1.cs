
namespace Mogger;

/// <inheritdoc cref="IMogger"/>
/// <typeparam name="T">The logger category type.</typeparam>
public interface IMogger<out T> : IMogger, ILogger<T>
{
}
