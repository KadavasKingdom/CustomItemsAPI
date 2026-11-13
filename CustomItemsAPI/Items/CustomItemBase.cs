using InventorySystem.Items;
using InventorySystem.Items.Pickups;
using MEC;
using Scp914;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace CustomItemsAPI.Items;

/// <summary>
/// Custom <see cref="Item"/> base.
/// </summary>
public abstract class CustomItemBase
{
	/// <summary>
	/// The time to unregister the item.
	/// </summary>
	public const float UnregisterTime = 0.3f;

	/// <summary>
	/// Action to show custom hint. Can be used with any hint framework or even disabling it (null).
	/// </summary>
#pragma warning disable CA1051 // Do not declare visible instance fields
#pragma warning disable SA1401 // Fields should be private
	public Action<Player, string>? HintShow = (player, hint) => player.SendHint(hint);
#pragma warning restore SA1401 // Fields should be private
#pragma warning restore CA1051 // Do not declare visible instance fields

	/// <summary>
	/// Gets or sets name of your custom item.
	/// </summary>
	public abstract string CustomItemName { get; set; }

	/// <summary>
	/// Gets or sets item description for admins/players.
	/// </summary>
	public abstract string Description { get; set; }

	/// <summary>
	/// <see cref="ItemType"/> Gets to be made.
	/// </summary>
	public abstract ItemType Type { get; }

	/// <summary>
	/// Gets or sets name of your custom item to display to others.
	/// </summary>
	public virtual required string DisplayName { get; set; }

	/// <summary>
	/// Gets the new weight of the <see cref="Pickup"/>.
	/// </summary>
	public virtual float Weight { get; } = float.NaN;

	/// <summary>
	/// Gets or sets a value indicating whether overrides a to show a custom picked up show details.
	/// </summary>
	public virtual bool OverrideShowPickedUpHint { get; set; } = Main.Instance.Config.ShowPickedUpHint;

	/// <summary>
	/// Gets or sets overrides the hint for showing custom picked up details.
	/// </summary>
	public virtual string OverridePickedUpHint { get; set; } = Main.Instance.Config.PickedUpHint;

	/// <summary>
	/// Gets or sets a value indicating whether overrides a to show a custom selected show details.
	/// </summary>
	public virtual bool OverrideShowSelectHint { get; set; } = Main.Instance.Config.ShowSelectedHint;

	/// <summary>
	/// Gets or sets overrides the hint for showing custom selected details.
	/// </summary>
	public virtual string OverrideSelectedHint { get; set; } = Main.Instance.Config.SelectedHint;

	/// <summary>
	/// Called once when this instance is registered.
	/// </summary>
	public virtual void OnRegistered()
	{
	}

	/// <summary>
	/// Called once when this instance is unregistered.
	/// </summary>
	public virtual void OnUnRegistered()
	{
	}

	/// <summary>
	/// Called when the New Item created for this Type.
	/// </summary>
	public virtual void OnNewCreated()
	{
	}

	/// <summary>
	/// Parsing <paramref name="item"/> to this Custom Item.
	/// </summary>
	/// <param name="item">A custom item.</param>
	public virtual void Parse(Item item)
	{
	}

	/// <summary>
	/// Parsing <paramref name="pickup"/> to this custom item.
	/// </summary>
	/// <param name="pickup">A Custom Pickup.</param>
	public virtual void Parse(Pickup pickup)
	{
		if (pickup is null)
		{
			return;
		}

		if (!float.IsNaN(Weight))
		{
			pickup.Weight = Weight;
		}
	}

	/// <summary>
	/// Called when <see cref="LabApi.Events.Handlers.ServerEvents.MapGenerated"/> runs for every already made instance.
	/// </summary>
	public virtual void OnDistribute()
	{
	}

	/// <summary>
	/// This <paramref name="player"/> changed to/from this Custom Item.
	/// </summary>
	/// <param name="player">The Player who called this function.</param>
	/// <param name="oldItem">The old item.</param>
	/// <param name="newItem">The new item.</param>
	/// <param name="changedToThisItem">Is player selected item or unselected it.</param>
	public virtual void OnChanged(Player player, Item? oldItem, Item? newItem, bool changedToThisItem)
	{
		if (player is null)
		{
			return;
		}

		if (changedToThisItem && OverrideShowSelectHint)
		{
#pragma warning disable CA1305 // Specify IFormatProvider
			HintShow?.Invoke(player, string.Format(OverrideSelectedHint, DisplayName, Description));
#pragma warning restore CA1305 // Specify IFormatProvider
		}
	}

	/// <summary>
	/// This <paramref name="player"/> changing to/from this Custom Item.
	/// </summary>
	/// <param name="player">The Player who called this function.</param>
	/// <param name="oldItem">The old item.</param>
	/// <param name="newItem">The new item.</param>
	/// <param name="changedToThisItem">Is player selected item or unselected it.</param>
	/// <param name="isAllowed">Can allow this action.</param>
	public virtual void OnChanging(Player player, Item? oldItem, Item? newItem, bool changedToThisItem, [NotNull] TypeWrapper<bool> isAllowed)
	{
	}

	/// <summary>
	/// This <paramref name="player"/> who currently dropped to current Custom Item.
	/// </summary>
	/// <param name="player">The Player who called this function.</param>
	/// <param name="pickup">The pickup dropped.</param>
	public virtual void OnDropped(Player player, Pickup pickup)
	{
	}

	/// <summary>
	/// This <paramref name="player"/> who currently dropping to current Custom Item.
	/// </summary>
	/// <param name="player">The Player who called this function.</param>
	/// <param name="item">The item to drop.</param>
	/// <param name="isThrow">Can allow throw item from inventory.</param>
	/// <param name="isAllowed">Can allow this action.</param>
	public virtual void OnDropping(Player player, Item item, [NotNull] TypeWrapper<bool> isThrow, [NotNull] TypeWrapper<bool> isAllowed)
	{
	}

	/// <summary>
	/// This <paramref name="player"/> who searched the current Custom Item.
	/// </summary>
	/// <param name="player">The Player who called this function.</param>
	/// <param name="pickup">The pickup is currently searching.</param>
	public virtual void OnSearched(Player player, Pickup pickup)
	{
	}

	/// <summary>
	/// This <paramref name="player"/> who searching the current Custom Item.
	/// </summary>
	/// <param name="player">Player who called this function.</param>
	/// <param name="pickup">The pickup searching.</param>
	/// <param name="isAllowed">Can allow this action.</param>
	public virtual void OnSearching(Player player, Pickup pickup, [NotNull] TypeWrapper<bool> isAllowed)
	{
	}

	/// <summary>
	/// This <paramref name="player"/> who picked up the current Custom Item.
	/// </summary>
	/// <param name="player">The Player who called this function.</param>
	/// <param name="item">The item has been picked up.</param>
	public virtual void OnPicked(Player player, Item? item)
	{
		if (player is null)
		{
			return;
		}

		if (OverrideShowPickedUpHint)
		{
#pragma warning disable CA1305 // Specify IFormatProvider
			HintShow?.Invoke(player, string.Format(OverridePickedUpHint, DisplayName, Description));
#pragma warning restore CA1305 // Specify IFormatProvider
		}
	}

	/// <summary>
	/// This <paramref name="player"/> who picking up the current Custom Item.
	/// </summary>
	/// <param name="player">Player who called this function.</param>
	/// <param name="pickup">The pickup picking up.</param>
	/// <param name="isAllowed">Can allow this action.</param>
	public virtual void OnPicking(Player player, Pickup pickup, [NotNull] TypeWrapper<bool> isAllowed)
	{
	}

	/// <summary>
	/// This <paramref name="player"/> who threw the current Custom Item.
	/// </summary>
	/// <param name="player">The Player who called this function.</param>
	/// <param name="pickup">The pickup.</param>
	/// <param name="rigidbody">The rigidbody for the <see cref="Pickup"/>.</param>
	public virtual void OnThrew(Player player, Pickup pickup, Rigidbody rigidbody)
	{
	}

	/// <summary>
	/// This <paramref name="player"/> who throwing the current Custom Item.
	/// </summary>
	/// <param name="player">Player who called this function.</param>
	/// <param name="pickup">The pickup to throw.</param>
	/// <param name="rigidbody">The rigidbody for the <paramref name="pickup"/>.</param>
	/// <param name="isAllowed">Can allow this action.</param>
	public virtual void OnThrowing(Player player, Pickup pickup, Rigidbody rigidbody, [NotNull] TypeWrapper<bool> isAllowed)
	{
		if (pickup is null)
		{
			return;
		}

		if (!float.IsNaN(Weight))
		{
			pickup.Weight = Weight;
		}
	}

	/// <summary>
	/// Called when <paramref name="player"/> is processing <paramref name="item"/> on <paramref name="knobSetting"/> setting at 914.
	/// </summary>
	/// <param name="player">The Player who called this function.</param>
	/// <param name="item">The currently processing item.</param>
	/// <param name="knobSetting">The know setting value.</param>
	/// <param name="isAllowed">Can allow this action.</param>
	public virtual void OnProcessingItem(Player player, Item item, [NotNull] TypeWrapper<Scp914KnobSetting> knobSetting, [NotNull] TypeWrapper<bool> isAllowed)
	{
		isAllowed.Value = false;
	}

	/// <summary>
	/// Called when a <paramref name="pickup"/> is processing on <paramref name="knobSetting"/> setting at 914.
	/// </summary>
	/// <param name="pickup">The currently processing pickup.</param>
	/// <param name="knobSetting">The know setting value.</param>
	/// <param name="newPosition">New position for the pickup.</param>
	/// <param name="isAllowed">Can allow this action.</param>
	public virtual void OnProcessingPickup(Pickup pickup, [NotNull] TypeWrapper<Scp914KnobSetting> knobSetting, [NotNull] TypeWrapper<Vector3> newPosition, [NotNull] TypeWrapper<bool> isAllowed)
	{
		isAllowed.Value = false;
	}

	/// <summary>
	/// This <paramref name="player"/> who removed the current Custom Item.
	/// </summary>
	/// <param name="player">The Player who called this function.</param>
	/// <param name="itemBase">ItemBase.</param>
	/// <param name="itemPickupBase">itemPickupBase.</param>
	public virtual void OnRemoved(Player player, ItemBase? itemBase, ItemPickupBase? itemPickupBase)
	{
		CL.Debug($"OnRemoved {player?.PlayerId} {itemBase is null} {itemPickupBase is null}", Main.Instance.Config.Debug);
		if (itemBase is not null && itemPickupBase is null)
		{
			ushort serial = itemBase.ItemSerial;
			Timing.CallDelayed(UnregisterTime, () =>
			{
				CustomItems.SerialToCustomItem.Remove(serial);
			});
		}
	}
}
