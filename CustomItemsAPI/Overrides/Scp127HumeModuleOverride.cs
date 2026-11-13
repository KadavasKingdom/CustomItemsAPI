using InventorySystem.Items.Firearms.Modules.Scp127;
using System.Diagnostics.CodeAnalysis;

namespace CustomItemsAPI;

/// <summary>
/// Override class for <see cref="Scp127HumeModule"/>.
/// </summary>
public class Scp127HumeModuleOverride : BaseOverride<Scp127HumeModule>
{
	/// <summary>
	/// Gets or sets the override for <see cref="Scp127HumeModule._maxPerTier"/>.
	/// </summary>
	public Dictionary<Scp127Tier, float>? TierToMaxShield { get; set; }

	/// <inheritdoc/>
	public override void Apply([NotNull] ref Scp127HumeModule classToOverride)
	{
		if (TierToMaxShield == null)
		{
			return;
		}

		classToOverride._maxPerTier = [.. TierToMaxShield.Select(x => new Scp127HumeModule.MaxHumeTierPair() { Tier = x.Key, MaxShield = x.Value })];
	}
}
