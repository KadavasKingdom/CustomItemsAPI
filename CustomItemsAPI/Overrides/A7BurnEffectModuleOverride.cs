using InventorySystem.Items.Firearms.Modules;
using System.Diagnostics.CodeAnalysis;

namespace CustomItemsAPI;

/// <summary>
/// Override class for <see cref="A7BurnEffectModule"/>.
/// </summary>
public class A7BurnEffectModuleOverride : BaseOverride<A7BurnEffectModule>
{
	/// <summary>
	/// Gets or sets the max duration for the Burned effect.
	/// </summary>
	public MathValueInt MaxDuration { get; set; } = new();

	/// <summary>
	/// Gets or sets the per shot duration.
	/// </summary>
	public MathValueInt PerShotDuration { get; set; } = new();

	/// <summary>
	/// Gets or sets the forward offset for checking the range.
	/// </summary>
	public MathValueFloat ForwardOffset { get; set; } = new();

	/// <summary>
	/// Gets or sets the radius for checking the hit range.
	/// </summary>
	public MathValueFloat Radius { get; set; } = new();

	/// <inheritdoc/>
	public override void Apply([NotNull] ref A7BurnEffectModule burnEffectModule)
	{
		MaxDuration.MathCalculation(ref burnEffectModule._maxDuration);
		PerShotDuration.MathCalculation(ref burnEffectModule._perShotDuration);
		ForwardOffset.MathCalculation(ref burnEffectModule._forwardOffset);
		Radius.MathCalculation(ref burnEffectModule._radius);
	}
}
