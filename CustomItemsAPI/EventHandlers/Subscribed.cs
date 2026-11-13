using CustomItemsAPI.Events;
using CustomItemsAPI.Items;
using InventorySystem;
using InventorySystem.Items;
using InventorySystem.Items.MicroHID.Modules;
using InventorySystem.Items.Pickups;
using InventorySystem.Items.ThrowableProjectiles;

namespace CustomItemsAPI.EventHandlers;

/// <summary>
/// Provides a custom processing for non existent labapi events.
/// </summary>
internal static class Subscribed
{
	/// <inheritdoc cref="InventoryExtensions.OnItemRemoved"/>
	internal static void OnItemRemoved(ReferenceHub hub, ItemBase itemBase, ItemPickupBase itemPickupBase)
	{
		var customItem = CustomItems.GetCustomItem<CustomItemBase>(Item.Get(itemBase));
		var customItem2 = CustomItems.GetCustomItem<CustomItemBase>(Pickup.Get(itemPickupBase));
		Player player = Player.Get(hub);
		if (customItem != null)
		{
			CustomItemEvents.OnRemoved(customItem, player, itemBase, itemPickupBase);
			customItem.OnRemoved(player, itemBase, itemPickupBase);
		}

		if (customItem != null && customItem != customItem2 && customItem2 != null)
		{
			CustomItemEvents.OnRemoved(customItem2, player, itemBase, itemPickupBase);
			customItem2.OnRemoved(player, itemBase, itemPickupBase);
		}
	}

	/// <inheritdoc cref="ThrownProjectile.OnProjectileSpawned"/>
	internal static void ProjectileSpawned(ThrownProjectile projectile)
	{
		Projectile labProjectile = Projectile.Get(projectile);
		if (!CustomItems.TryGetCustomItem(labProjectile, out CustomThrowableBase? cur_item))
		{
			return;
		}

		CustomThrowableEvents.OnProjectileSpawned(cur_item, labProjectile);
		cur_item.OnProjectileSpawned(labProjectile);
	}

	/// <inheritdoc cref="CycleController.OnPhaseChanged"/>
	internal static void PhaseChanged(ushort serial, MicroHidPhase phase)
	{
		if (!CustomItems.TryGetCustomItem(serial, out CustomMicroHIDBase? cur_item))
		{
			return;
		}

		if (Item.Get(serial) is not MicroHIDItem microHIDItem)
		{
			return;
		}

		CustomMicroHIDEvents.OnPhaseChanged(cur_item, microHIDItem, phase);
		cur_item.OnPhaseChanged(microHIDItem, phase);
	}

	/// <inheritdoc cref="BrokenSyncModule.OnBroken"/>
	internal static void BrokenSyncModule_OnBroken(ushort serial)
	{
		if (!CustomItems.TryGetCustomItem(serial, out CustomMicroHIDBase? cur_item))
		{
			return;
		}

		if (Item.Get(serial) is not MicroHIDItem microHIDItem)
		{
			return;
		}

		CustomMicroHIDEvents.OnBroken(cur_item, microHIDItem);
		cur_item.OnBroken(microHIDItem);
	}
}
