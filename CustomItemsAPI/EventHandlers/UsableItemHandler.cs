using CustomItemsAPI.Events;
using CustomItemsAPI.Items;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;

namespace CustomItemsAPI.EventHandlers;

/// <summary>
/// Represent a handle that process usable item events.
/// </summary>
internal sealed class UsableItemHandler : CustomEventsHandler
{
	/// <inheritdoc/>
	public override void OnPlayerCancellingUsingItem(PlayerCancellingUsingItemEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.UsableItem, out CustomUsableBase? cur_item))
		{
			return;
		}

		TypeWrapper<bool> isAllowed = new(ev.IsAllowed);
		CustomUsableEvents.OnCancelling(cur_item, ev.Player, ev.UsableItem, isAllowed);
		cur_item?.OnCancelling(ev.Player, ev.UsableItem, isAllowed);
		ev.IsAllowed = isAllowed.Value;
	}

	/// <inheritdoc/>
	public override void OnPlayerCancelledUsingItem(PlayerCancelledUsingItemEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.UsableItem, out CustomUsableBase? cur_item))
		{
			return;
		}

		CustomUsableEvents.OnCancelled(cur_item, ev.Player, ev.UsableItem);
		cur_item.OnCancelled(ev.Player, ev.UsableItem);
	}

	/// <inheritdoc/>
	public override void OnPlayerUsingItem(PlayerUsingItemEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.UsableItem, out CustomUsableBase? cur_item))
		{
			return;
		}

		TypeWrapper<bool> isAllowed = new(ev.IsAllowed);
		CustomUsableEvents.OnUsing(cur_item, ev.Player, ev.UsableItem, isAllowed);
		cur_item?.OnUsing(ev.Player, ev.UsableItem, isAllowed);
		ev.IsAllowed = isAllowed.Value;
	}

	/// <inheritdoc/>
	public override void OnPlayerUsedItem(PlayerUsedItemEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.UsableItem, out CustomUsableBase? cur_item))
		{
			return;
		}

		CustomUsableEvents.OnUsed(cur_item, ev.Player, ev.UsableItem);
		cur_item.OnUsed(ev.Player, ev.UsableItem);
	}
}
