using System.Diagnostics.CodeAnalysis;

namespace CustomItemsAPI;

/// <summary>
/// Represent a basic <see cref="IOverride{T}"/>.
/// </summary>
/// <typeparam name="T">Any type.</typeparam>
public abstract class BaseOverride<T> : IOverride<T>
{
	/// <inheritdoc/>
	public virtual Type OverrideType => typeof(T);

	/// <inheritdoc/>
	public abstract void Apply([NotNull] ref T classToOverride);

	/// <inheritdoc/>
	public virtual void Apply(ref object classToOverride)
	{
		if (classToOverride is not T)
		{
			return;
		}

		T overrides = (T)classToOverride;
		Apply(ref overrides);
	}
}
