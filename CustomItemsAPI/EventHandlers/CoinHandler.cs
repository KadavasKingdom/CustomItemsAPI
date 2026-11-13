using CustomItemsAPI.Events;
using CustomItemsAPI.Items;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;

namespace CustomItemsAPI.EventHandlers;

/// <summary>
/// Represent a handle that process coin events.
/// </summary>
internal sealed class CoinHandler : CustomEventsHandler
{
	/// <inheritdoc/>
	public override void OnPlayerFlippingCoin(PlayerFlippingCoinEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.CoinItem, out CustomCoinBase? cur_item))
		{
			return;
		}

		TypeWrapper<bool> isTailsHelper = new(ev.IsTails);
		TypeWrapper<bool> isAllowedHelper = new(ev.IsAllowed);
		CustomCoinEvents.OnFlipping(cur_item, ev.Player, ev.CoinItem, isTailsHelper, isAllowedHelper);
		cur_item.OnFlipping(ev.Player, ev.CoinItem, isTailsHelper, isAllowedHelper);
		ev.IsTails = isTailsHelper.Value;
		ev.IsAllowed = isAllowedHelper.Value;
	}

	/// <inheritdoc/>
	public override void OnPlayerFlippedCoin(PlayerFlippedCoinEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.CoinItem, out CustomCoinBase? cur_item))
		{
			return;
		}

		CustomCoinEvents.OnFlipped(cur_item, ev.Player, ev.CoinItem, ev.IsTails);
		cur_item.OnFlipped(ev.Player, ev.CoinItem, ev.IsTails);
	}
}
