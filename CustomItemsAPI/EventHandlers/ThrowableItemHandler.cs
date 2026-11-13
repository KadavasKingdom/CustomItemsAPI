using CustomItemsAPI.Events;
using CustomItemsAPI.Items;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;

namespace CustomItemsAPI.EventHandlers;

/// <summary>
/// Represent a handle that process throwable item events.
/// </summary>
internal sealed class ThrowableItemHandler : CustomEventsHandler
{
	/// <inheritdoc/>
	public override void OnPlayerThrowingProjectile(PlayerThrowingProjectileEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.ThrowableItem, out CustomThrowableBase? cur_item))
		{
			return;
		}

		TypeWrapper<bool> isAllowed = new(ev.IsAllowed);
		TypeWrapper<bool> fullForce = new(ev.FullForce);
		TypeWrapper<InventorySystem.Items.ThrowableProjectiles.ThrowableItem.ProjectileSettings> settings = new(ev.ProjectileSettings);
		CustomThrowableEvents.OnThrowingProjectile(cur_item, ev.Player, ev.ThrowableItem, settings, fullForce, isAllowed);
		cur_item.OnThrowingProjectile(ev.Player, ev.ThrowableItem, settings, fullForce, isAllowed);
		ev.IsAllowed = isAllowed.Value;
		ev.FullForce = fullForce.Value;
		ev.ProjectileSettings = settings.Value;
	}

	/// <inheritdoc/>
	public override void OnPlayerThrewProjectile(PlayerThrewProjectileEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.ThrowableItem, out CustomThrowableBase? cur_item))
		{
			return;
		}

		CustomThrowableEvents.OnThrewProjectile(cur_item, ev.Player, ev.ThrowableItem, ev.Projectile, ev.ProjectileSettings, ev.FullForce);
		cur_item.OnThrewProjectile(ev.Player, ev.ThrowableItem, ev.Projectile, ev.ProjectileSettings, ev.FullForce);
	}
}
