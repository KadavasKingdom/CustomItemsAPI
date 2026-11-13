using InventorySystem.Items.Firearms.Modules;
using System.Diagnostics.CodeAnalysis;

namespace CustomItemsAPI;

/// <summary>
/// Override class for <see cref="DisruptorHitregModule"/>.
/// </summary>
public class DisruptorHitregModuleOverride : BaseOverride<DisruptorHitregModule>
{
	/// <summary>
	/// Gets or sets the changes for <see cref="DisruptorHitregModule._singleShotBaseDamage"/>.
	/// </summary>
	public MathValueFloat SingleShotBaseDamage { get; set; } = new();

	/// <summary>
	/// Gets or sets the changes for <see cref="DisruptorHitregModule._singleShotFalloffDistance"/>.
	/// </summary>
	public MathValueFloat SingleShotFalloffDistance { get; set; } = new();

	/// <summary>
	/// Gets or sets the changes for <see cref="DisruptorHitregModule._singleShotDivisionPerTarget"/>.
	/// </summary>
	public MathValueFloat SingleShotDivisionPerTarget { get; set; } = new();

	/// <summary>
	/// Gets or sets the changes for <see cref="DisruptorHitregModule._singleShotThickness"/>.
	/// </summary>
	public MathValueFloat SingleShotThickness { get; set; } = new();

	/// <summary>
	/// Gets or sets the changes for <see cref="DisruptorHitregModule._singleShotExplosionSettings"/>.
	/// </summary>
	public ExplosionGrenadeOverride SingleShotExplosionSettings { get; set; } = new();

	/// <summary>
	/// Gets or sets the changes for <see cref="DisruptorHitregModule._rapidFireBaseDamage"/>.
	/// </summary>
	public MathValueFloat RapidFireBaseDamage { get; set; } = new();

	/// <summary>
	/// Gets or sets the changes for <see cref="DisruptorHitregModule._rapidFireFalloffDistance"/>.
	/// </summary>
	public MathValueFloat RapidFireFalloffDistance { get; set; } = new();

	/// <inheritdoc/>
	public override void Apply([NotNull] ref DisruptorHitregModule disruptorHitregModule)
	{
		SingleShotBaseDamage.MathCalculation(ref disruptorHitregModule._singleShotBaseDamage);
		SingleShotFalloffDistance.MathCalculation(ref disruptorHitregModule._singleShotFalloffDistance);
		SingleShotDivisionPerTarget.MathCalculation(ref disruptorHitregModule._singleShotDivisionPerTarget);
		SingleShotThickness.MathCalculation(ref disruptorHitregModule._singleShotThickness);
		SingleShotExplosionSettings.Apply(ref disruptorHitregModule._singleShotExplosionSettings);
		RapidFireBaseDamage.MathCalculation(ref disruptorHitregModule._rapidFireBaseDamage);
		RapidFireFalloffDistance.MathCalculation(ref disruptorHitregModule._rapidFireFalloffDistance);
	}
}
