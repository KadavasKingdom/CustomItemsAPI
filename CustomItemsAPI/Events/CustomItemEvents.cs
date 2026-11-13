using CustomItemsAPI.Items;
using InventorySystem.Items;
using InventorySystem.Items.Pickups;
using PlayerStatsSystem;
using Scp914;
using UnityEngine;

namespace CustomItemsAPI.Events;

#pragma warning disable CA1003 // Use generic event handler instances

/// <summary>
/// Events for calling methods for <see cref="CustomItemBase"/>.
/// </summary>
public static class CustomItemEvents
{
	/// <inheritdoc cref="CustomItemBase.OnRegistered"/>
	public static event Action<CustomItemBase>? Registered;

	/// <inheritdoc cref="CustomItemBase.OnUnRegistered"/>
	public static event Action<CustomItemBase>? Unregistered;

	/// <inheritdoc cref="CustomItemBase.OnNewCreated"/>
	public static event Action<CustomItemBase>? NewCreated;

	/// <inheritdoc cref="CustomItemBase.Parse(Pickup)"/>
	public static event Action<CustomItemBase, Pickup>? ParsePickup;

	/// <inheritdoc cref="CustomItemBase.Parse(Item)"/>
	public static event Action<CustomItemBase, Item>? ParseItem;

	/// <inheritdoc cref="CustomItemBase.OnDistribute"/>
	public static event Action<CustomItemBase>? Distribute;

	/// <inheritdoc cref="CustomItemBase.OnChanged(Player, Item?, Item?, bool)"/>
	public static event Action<CustomItemBase, Player, Item?, Item?, bool>? Changed;

	/// <inheritdoc cref="CustomItemBase.OnChanging(Player, Item?, Item?, bool, TypeWrapper{bool})"/>
	public static event Action<CustomItemBase, Player, Item?, Item?, bool, TypeWrapper<bool>>? Changing;

	/// <inheritdoc cref="CustomItemBase.OnDropped(Player, Pickup)"/>
	public static event Action<CustomItemBase, Player, Pickup>? Dropped;

	/// <inheritdoc cref="CustomItemBase.OnDropping(Player, Item, TypeWrapper{bool}, TypeWrapper{bool})"/>
	public static event Action<CustomItemBase, Player, Item, TypeWrapper<bool>, TypeWrapper<bool>>? Dropping;

	/// <inheritdoc cref="CustomItemBase.OnSearched(Player, Pickup)"/>
	public static event Action<CustomItemBase, Player, Pickup>? Searched;

	/// <inheritdoc cref="CustomItemBase.OnSearching(Player, Pickup, TypeWrapper{bool})"/>
	public static event Action<CustomItemBase, Player, Pickup, TypeWrapper<bool>>? Searching;

	/// <inheritdoc cref="CustomItemBase.OnPicked(Player, Item)"/>
	public static event Action<CustomItemBase, Player, Item?>? Picked;

	/// <inheritdoc cref="CustomItemBase.OnPicking(Player, Pickup, TypeWrapper{bool})"/>
	public static event Action<CustomItemBase, Player, Pickup, TypeWrapper<bool>>? Picking;

	/// <inheritdoc cref="CustomItemBase.OnThrew(Player, Pickup, Rigidbody)"/>
	public static event Action<CustomItemBase, Player, Pickup, Rigidbody>? Threw;

	/// <inheritdoc cref="CustomItemBase.OnThrowing(Player, Pickup, Rigidbody, TypeWrapper{bool})"/>
	public static event Action<CustomItemBase, Player, Pickup, Rigidbody, TypeWrapper<bool>>? Throwing;

	/// <inheritdoc cref="CustomItemBase.OnProcessingItem(Player, Item, TypeWrapper{Scp914KnobSetting}, TypeWrapper{bool})"/>
	public static event Action<CustomItemBase, Player, Item, TypeWrapper<Scp914KnobSetting>, TypeWrapper<bool>>? ProcessingItem;

	/// <inheritdoc cref="CustomItemBase.OnProcessingPickup(Pickup, TypeWrapper{Scp914KnobSetting}, TypeWrapper{Vector3}, TypeWrapper{bool})"/>
	public static event Action<CustomItemBase, Pickup, TypeWrapper<Scp914KnobSetting>, TypeWrapper<Vector3>, TypeWrapper<bool>>? ProcessingPickup;

	/// <inheritdoc cref="CustomItemBase.OnRemoved(Player, ItemBase?, ItemPickupBase?)"/>
	public static event Action<CustomItemBase, Player, ItemBase?, ItemPickupBase?>? Removed;

	/// <inheritdoc cref="CustomArmorBase.OnTakingDamage(Player, Player, FirearmDamageHandler, TypeWrapper{bool})"/>
	public static event Action<CustomItemBase, Player, Player, DamageHandlerBase, TypeWrapper<bool>>? TakingDamage;

	/// <inheritdoc cref="CustomItemBase.OnRegistered"/>
	public static void OnRegistered(CustomItemBase customItem)
		=> Registered?.Invoke(customItem);

	/// <inheritdoc cref="CustomItemBase.OnUnRegistered"/>
	public static void OnUnregistered(CustomItemBase customItem)
		=> Unregistered?.Invoke(customItem);

	/// <inheritdoc cref="CustomItemBase.OnNewCreated"/>
	public static void OnNewCreated(CustomItemBase customItem)
		=> NewCreated?.Invoke(customItem);

	/// <inheritdoc cref="CustomItemBase.Parse(Pickup)"/>
	public static void OnParsePickup(CustomItemBase customItem, Pickup pickup)
		=> ParsePickup?.Invoke(customItem, pickup);

	/// <inheritdoc cref="CustomItemBase.Parse(Item)"/>
	public static void OnParseItem(CustomItemBase customItem, Item item)
		=> ParseItem?.Invoke(customItem, item);

	/// <inheritdoc cref="CustomItemBase.OnDistribute"/>
	public static void OnDistribute(CustomItemBase customItem)
		=> Distribute?.Invoke(customItem);

	/// <inheritdoc cref="CustomItemBase.OnChanged(Player, Item?, Item?, bool)"/>
	public static void OnChanged(CustomItemBase customItem, Player player, Item? oldItem, Item? newItem, bool changedToThisItem)
		=> Changed?.Invoke(customItem, player, oldItem, newItem, changedToThisItem);

	/// <inheritdoc cref="CustomItemBase.OnChanging(Player, Item?, Item?, bool, TypeWrapper{bool})"/>
	public static void OnChanging(CustomItemBase customItem, Player player, Item? oldItem, Item? newItem, bool changedToThisItem, TypeWrapper<bool> isAllowed)
		=> Changing?.Invoke(customItem, player, oldItem, newItem, changedToThisItem, isAllowed);

	/// <inheritdoc cref="CustomItemBase.OnDropped(Player, Pickup)"/>
	public static void OnDropped(CustomItemBase customItem, Player player, Pickup pickup)
		=> Dropped?.Invoke(customItem, player, pickup);

	/// <inheritdoc cref="CustomItemBase.OnDropping(Player, Item, TypeWrapper{bool}, TypeWrapper{bool})"/>
	public static void OnDropping(CustomItemBase customItem, Player player, Item item, TypeWrapper<bool> isThrow, TypeWrapper<bool> isAllowed)
		=> Dropping?.Invoke(customItem, player, item, isThrow, isAllowed);

	/// <inheritdoc cref="CustomItemBase.OnSearched(Player, Pickup)"/>
	public static void OnSearched(CustomItemBase customItem, Player player, Pickup pickup)
		=> Searched?.Invoke(customItem, player, pickup);

	/// <inheritdoc cref="CustomItemBase.OnSearching(Player, Pickup, TypeWrapper{bool})"/>
	public static void OnSearching(CustomItemBase customItem, Player player, Pickup pickup, TypeWrapper<bool> isAllowed)
		=> Searching?.Invoke(customItem, player, pickup, isAllowed);

	/// <inheritdoc cref="CustomItemBase.OnPicked(Player, Item?)"/>
	public static void OnPicked(CustomItemBase customItem, Player player, Item? item)
		=> Picked?.Invoke(customItem, player, item);

	/// <inheritdoc cref="CustomItemBase.OnPicking(Player, Pickup, TypeWrapper{bool})"/>
	public static void OnPicking(CustomItemBase customItem, Player player, Pickup pickup, TypeWrapper<bool> isAllowed)
		=> Picking?.Invoke(customItem, player, pickup, isAllowed);

	/// <inheritdoc cref="CustomItemBase.OnThrew(Player, Pickup, Rigidbody)"/>
	public static void OnThrew(CustomItemBase customItem, Player player, Pickup pickup, Rigidbody rigidbody)
		=> Threw?.Invoke(customItem, player, pickup, rigidbody);

	/// <inheritdoc cref="CustomItemBase.OnThrowing(Player, Pickup, Rigidbody, TypeWrapper{bool})"/>
	public static void OnThrowing(CustomItemBase customItem, Player player, Pickup pickup, Rigidbody rigidbody, TypeWrapper<bool> isAllowed)
		=> Throwing?.Invoke(customItem, player, pickup, rigidbody, isAllowed);

	/// <inheritdoc cref="CustomItemBase.OnProcessingItem(Player, Item, TypeWrapper{Scp914KnobSetting}, TypeWrapper{bool})"/>
	public static void OnProcessingItem(CustomItemBase customItem, Player player, Item item, TypeWrapper<Scp914KnobSetting> knobSetting, TypeWrapper<bool> isAllowed)
		=> ProcessingItem?.Invoke(customItem, player, item, knobSetting, isAllowed);

	/// <inheritdoc cref="CustomItemBase.OnProcessingPickup(Pickup, TypeWrapper{Scp914KnobSetting}, TypeWrapper{Vector3}, TypeWrapper{bool})"/>
	public static void OnProcessingPickup(CustomItemBase customItem, Pickup pickup, TypeWrapper<Scp914KnobSetting> knobSetting, TypeWrapper<Vector3> newPosition, TypeWrapper<bool> isAllowed)
		=> ProcessingPickup?.Invoke(customItem, pickup, knobSetting, newPosition, isAllowed);

	/// <inheritdoc cref="CustomItemBase.OnRemoved(Player, ItemBase?, ItemPickupBase?)"/>
	public static void OnRemoved(CustomItemBase customItem, Player player, ItemBase? itemBase, ItemPickupBase? itemPickupBase)
		=> Removed?.Invoke(customItem, player, itemBase, itemPickupBase);

	/// <inheritdoc cref="CustomArmorBase.OnTakingDamage(Player, Player, FirearmDamageHandler, TypeWrapper{bool})"/>
	public static void OnTakingDamage(CustomItemBase customItem, Player player, Player attacker, DamageHandlerBase damageHandler, TypeWrapper<bool> isAllowed)
		=> TakingDamage?.Invoke(customItem, player, attacker, damageHandler, isAllowed);
}
