using CustomItemsAPI.Events;
using CustomItemsAPI.Items;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;

namespace CustomItemsAPI.EventHandlers;

/// <summary>
/// Represent a handle that process keycard events.
/// </summary>
internal sealed class KeyCardItemHandler : CustomEventsHandler
{
	/// <inheritdoc/>
	public override void OnPlayerInteractingDoor(PlayerInteractingDoorEventArgs ev)
	{
		Item? item = ev.Player.CurrentItem;
		if (item == null)
		{
			return;
		}

		if (!CustomItems.TryGetCustomItem(item, out CustomKeycardBase? customKeyCardBase))
		{
			return;
		}

		TypeWrapper<bool> isAllowedHelper = new(ev.IsAllowed);
		TypeWrapper<bool> canOpenHelper = new(ev.CanOpen);
		CustomKeycardEvents.OnInteractingDoor(customKeyCardBase, ev.Player, item, ev.Door, canOpenHelper, isAllowedHelper);
		customKeyCardBase.OnInteractingDoor(ev.Player, item, ev.Door, canOpenHelper, isAllowedHelper);
		ev.IsAllowed = isAllowedHelper.Value;
		ev.CanOpen = canOpenHelper.Value;
	}

	/// <inheritdoc/>
	public override void OnPlayerInteractedDoor(PlayerInteractedDoorEventArgs ev)
	{
		Item? item = ev.Player.CurrentItem;
		if (item == null)
		{
			return;
		}

		if (!CustomItems.TryGetCustomItem(item, out CustomKeycardBase? customKeyCardBase))
		{
			return;
		}

		CustomKeycardEvents.OnInteractedDoor(customKeyCardBase, ev.Player, item, ev.Door, ev.CanOpen);
		customKeyCardBase.OnInteractedDoor(ev.Player, item, ev.Door, ev.CanOpen);
	}

	/// <inheritdoc/>
	public override void OnPlayerInteractingGenerator(PlayerInteractingGeneratorEventArgs ev)
	{
		Item? item = ev.Player.CurrentItem;
		if (item == null)
		{
			return;
		}

		if (!CustomItems.TryGetCustomItem(item, out CustomKeycardBase? customKeyCardBase))
		{
			return;
		}

		TypeWrapper<bool> isAllowedHelper = new(ev.IsAllowed);
		TypeWrapper<MapGeneration.Distributors.Scp079Generator.GeneratorColliderId> colliderIdHelper = new(ev.ColliderId);
		CustomKeycardEvents.OnInteractingGenerator(customKeyCardBase, ev.Player, item, ev.Generator, colliderIdHelper, isAllowedHelper);
		customKeyCardBase.OnInteractingGenerator(ev.Player, item, ev.Generator, colliderIdHelper, isAllowedHelper);
		ev.IsAllowed = isAllowedHelper.Value;
		ev.ColliderId = colliderIdHelper.Value;
	}

	/// <inheritdoc/>
	public override void OnPlayerInteractedGenerator(PlayerInteractedGeneratorEventArgs ev)
	{
		Item? item = ev.Player.CurrentItem;
		if (item == null)
		{
			return;
		}

		if (!CustomItems.TryGetCustomItem(item, out CustomKeycardBase? customKeyCardBase))
		{
			return;
		}

		CustomKeycardEvents.OnInteractedGenerator(customKeyCardBase, ev.Player, item, ev.Generator, ev.ColliderId);
		customKeyCardBase.OnInteractedGenerator(ev.Player, item, ev.Generator, ev.ColliderId);
	}

	/// <inheritdoc/>
	public override void OnPlayerInteractingLocker(PlayerInteractingLockerEventArgs ev)
	{
		Item? item = ev.Player.CurrentItem;
		if (item == null)
		{
			return;
		}

		if (!CustomItems.TryGetCustomItem(item, out CustomKeycardBase? customKeyCardBase))
		{
			return;
		}

		TypeWrapper<bool> isAllowedHelper = new(ev.IsAllowed);
		TypeWrapper<bool> canOpenHelper = new(ev.CanOpen);
		CustomKeycardEvents.OnInteractingLocker(customKeyCardBase, ev.Player, item, ev.Locker, ev.Chamber, canOpenHelper, isAllowedHelper);
		customKeyCardBase.OnInteractingLocker(ev.Player, item, ev.Locker, ev.Chamber, canOpenHelper, isAllowedHelper);
		ev.IsAllowed = isAllowedHelper.Value;
		ev.CanOpen = canOpenHelper.Value;
	}

	/// <inheritdoc/>
	public override void OnPlayerInteractedLocker(PlayerInteractedLockerEventArgs ev)
	{
		Item? item = ev.Player.CurrentItem;
		if (item == null)
		{
			return;
		}

		if (!CustomItems.TryGetCustomItem(item, out CustomKeycardBase? customKeyCardBase))
		{
			return;
		}

		CustomKeycardEvents.OnInteractedLocker(customKeyCardBase, ev.Player, item, ev.Locker, ev.Chamber, ev.CanOpen);
		customKeyCardBase?.OnInteractedLocker(ev.Player, item, ev.Locker, ev.Chamber, ev.CanOpen);
	}

	/// <inheritdoc/>
	public override void OnPlayerInspectingKeycard(PlayerInspectingKeycardEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.KeycardItem, out CustomKeycardBase? cur_item))
		{
			return;
		}

		TypeWrapper<bool> isAllowedHelper = new(ev.IsAllowed);
		CustomKeycardEvents.OnInspectingKeycard(cur_item, ev.Player, ev.KeycardItem, isAllowedHelper);
		cur_item.OnInspectingKeycard(ev.Player, ev.KeycardItem, isAllowedHelper);
		ev.IsAllowed = isAllowedHelper.Value;
	}

	/// <inheritdoc/>
	public override void OnPlayerInspectedKeycard(PlayerInspectedKeycardEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.KeycardItem, out CustomKeycardBase? cur_item))
		{
			return;
		}

		CustomKeycardEvents.OnInspectedKeycard(cur_item, ev.Player, ev.KeycardItem);
		cur_item.OnInspectedKeycard(ev.Player, ev.KeycardItem);
	}
}
