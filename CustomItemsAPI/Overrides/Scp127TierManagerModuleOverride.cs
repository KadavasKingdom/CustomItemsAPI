using InventorySystem.Items.Firearms.Modules.Scp127;
using System.Diagnostics.CodeAnalysis;

namespace CustomItemsAPI;

/// <summary>
/// Override class for <see cref="Scp127TierManagerModule"/>.
/// </summary>
public class Scp127TierManagerModuleOverride : BaseOverride<Scp127TierManagerModule>
{
	/// <summary>
	/// Gets or sets the override for <see cref="Scp127TierManagerModule.Thresholds"/>.
	/// </summary>
	public Dictionary<Scp127Tier, float>? TierToRequiredDamage { get; set; }

	/// <inheritdoc/>
	public override void Apply([NotNull] ref Scp127TierManagerModule classToOverride)
	{
		if (TierToRequiredDamage == null)
		{
			return;
		}

		classToOverride.Thresholds = [.. TierToRequiredDamage.Select(x => new Scp127TierManagerModule.TierThreshold() { Tier = x.Key, RequiredDamage = x.Value })];
	}
}
