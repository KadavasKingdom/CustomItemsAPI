using CustomItemsAPI.Events;
using CustomItemsAPI.Items;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;

namespace CustomItemsAPI.EventHandlers;

/// <summary>
/// Represent a handle that process firearm events.
/// </summary>
internal sealed class FirearmHandler : CustomEventsHandler
{
	/// <inheritdoc/>
	public override void OnPlayerDryFiringWeapon(PlayerDryFiringWeaponEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.FirearmItem, out CustomFirearmBase? cur_item))
		{
			return;
		}

		TypeWrapper<bool> isAllowedHelper = new(ev.IsAllowed);
		CustomFirearmEvents.OnDryFiring(cur_item, ev.Player, ev.FirearmItem, isAllowedHelper);
		cur_item.OnDryFiring(ev.Player, ev.FirearmItem, isAllowedHelper);
		ev.IsAllowed = isAllowedHelper.Value;
	}

	/// <inheritdoc/>
	public override void OnPlayerDryFiredWeapon(PlayerDryFiredWeaponEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.FirearmItem, out CustomFirearmBase? cur_item))
		{
			return;
		}

		CustomFirearmEvents.OnDryFired(cur_item, ev.Player, ev.FirearmItem);
		cur_item.OnDryFired(ev.Player, ev.FirearmItem);
	}

	/// <inheritdoc/>
	public override void OnPlayerAimedWeapon(PlayerAimedWeaponEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.FirearmItem, out CustomFirearmBase? cur_item))
		{
			return;
		}

		CustomFirearmEvents.OnAim(cur_item, ev.Player, ev.FirearmItem, ev.Aiming);
		cur_item.OnAim(ev.Player, ev.FirearmItem, ev.Aiming);
	}

	/// <inheritdoc/>
	public override void OnPlayerReloadingWeapon(PlayerReloadingWeaponEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.FirearmItem, out CustomFirearmBase? cur_item))
		{
			return;
		}

		TypeWrapper<bool> isAllowedHelper = new(ev.IsAllowed);
		CustomFirearmEvents.OnReloading(cur_item, ev.Player, ev.FirearmItem, isAllowedHelper);
		cur_item.OnReloading(ev.Player, ev.FirearmItem, isAllowedHelper);
		ev.IsAllowed = isAllowedHelper.Value;
	}

	/// <inheritdoc/>
	public override void OnPlayerReloadedWeapon(PlayerReloadedWeaponEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.FirearmItem, out CustomFirearmBase? cur_item))
		{
			return;
		}

		CustomFirearmEvents.OnReloaded(cur_item, ev.Player, ev.FirearmItem);
		cur_item.OnReloaded(ev.Player, ev.FirearmItem);
	}

	/// <inheritdoc/>
	public override void OnPlayerShootingWeapon(PlayerShootingWeaponEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.FirearmItem, out CustomFirearmBase? cur_item))
		{
			return;
		}

		TypeWrapper<bool> isAllowedHelper = new(ev.IsAllowed);
		CustomFirearmEvents.OnShooting(cur_item, ev.Player, ev.FirearmItem, isAllowedHelper);
		cur_item.OnShooting(ev.Player, ev.FirearmItem, isAllowedHelper);
		ev.IsAllowed = isAllowedHelper.Value;
	}

	/// <inheritdoc/>
	public override void OnPlayerShotWeapon(PlayerShotWeaponEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.FirearmItem, out CustomFirearmBase? cur_item))
		{
			return;
		}

		CustomFirearmEvents.OnShot(cur_item, ev.Player, ev.FirearmItem);
		cur_item.OnShot(ev.Player, ev.FirearmItem);
	}

	/// <inheritdoc/>
	public override void OnPlayerUnloadingWeapon(PlayerUnloadingWeaponEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.FirearmItem, out CustomFirearmBase? cur_item))
		{
			return;
		}

		TypeWrapper<bool> isAllowedHelper = new(ev.IsAllowed);
		CustomFirearmEvents.OnUnloading(cur_item, ev.Player, ev.FirearmItem, isAllowedHelper);
		cur_item.OnUnloading(ev.Player, ev.FirearmItem, isAllowedHelper);
		ev.IsAllowed = isAllowedHelper.Value;
	}

	/// <inheritdoc/>
	public override void OnPlayerUnloadedWeapon(PlayerUnloadedWeaponEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.FirearmItem, out CustomFirearmBase? cur_item))
		{
			return;
		}

		CustomFirearmEvents.OnUnloaded(cur_item, ev.Player, ev.FirearmItem);
		cur_item.OnUnloaded(ev.Player, ev.FirearmItem);
	}

	/// <inheritdoc/>
	public override void OnPlayerTogglingWeaponFlashlight(PlayerTogglingWeaponFlashlightEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.FirearmItem, out CustomFirearmBase? cur_item))
		{
			return;
		}

		TypeWrapper<bool> isAllowedHelper = new(ev.IsAllowed);
		TypeWrapper<bool> newState = new(ev.NewState);
		CustomFirearmEvents.OnTogglingFlashlight(cur_item, ev.Player, ev.FirearmItem, newState, isAllowedHelper);
		cur_item.OnTogglingFlashlight(ev.Player, ev.FirearmItem, newState, isAllowedHelper);
		ev.IsAllowed = isAllowedHelper.Value;
		ev.NewState = newState.Value;
	}

	/// <inheritdoc/>
	public override void OnPlayerToggledWeaponFlashlight(PlayerToggledWeaponFlashlightEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.FirearmItem, out CustomFirearmBase? cur_item))
		{
			return;
		}

		CustomFirearmEvents.OnToggledFlashlight(cur_item, ev.Player, ev.FirearmItem, ev.NewState);
		cur_item.OnToggledFlashlight(ev.Player, ev.FirearmItem, ev.NewState);
	}

	/// <inheritdoc/>
	public override void OnPlayerChangingAttachments(PlayerChangingAttachmentsEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.FirearmItem, out CustomFirearmBase? cur_item))
		{
			return;
		}

		TypeWrapper<bool> isAllowedHelper = new(ev.IsAllowed);
		TypeWrapper<uint> newState = new(ev.NewAttachments);
		CustomFirearmEvents.OnChangingAttachments(cur_item, ev.Player, ev.FirearmItem, ev.OldAttachments, newState, isAllowedHelper);
		cur_item.OnChangingAttachments(ev.Player, ev.FirearmItem, ev.OldAttachments, newState, isAllowedHelper);
		ev.IsAllowed = isAllowedHelper.Value;
		ev.NewAttachments = newState.Value;
	}

	/// <inheritdoc/>
	public override void OnPlayerChangedAttachments(PlayerChangedAttachmentsEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.FirearmItem, out CustomFirearmBase? cur_item))
		{
			return;
		}

		CustomFirearmEvents.OnChangedAttachments(cur_item, ev.Player, ev.FirearmItem, ev.OldAttachments, ev.NewAttachments);
		cur_item.OnChangedAttachments(ev.Player, ev.FirearmItem, ev.OldAttachments, ev.NewAttachments);
	}
}
