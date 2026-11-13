using InventorySystem.Items.ThrowableProjectiles;
using System.Diagnostics.CodeAnalysis;

namespace CustomItemsAPI;

/// <summary>
/// Override class for <see cref="TimeGrenade"/>.
/// </summary>
public class TimeGrenadeOverride : BaseOverride<TimeGrenade>
{
	/// <summary>
	/// Gets or sets the changes for <see cref="TimeGrenade._fuseTime"/>.
	/// </summary>
	public MathValueFloat FuseTime { get; set; } = new();

	/// <inheritdoc/>
	public override void Apply([NotNull] ref TimeGrenade classToOverride)
	{
		FuseTime.MathCalculation(ref classToOverride._fuseTime);
	}
}
