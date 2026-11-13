using System.Diagnostics.CodeAnalysis;

namespace CustomItemsAPI;

/// <summary>
/// Used in places where can something be easily overriden.
/// </summary>
/// <typeparam name="T">Any type to override.</typeparam>
public interface IOverride<T> : IOverride
{
	/// <summary>
	/// Apply override class for <paramref name="classToOverride"/>.
	/// </summary>
	/// <param name="classToOverride">The item to override.</param>
	public void Apply([NotNull] ref T classToOverride);
}
