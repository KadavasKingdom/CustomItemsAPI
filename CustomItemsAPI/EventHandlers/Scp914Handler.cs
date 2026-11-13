using CustomItemsAPI.Events;
using CustomItemsAPI.Items;
using LabApi.Events.Arguments.Scp914Events;
using LabApi.Events.CustomHandlers;
using Scp914;
using UnityEngine;

namespace CustomItemsAPI.EventHandlers;

/// <summary>
/// Represent a handle that process SCP-914 events.
/// </summary>
internal sealed class Scp914Handler : CustomEventsHandler
{
	/// <inheritdoc/>
	public override void OnScp914ProcessingInventoryItem(Scp914ProcessingInventoryItemEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.Item, out CustomItemBase? cur_item))
		{
			return;
		}

		TypeWrapper<bool> isAllowed = new(ev.IsAllowed);
		TypeWrapper<Scp914KnobSetting> knobSetting = new(ev.KnobSetting);
		CustomItemEvents.OnProcessingItem(cur_item, ev.Player, ev.Item, knobSetting, isAllowed);
		cur_item.OnProcessingItem(ev.Player, ev.Item, knobSetting, isAllowed);
		ev.IsAllowed = isAllowed.Value;
		ev.KnobSetting = knobSetting.Value;
	}

	/// <inheritdoc/>
	public override void OnScp914ProcessingPickup(Scp914ProcessingPickupEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.Pickup, out CustomItemBase? cur_item))
		{
			return;
		}

		TypeWrapper<bool> isAllowed = new(ev.IsAllowed);
		TypeWrapper<Scp914KnobSetting> knobSetting = new(ev.KnobSetting);
		TypeWrapper<Vector3> newPosition = new(ev.NewPosition);
		CustomItemEvents.OnProcessingPickup(cur_item, ev.Pickup, knobSetting, newPosition, isAllowed);
		cur_item.OnProcessingPickup(ev.Pickup, knobSetting, newPosition, isAllowed);
		ev.IsAllowed = isAllowed.Value;
		ev.KnobSetting = knobSetting.Value;
		ev.NewPosition = newPosition.Value;
	}
}
