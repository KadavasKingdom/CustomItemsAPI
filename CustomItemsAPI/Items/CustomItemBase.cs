using InventorySystem.Items;
using InventorySystem.Items.Pickups;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Features.Wrappers;
using MEC;
using Scp914;
using UnityEngine;

namespace CustomItemsAPI.Items;

/// <summary>
/// Custom <see cref="Item"/> base
/// </summary>
public abstract class CustomItemBase
{
    internal const float UnregisterTime = 0.3f;
    /// <summary>
    /// Action to show custom hint. Can be used with any hint framework or even disabling it (null).
    /// </summary>
    public Action<Player, string>? HintShow { get; set; } =
#if HINT_FRAMEWORK_HUB
    (player, hint) =>
    {
        CL.Debug("Showing custom hint", Main.Instance.Config.Debug);
        HintFrameworkHub.HintSystem.ShowHint(player, hint, 5f);
    };
#else
    (player, hint) => player.SendHint(hint, 5f);
#endif

    /// <summary>
    /// Name of your custom item.
    /// </summary>
    public abstract string CustomItemName { get; set; }

    /// <summary>
    /// Item description for admins/players.
    /// </summary>
    public abstract string Description { get; set; }

    /// <summary>
    /// <see cref="ItemType"/> to be made.
    /// </summary>
    public abstract ItemType Type { get; }

    /// <summary>
    /// Name of your custom item to display to others.
    /// </summary>
    public virtual string DisplayName { get; set; }

    /// <summary>
    /// Gets the new weight of the <see cref="Pickup"/>
    /// </summary>
    public virtual float Weight { get; } = float.NaN;

    /// <summary>
    /// Overrides a to show a custom picked up show details
    /// </summary>
    public virtual bool OverrideShowPickedUpHint { get; set; } = Main.Instance.Config.ShowPickedUpHint;

    /// <summary>
    /// Overrides the hint for showing custom picked up details
    /// </summary>
    public virtual string OverridePickedUpHint { get; set; } = Main.Instance.Config.PickedUpHint;

    /// <summary>
    /// Overrides a to show a custom selected show details
    /// </summary>
    public virtual bool OverrideShowSelectHint { get; set; } = Main.Instance.Config.ShowSelectedHint;

    /// <summary>
    /// Overrides the hint for showing custom selected details
    /// </summary>
    public virtual string OverrideSelectedHint { get; set; } = Main.Instance.Config.SelectedHint;

    /// <summary>
    /// Shows spectators when a player has this item seelcted.
    /// </summary>
    public virtual bool OverrideShowItemHintToSpectators { get; set; } = true;

    /// <summary>
    /// Called once when this instance is registered.
    /// </summary>
    public virtual void OnRegistered()
    {
        CL.Debug($"OnRegistered {this.GetType()} {this.CustomItemName}", Main.Instance.Config.Debug);
    }

    /// <summary>
    /// Called once when this instance is unregistered.
    /// </summary>
    public virtual void OnUnRegistered()
    {
        CL.Debug($"OnUnRegistered {this.GetType()} {this.CustomItemName}", Main.Instance.Config.Debug);
    }

    /// <summary>
    /// Called when the New Item created for this Type.
    /// </summary>
    public virtual void OnNewCreated()
    {
        CL.Debug($"OnNewCreated {this.GetType()} {this.CustomItemName}", Main.Instance.Config.Debug);
    }

    /// <summary>
    /// Parsing <paramref name="item"/> to this Custom Item.
    /// </summary>
    /// <param name="item">A custom item.</param>
    public virtual void Parse(Item item)
    {
        CL.Debug($"Parse {item.Serial}", Main.Instance.Config.Debug);
    }

    /// <summary>
    /// Parsing <paramref name="pickup"/> to this custom item.
    /// </summary>
    /// <param name="pickup">A Custom Pickup</param>
    public virtual void Parse(Pickup pickup)
    {
        CL.Debug($"Parse {pickup.Serial}", Main.Instance.Config.Debug);
        if (!float.IsNaN(Weight))
            pickup.Weight = Weight;
    }

    /// <summary>
    /// Called when <see cref="LabApi.Events.Handlers.ServerEvents.MapGenerated"/> runs for every already made instance.
    /// </summary>
    public virtual void OnDistribute()
    {
        CL.Debug($"OnDistribute {this.GetType()} {this.CustomItemName}", Main.Instance.Config.Debug);
    }

    /// <summary>
    /// This <paramref name="player"/> changed to/from this Custom Item.
    /// </summary>
    /// <param name="player">The Player who called this function.</param>
    /// <param name="oldItem"></param>
    /// <param name="newItem"></param>
    /// <param name="changedToThisItem">Is player selected item or unselected it.</param>
    public virtual void OnChanged(Player player, Item? oldItem, Item? newItem, bool changedToThisItem)
    {
        CL.Debug($"OnChanged {player.PlayerId} {oldItem?.Serial} {newItem?.Serial} {changedToThisItem}", Main.Instance.Config.Debug);
        if (!changedToThisItem)
            return;

        if (OverrideShowSelectHint)
            HintShow?.Invoke(player, string.Format(OverrideSelectedHint, DisplayName, Description));

        if (OverrideShowItemHintToSpectators)
            foreach (var item in player.CurrentSpectators)
            {
                CL.Debug("HINT SENDING TO SPECTATOR ON CHANGED", Main.Instance.Config.Debug);
                HintShow?.Invoke(player, string.Format(OverrideSelectedHint, DisplayName, Description));
            }
    }

    /// <summary>
    /// This <paramref name="player"/> changing to/from this Custom Item.
    /// </summary>
    /// <param name="player">The Player who called this function.</param>
    /// <param name="oldItem"></param>
    /// <param name="newItem"></param>
    /// <param name="changedToThisItem">Is player selected item or unselected it.</param>
    /// <param name="isAllowed">Can allow this action.</param>
    public virtual void OnChanging(Player player, Item? oldItem, Item? newItem, bool changedToThisItem, TypeWrapper<bool> isAllowed)
    {
        CL.Debug($"OnChanging {player.PlayerId} {oldItem?.Serial} {newItem?.Serial} {changedToThisItem}", Main.Instance.Config.Debug);
    }

    /// <summary>
    /// This <paramref name="player"/> who currently dropped to current Custom Item.
    /// </summary>
    /// <param name="player">The Player who called this function.</param>
    /// <param name="pickup"></param>
    public virtual void OnDropped(Player player, Pickup pickup)
    {
        CL.Debug($"OnDropped {player.PlayerId} {pickup.Serial}", Main.Instance.Config.Debug);
    }

    /// <summary>
    /// This <paramref name="player"/> who currently dropping to current Custom Item.
    /// </summary>
    /// <param name="player">The Player who called this function.</param>
    /// <param name="item"></param>
    /// <param name="isThrow">Can allow throw item from inventory.</param>
    /// <param name="isAllowed">Can allow this action.</param>
    public virtual void OnDropping(Player player, Item item, TypeWrapper<bool> isThrow, TypeWrapper<bool> isAllowed)
    {
        CL.Debug($"OnDropping {player.PlayerId} {item.Serial}", Main.Instance.Config.Debug);
    }

    /// <summary>
    /// This <paramref name="player"/> who picked up the current Custom Item.
    /// </summary>
    /// <param name="player">The Player who called this function.</param>
    /// <param name="pickup"></param>
    public virtual void OnSearched(Player player, Pickup pickup)
    {
        CL.Debug($"OnSearched {player.PlayerId} {pickup.Serial}", Main.Instance.Config.Debug);
    }

    /// <summary>
    /// This <paramref name="player"/> who searching the current Custom Item.
    /// </summary>
    /// <param name="player">Player who called this function.</param>
    /// <param name="pickup">The pickup searching.</param>
    /// <param name="isAllowed">Can allow this action.</param>
    public virtual void OnSearching(Player player, Pickup pickup, TypeWrapper<bool> isAllowed)
    {
        CL.Debug($"OnSearching {player.PlayerId} {pickup.Serial}", Main.Instance.Config.Debug);
    }

    /// <summary>
    /// This <paramref name="player"/> who picked up the current Custom Item.
    /// </summary>
    /// <param name="player">The Player who called this function.</param>
    /// <param name="item">The item has been picked up.</param>
    public virtual void OnPicked(Player player, Item item)
    {
        CL.Debug($"OnPicked {player.PlayerId} {item.Serial}", Main.Instance.Config.Debug);
        if (OverrideShowPickedUpHint)
            HintShow?.Invoke(player, string.Format(OverridePickedUpHint, DisplayName, Description));
    }

    /// <summary>
    /// This <paramref name="player"/> who picking up the current Custom Item.
    /// </summary>
    /// <param name="player">Player who called this function.</param>
    /// <param name="pickup">The pickup picking up</param>
    /// <param name="isAllowed">Can allow this action.</param>
    public virtual void OnPicking(Player player, Pickup pickup, TypeWrapper<bool> isAllowed)
    {
        CL.Debug($"OnPicking {player.PlayerId} {pickup.Serial}", Main.Instance.Config.Debug);
    }

    /// <summary>
    /// This <paramref name="player"/> who threw up the current Custom Item.
    /// </summary>
    /// <param name="player">The Player who called this function.</param>
    /// <param name="pickup"></param>
    /// <param name="rigidbody">The rigidbody for the <see cref="Pickup"/></param>
    public virtual void OnThrew(Player player, Pickup pickup, Rigidbody rigidbody)
    {
        CL.Debug($"OnThrew {player.PlayerId} {rigidbody}", Main.Instance.Config.Debug);
    }

    /// <summary>
    /// This <paramref name="player"/> who throwing the current Custom Item.
    /// </summary>
    /// <param name="player">Player who called this function.</param>
    /// <param name="pickup">The pickup to throw.</param>
    /// <param name="rigidbody">The rigidbody for the <paramref name="pickup"/></param>
    /// <param name="isAllowed">Can allow this action.</param>
    public virtual void OnThrowing(Player player, Pickup pickup, Rigidbody rigidbody, TypeWrapper<bool> isAllowed)
    {
        CL.Debug($"OnThrowing {player.PlayerId} {pickup.Serial} {rigidbody}", Main.Instance.Config.Debug);
        if (!float.IsNaN(Weight))
            pickup.Weight = Weight;
    }

    /// <summary>
    /// Called when <paramref name="player"/> is processing <paramref name="item"/> on <paramref name="knobSetting"/> setting at 914.
    /// </summary>
    /// <param name="player">The Player who called this function.</param>
    /// <param name="item">The currently processing item.</param>
    /// <param name="knobSetting">The know setting value.</param>
    /// <param name="isAllowed">Can allow this action.</param>
    public virtual void OnProcessingItem(Player player, Item item, TypeWrapper<Scp914KnobSetting> knobSetting, TypeWrapper<bool> isAllowed)
    {
        CL.Debug($"OnProcessingItem {player.PlayerId} {item.Serial} {knobSetting}", Main.Instance.Config.Debug);
        isAllowed.Value = false;
    }

    /// <summary>
    /// Called when a <paramref name="pickup"/> is processing on <paramref name="knobSetting"/> setting at 914.
    /// </summary>
    /// <param name="pickup">The currently processing pickup.</param>
    /// <param name="knobSetting">The know setting value.</param>
    /// <param name="newPosition">New position for the pickup.</param>
    /// <param name="isAllowed">Can allow this action.</param>
    public virtual void OnProcessingPickup(Pickup pickup, TypeWrapper<Scp914KnobSetting> knobSetting, TypeWrapper<Vector3> newPosition, TypeWrapper<bool> isAllowed)
    {
        CL.Debug($"OnProcessingPickup {pickup.Serial} {knobSetting.Value}", Main.Instance.Config.Debug);
        isAllowed.Value = false;
    }

    /// <summary>
    /// This <paramref name="player"/> who removed the current Custom Item.
    /// </summary>
    /// <param name="player">The Player who called this function.</param>
    /// <param name="itemBase">ItemBase</param>
    /// <param name="itemPickupBase">itemPickupBase</param>
    public virtual void OnRemoved(Player player, ItemBase? itemBase, ItemPickupBase? itemPickupBase)
    {
        CL.Debug($"OnRemoved {player.PlayerId} {itemBase is null} {itemPickupBase is null}", Main.Instance.Config.Debug);
        if (itemBase is not null && itemPickupBase is null)
        {
            ushort serial = itemBase.ItemSerial;
            Timing.CallDelayed(UnregisterTime, () =>
            {
                CustomItems.SerialToCustomItem.Remove(serial);
            });

        }
    }

    /// <summary>
    /// Someone changes to spectate someone holding the current Custom Item.
    /// </summary>
    public virtual void OnPlayerChangedSpectator(PlayerChangedSpectatorEventArgs ev)
    {
        CL.Debug($"OnPlayerChangedSpectator {ev.Player.PlayerId} changed to {ev.NewTarget?.PlayerId ?? -1}", Main.Instance.Config.Debug);
        if (OverrideShowItemHintToSpectators)
            HintShow?.Invoke(ev.Player, string.Format(OverrideSelectedHint, DisplayName, Description));
    }
}
