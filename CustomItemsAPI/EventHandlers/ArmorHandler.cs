using CustomItemsAPI.Events;
using CustomItemsAPI.Items;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;

namespace CustomItemsAPI.EventHandlers;

/// <summary>
/// Represent a handle that process armor events.
/// </summary>
internal sealed class ArmorHandler : CustomEventsHandler
{
	/// <inheritdoc/>
	public override void OnPlayerPickingUpArmor(PlayerPickingUpArmorEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.BodyArmorPickup, out CustomItemBase? cur_item))
		{
			return;
		}

		TypeWrapper<bool> isAllowedHelper = new(ev.IsAllowed);
		CustomItemEvents.OnPicking(cur_item, ev.Player, ev.BodyArmorPickup, isAllowedHelper);
		cur_item.OnPicking(ev.Player, ev.BodyArmorPickup, isAllowedHelper);
		ev.IsAllowed = isAllowedHelper;
	}

	/// <inheritdoc/>
	public override void OnPlayerPickedUpArmor(PlayerPickedUpArmorEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.BodyArmorItem, out CustomItemBase? cur_item))
		{
			return;
		}

		CustomItemEvents.OnPicked(cur_item, ev.Player, ev.BodyArmorItem);
		cur_item.OnPicked(ev.Player, ev.BodyArmorItem);
	}

	/// <inheritdoc/>
	public override void OnPlayerSearchingArmor(PlayerSearchingArmorEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.BodyArmorPickup, out CustomItemBase? cur_item))
		{
			return;
		}

		TypeWrapper<bool> isAllowedHelper = new(ev.IsAllowed);
		CustomItemEvents.OnSearching(cur_item, ev.Player, ev.BodyArmorPickup, isAllowedHelper);
		cur_item.OnSearching(ev.Player, ev.BodyArmorPickup, isAllowedHelper);
		ev.IsAllowed = isAllowedHelper.Value;
	}

	/// <inheritdoc/>
	public override void OnPlayerSearchedArmor(PlayerSearchedArmorEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.BodyArmorPickup, out CustomItemBase? cur_item))
		{
			return;
		}

		CustomItemEvents.OnSearched(cur_item, ev.Player, ev.BodyArmorPickup);
		cur_item.OnSearched(ev.Player, ev.BodyArmorPickup);
	}
}
