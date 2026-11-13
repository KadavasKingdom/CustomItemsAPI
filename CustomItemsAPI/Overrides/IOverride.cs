namespace CustomItemsAPI;

/// <summary>
/// Used in places where can something be easily overriden.
/// </summary>
public interface IOverride
{
	/// <summary>
	/// Gets type to override.
	/// </summary>
	public Type OverrideType { get; }

	/// <summary>
	/// Apply override class for <paramref name="classToOverride"/>.
	/// </summary>
	/// <param name="classToOverride">The class to override.</param>
	public void Apply(ref object classToOverride);
}
