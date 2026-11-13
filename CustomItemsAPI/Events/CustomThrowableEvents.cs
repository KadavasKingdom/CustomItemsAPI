using CustomItemsAPI.Items;
using PSettings = InventorySystem.Items.ThrowableProjectiles.ThrowableItem.ProjectileSettings;

namespace CustomItemsAPI.Events;

#pragma warning disable CA1003 // Use generic event handler instances

/// <summary>
/// Events for calling methods for <see cref="CustomThrowableBase"/>.
/// </summary>
public static class CustomThrowableEvents
{
	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.ThrewProjectile"/>
	public static event Action<CustomThrowableBase, Player, ThrowableItem, Projectile, PSettings, bool>? ThrewProjectile;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.ThrowingProjectile"/>
	public static event Action<CustomThrowableBase, Player, ThrowableItem, TypeWrapper<PSettings>, TypeWrapper<bool>, TypeWrapper<bool>>? ThrowingProjectile;

	/// <inheritdoc cref="CustomThrowableBase.OnProjectileSpawned(Projectile)"/>
	public static event Action<CustomThrowableBase, Projectile>? ProjectileSpawned;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.ThrewProjectile"/>
	public static void OnThrewProjectile(CustomThrowableBase custom, Player player, ThrowableItem throwableItem, Projectile projectile, PSettings settings, bool fullForce)
		=> ThrewProjectile?.Invoke(custom, player, throwableItem, projectile, settings, fullForce);

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.ThrowingProjectile"/>
	public static void OnThrowingProjectile(CustomThrowableBase custom, Player player, ThrowableItem throwableItem, TypeWrapper<PSettings> settings, TypeWrapper<bool> isFullForce, TypeWrapper<bool> isAllowed)
		=> ThrowingProjectile?.Invoke(custom, player, throwableItem, settings, isFullForce, isAllowed);

	/// <inheritdoc cref="CustomThrowableBase.OnProjectileSpawned(Projectile)"/>
	public static void OnProjectileSpawned(CustomThrowableBase curItem, Projectile labProjectile)
		=> ProjectileSpawned?.Invoke(curItem, labProjectile);
}
