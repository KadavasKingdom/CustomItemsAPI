using System.Diagnostics.CodeAnalysis;

namespace CustomItemsAPI.Items;

/// <summary>
/// Custom <see cref="CoinItem"/> base.
/// </summary>
public abstract class CustomCoinBase : CustomItemBase
{
	/// <inheritdoc/>
	public override void Parse(Item item)
	{
		base.Parse(item);
		if (item is not CoinItem)
		{
			throw new ArgumentException("CoinItem must not be null!");
		}
	}

	/// <summary>
	/// Called when a <paramref name="coinItem"/> is being flipped.
	/// </summary>
	/// <param name="player">The Player who called this function.</param>
	/// <param name="coinItem">The Custom Coin Item.</param>
	/// <param name="isTails">Is resulting in tails.</param>
	/// <param name="isAllowed">Can allow this action.</param>
	public virtual void OnFlipping(Player player, CoinItem coinItem, [NotNull] TypeWrapper<bool> isTails, [NotNull] TypeWrapper<bool> isAllowed)
	{
	}

	/// <summary>
	/// Called when a <paramref name="coinItem"/> has been flipped.
	/// </summary>
	/// <param name="player">The Player who called this function.</param>
	/// <param name="coinItem">The Custom Coin Item.</param>
	/// <param name="isTails">Resulted in tails.</param>
	public virtual void OnFlipped(Player player, CoinItem coinItem, bool isTails)
	{
	}
}
