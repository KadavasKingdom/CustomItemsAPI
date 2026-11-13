using CustomItemsAPI.Events;
using CustomItemsAPI.Items;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;

namespace CustomItemsAPI.EventHandlers;

/// <summary>
/// Represent a handle that process common items events.
/// </summary>
internal sealed class CommonItemHandler : CustomEventsHandler
{
	/// <inheritdoc/>
	public override void OnPlayerChangingItem(PlayerChangingItemEventArgs ev)
	{
		TypeWrapper<bool> isAllowedHelper = new(ev.IsAllowed);
		if (CustomItems.TryGetCustomItem(ev.OldItem, out CustomItemBase? prev_Item))
		{
			CustomItemEvents.OnChanging(prev_Item, ev.Player, ev.OldItem, ev.NewItem, false, isAllowedHelper);
			prev_Item.OnChanging(ev.Player, ev.OldItem, ev.NewItem, false, isAllowedHelper);
		}

		if (CustomItems.TryGetCustomItem(ev.NewItem, out CustomItemBase? cur_item))
		{
			CustomItemEvents.OnChanging(cur_item, ev.Player, ev.OldItem, ev.NewItem, true, isAllowedHelper);
			cur_item.OnChanging(ev.Player, ev.OldItem, ev.NewItem, true, isAllowedHelper);
		}

		ev.IsAllowed = isAllowedHelper.Value;
	}

	/// <inheritdoc/>
	public override void OnPlayerChangedItem(PlayerChangedItemEventArgs ev)
	{
		if (CustomItems.TryGetCustomItem(ev.OldItem, out CustomItemBase? prev_Item))
		{
			CustomItemEvents.OnChanged(prev_Item, ev.Player, ev.OldItem, ev.NewItem, false);
			prev_Item.OnChanged(ev.Player, ev.OldItem, ev.NewItem, false);
		}

		if (CustomItems.TryGetCustomItem(ev.NewItem, out CustomItemBase? cur_item))
		{
			CustomItemEvents.OnChanged(cur_item, ev.Player, ev.OldItem, ev.NewItem, true);
			cur_item.OnChanged(ev.Player, ev.OldItem, ev.NewItem, true);
		}
	}

	/// <inheritdoc/>
	public override void OnPlayerDroppingItem(PlayerDroppingItemEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.Item, out CustomItemBase? cur_item))
		{
			return;
		}

		TypeWrapper<bool> isThrowHelper = new(ev.Throw);
		TypeWrapper<bool> isAllowedHelper = new(ev.IsAllowed);
		CustomItemEvents.OnDropping(cur_item, ev.Player, ev.Item, isThrowHelper, isAllowedHelper);
		cur_item.OnDropping(ev.Player, ev.Item, isThrowHelper, isAllowedHelper);
		ev.Throw = isThrowHelper.Value;
		ev.IsAllowed = isAllowedHelper.Value;
	}

	/// <inheritdoc/>
	public override void OnPlayerDroppedItem(PlayerDroppedItemEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.Pickup, out CustomItemBase? cur_item))
		{
			return;
		}

		CustomItemEvents.OnDropped(cur_item, ev.Player, ev.Pickup);
		cur_item.OnDropped(ev.Player, ev.Pickup);
	}

	/// <inheritdoc/>
	public override void OnPlayerPickingUpItem(PlayerPickingUpItemEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.Pickup, out CustomItemBase? cur_item))
		{
			return;
		}

		TypeWrapper<bool> isAllowedHelper = new(ev.IsAllowed);
		CustomItemEvents.OnPicking(cur_item, ev.Player, ev.Pickup, isAllowedHelper);
		cur_item.OnPicking(ev.Player, ev.Pickup, isAllowedHelper);
		ev.IsAllowed = isAllowedHelper.Value;
	}

	/// <inheritdoc/>
	public override void OnPlayerPickedUpItem(PlayerPickedUpItemEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.Item, out CustomItemBase? cur_item))
		{
			return;
		}

		CustomItemEvents.OnPicked(cur_item, ev.Player, ev.Item);
		cur_item.OnPicked(ev.Player, ev.Item);
	}

	/// <inheritdoc/>
	public override void OnPlayerThrowingItem(PlayerThrowingItemEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.Pickup, out CustomItemBase? cur_item))
		{
			return;
		}

		TypeWrapper<bool> isAllowedHelper = new(ev.IsAllowed);
		CustomItemEvents.OnThrowing(cur_item, ev.Player, ev.Pickup, ev.Rigidbody, isAllowedHelper);
		cur_item.OnThrowing(ev.Player, ev.Pickup, ev.Rigidbody, isAllowedHelper);
		ev.IsAllowed = isAllowedHelper.Value;
	}

	/// <inheritdoc/>
	public override void OnPlayerThrewItem(PlayerThrewItemEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.Pickup, out CustomItemBase? cur_item))
		{
			return;
		}

		CustomItemEvents.OnThrew(cur_item, ev.Player, ev.Pickup, ev.Rigidbody);
		cur_item.OnThrew(ev.Player, ev.Pickup, ev.Rigidbody);
	}

	/// <inheritdoc/>
	public override void OnPlayerSearchingPickup(PlayerSearchingPickupEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.Pickup, out CustomItemBase? cur_item))
		{
			return;
		}

		TypeWrapper<bool> isAllowedHelper = new(ev.IsAllowed);
		CustomItemEvents.OnSearching(cur_item, ev.Player, ev.Pickup, isAllowedHelper);
		cur_item.OnSearching(ev.Player, ev.Pickup, isAllowedHelper);
		ev.IsAllowed = isAllowedHelper.Value;
	}

	/// <inheritdoc/>
	public override void OnPlayerSearchedPickup(PlayerSearchedPickupEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.Pickup, out CustomItemBase? cur_item))
		{
			return;
		}

		CustomItemEvents.OnSearched(cur_item, ev.Player, ev.Pickup);
		cur_item.OnSearched(ev.Player, ev.Pickup);
	}
}
