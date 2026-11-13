using CustomItemsAPI.Items;
using PlayerStatsSystem;

namespace CustomItemsAPI.Events;

#pragma warning disable CA1003 // Use generic event handler instances

/// <summary>
/// Events for calling methods for <see cref="CustomFirearmBase"/>.
/// </summary>
public static class CustomFirearmEvents
{
	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.AimedWeapon"/>
	public static event Action<CustomFirearmBase, Player, FirearmItem, bool>? Aim;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.DryFiredWeapon"/>
	public static event Action<CustomFirearmBase, Player, FirearmItem>? DryFired;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.DryFiringWeapon"/>
	public static event Action<CustomFirearmBase, Player, FirearmItem, TypeWrapper<bool>>? DryFiring;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.ReloadedWeapon"/>
	public static event Action<CustomFirearmBase, Player, FirearmItem>? Reloaded;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.ReloadingWeapon"/>
	public static event Action<CustomFirearmBase, Player, FirearmItem, TypeWrapper<bool>>? Reloading;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.ShotWeapon"/>
	public static event Action<CustomFirearmBase, Player, FirearmItem>? Shot;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.ShootingWeapon"/>
	public static event Action<CustomFirearmBase, Player, FirearmItem, TypeWrapper<bool>>? Shooting;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.ToggledWeaponFlashlight"/>
	public static event Action<CustomFirearmBase, Player, FirearmItem, bool>? ToggledFlashlight;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.TogglingWeaponFlashlight"/>
	public static event Action<CustomFirearmBase, Player, FirearmItem, TypeWrapper<bool>, TypeWrapper<bool>>? TogglingFlashlight;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.UnloadedWeapon"/>
	public static event Action<CustomFirearmBase, Player, FirearmItem>? Unloaded;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.UnloadingWeapon"/>
	public static event Action<CustomFirearmBase, Player, FirearmItem, TypeWrapper<bool>>? Unloading;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.Hurt"/>
	public static event Action<CustomFirearmBase, Player, Player, FirearmDamageHandler>? Hurt;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.Hurting"/>
	public static event Action<CustomFirearmBase, Player, Player, FirearmDamageHandler, TypeWrapper<bool>>? Hurting;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.ChangedAttachments"/>
	public static event Action<CustomFirearmBase, Player, FirearmItem, uint, uint>? ChangedAttachments;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.ChangingAttachments"/>
	public static event Action<CustomFirearmBase, Player, FirearmItem, uint, TypeWrapper<uint>, TypeWrapper<bool>>? ChangingAttachments;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.AimedWeapon"/>
	public static void OnAim(CustomFirearmBase customItem, Player player, FirearmItem weapon, bool aiming)
		=> Aim?.Invoke(customItem, player, weapon, aiming);

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.DryFiredWeapon"/>
	public static void OnDryFired(CustomFirearmBase customItem, Player player, FirearmItem weapon)
		=> DryFired?.Invoke(customItem, player, weapon);

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.DryFiringWeapon"/>
	public static void OnDryFiring(CustomFirearmBase customItem, Player player, FirearmItem weapon, TypeWrapper<bool> isAllowedHelper)
		=> DryFiring?.Invoke(customItem, player, weapon, isAllowedHelper);

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.ReloadedWeapon"/>
	public static void OnReloaded(CustomFirearmBase customItem, Player player, FirearmItem weapon)
		=> Reloaded?.Invoke(customItem, player, weapon);

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.ReloadingWeapon"/>
	public static void OnReloading(CustomFirearmBase customItem, Player player, FirearmItem weapon, TypeWrapper<bool> isAllowedHelper)
		=> Reloading?.Invoke(customItem, player, weapon, isAllowedHelper);

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.ShotWeapon"/>
	public static void OnShot(CustomFirearmBase customItem, Player player, FirearmItem weapon)
		=> Shot?.Invoke(customItem, player, weapon);

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.ShootingWeapon"/>
	public static void OnShooting(CustomFirearmBase customItem, Player player, FirearmItem weapon, TypeWrapper<bool> isAllowedHelper)
		=> Shooting?.Invoke(customItem, player, weapon, isAllowedHelper);

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.ToggledWeaponFlashlight"/>
	public static void OnToggledFlashlight(CustomFirearmBase customItem, Player player, FirearmItem weapon, bool newState)
		=> ToggledFlashlight?.Invoke(customItem, player, weapon, newState);

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.TogglingWeaponFlashlight"/>
	public static void OnTogglingFlashlight(CustomFirearmBase customItem, Player player, FirearmItem weapon, TypeWrapper<bool> newState, TypeWrapper<bool> isAllowedHelper)
		=> TogglingFlashlight?.Invoke(customItem, player, weapon, newState, isAllowedHelper);

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.UnloadedWeapon"/>
	public static void OnUnloaded(CustomFirearmBase customItem, Player player, FirearmItem weapon)
		=> Unloaded?.Invoke(customItem, player, weapon);

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.UnloadingWeapon"/>
	public static void OnUnloading(CustomFirearmBase customItem, Player player, FirearmItem weapon, TypeWrapper<bool> isAllowedHelper)
		=> Unloading?.Invoke(customItem, player, weapon, isAllowedHelper);

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.Hurt"/>
	public static void OnHurt(CustomFirearmBase customItem, Player player, Player attacker, FirearmDamageHandler firearmDamage)
		=> Hurt?.Invoke(customItem, player, attacker, firearmDamage);

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.Hurting"/>
	public static void OnHurting(CustomFirearmBase customItem, Player player, Player attacker, FirearmDamageHandler firearmDamage, TypeWrapper<bool> isAllowedHelper)
		=> Hurting?.Invoke(customItem, player, attacker, firearmDamage, isAllowedHelper);

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.ChangedAttachments"/>
	public static void OnChangedAttachments(CustomFirearmBase customItem, Player player, FirearmItem weapon, uint oldAttachments, uint newAttachments)
		=> ChangedAttachments?.Invoke(customItem, player, weapon, oldAttachments, newAttachments);

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.ChangingAttachments"/>
	public static void OnChangingAttachments(CustomFirearmBase customItem, Player player, FirearmItem weapon, uint oldAttachments, TypeWrapper<uint> newAttachments, TypeWrapper<bool> isAllowedHelper)
		=> ChangingAttachments?.Invoke(customItem, player, weapon, oldAttachments, newAttachments, isAllowedHelper);
}
