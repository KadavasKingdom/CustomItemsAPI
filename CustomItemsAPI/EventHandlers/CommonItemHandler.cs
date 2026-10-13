using CustomItemsAPI.Events;
using CustomItemsAPI.Items;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Wrappers;
using PlayerRoles;

namespace CustomItemsAPI.EventHandlers;

internal sealed class CommonItemHandler : CustomEventsHandler
{
    /// <summary>
    /// Action to show custom hint. Can be used with any hint framework or even disabling it (null).
    /// </summary>
    public Action<Player, string>? HintShow = (player, hint) => player.SendHint(hint);

    #region Change
    public override void OnPlayerChangingItem(PlayerChangingItemEventArgs ev)
    {
        TypeWrapper<bool> isAllowedHelper = new(ev.IsAllowed);
        if (CustomItems.TryGetCustomItem(ev.OldItem, out CustomItemBase prev_Item))
        {
            CustomItemEvents.OnChanging(prev_Item, ev.Player, ev.OldItem, ev.NewItem, false, isAllowedHelper);
            prev_Item.OnChanging(ev.Player, ev.OldItem, ev.NewItem, false, isAllowedHelper);
        }

        if (CustomItems.TryGetCustomItem(ev.NewItem, out CustomItemBase cur_item))
        {
            CustomItemEvents.OnChanging(prev_Item, ev.Player, ev.OldItem, ev.NewItem, true, isAllowedHelper);
            cur_item.OnChanging(ev.Player, ev.OldItem, ev.NewItem, true, isAllowedHelper);
        }

        ev.IsAllowed = isAllowedHelper.Value;
    }
    public override void OnPlayerChangedItem(PlayerChangedItemEventArgs ev)
    {
        if (CustomItems.TryGetCustomItem(ev.OldItem, out CustomItemBase prev_Item))
        {
            CustomItemEvents.OnChanged(prev_Item, ev.Player, ev.OldItem, ev.NewItem, false);
            prev_Item.OnChanged(ev.Player, ev.OldItem, ev.NewItem, false);
        }

        if (CustomItems.TryGetCustomItem(ev.NewItem, out CustomItemBase cur_item))
        {
            CustomItemEvents.OnChanged(prev_Item, ev.Player, ev.OldItem, ev.NewItem, true);
            cur_item.OnChanged(ev.Player, ev.OldItem, ev.NewItem, true);
        }
        else
            foreach (var item in ev.Player.CurrentSpectators)
                HintShow?.Invoke(ev.Player, string.Empty);
    }
    #endregion
    #region Drop
    public override void OnPlayerDroppingItem(PlayerDroppingItemEventArgs ev)
    {
        if (!CustomItems.TryGetCustomItem(ev.Item, out CustomItemBase cur_item))
            return;
        TypeWrapper<bool> isThrowHelper = new(ev.Throw);
        TypeWrapper<bool> isAllowedHelper = new(ev.IsAllowed);
        CustomItemEvents.OnDropping(cur_item, ev.Player, ev.Item, isThrowHelper, isAllowedHelper);
        cur_item.OnDropping(ev.Player, ev.Item, isThrowHelper, isAllowedHelper);
        ev.Throw = isThrowHelper.Value;
        ev.IsAllowed = isAllowedHelper.Value;
    }
    public override void OnPlayerDroppedItem(PlayerDroppedItemEventArgs ev)
    {
        if (!CustomItems.TryGetCustomItem(ev.Pickup, out CustomItemBase cur_item))
            return;
        CustomItemEvents.OnDropped(cur_item, ev.Player, ev.Pickup);
        cur_item.OnDropped(ev.Player, ev.Pickup);
    }
    #endregion
    #region Pick
    public override void OnPlayerPickingUpItem(PlayerPickingUpItemEventArgs ev)
    {
        if (!CustomItems.TryGetCustomItem(ev.Pickup, out CustomItemBase cur_item))
            return;
        TypeWrapper<bool> isAllowedHelper = new(ev.IsAllowed);
        CustomItemEvents.OnPicking(cur_item, ev.Player, ev.Pickup, isAllowedHelper);
        cur_item.OnPicking(ev.Player, ev.Pickup, isAllowedHelper);
        ev.IsAllowed = isAllowedHelper.Value;
    }
    public override void OnPlayerPickedUpItem(PlayerPickedUpItemEventArgs ev)
    {
        if (!CustomItems.TryGetCustomItem(ev.Item, out CustomItemBase cur_item))
            return;
        CustomItemEvents.OnPicked(cur_item, ev.Player, ev.Item);
        cur_item.OnPicked(ev.Player, ev.Item);
    }
    #endregion
    #region Throw
    public override void OnPlayerThrowingItem(PlayerThrowingItemEventArgs ev)
    {
        if (!CustomItems.TryGetCustomItem(ev.Pickup, out CustomItemBase cur_item))
            return;
        TypeWrapper<bool> isAllowedHelper = new(ev.IsAllowed);
        CustomItemEvents.OnThrowing(cur_item, ev.Player, ev.Pickup, ev.Rigidbody, isAllowedHelper);
        cur_item.OnThrowing(ev.Player, ev.Pickup, ev.Rigidbody, isAllowedHelper);
        ev.IsAllowed = isAllowedHelper.Value;
    }

    public override void OnPlayerThrewItem(PlayerThrewItemEventArgs ev)
    {
        if (!CustomItems.TryGetCustomItem(ev.Pickup, out CustomItemBase cur_item))
            return;
        CustomItemEvents.OnThrew(cur_item, ev.Player, ev.Pickup, ev.Rigidbody);
        cur_item.OnThrew(ev.Player, ev.Pickup, ev.Rigidbody);
    }
    #endregion
    public override void OnPlayerSearchingPickup(PlayerSearchingPickupEventArgs ev)
    {
        if (!CustomItems.TryGetCustomItem(ev.Pickup, out CustomItemBase cur_item))
            return;
        TypeWrapper<bool> isAllowedHelper = new(ev.IsAllowed);
        CustomItemEvents.OnSearching(cur_item, ev.Player, ev.Pickup, isAllowedHelper);
        cur_item.OnSearching(ev.Player, ev.Pickup, isAllowedHelper);
        ev.IsAllowed = isAllowedHelper.Value;
    }
    public override void OnPlayerSearchedPickup(PlayerSearchedPickupEventArgs ev)
    {
        if (!CustomItems.TryGetCustomItem(ev.Pickup, out CustomItemBase cur_item))
            return;
        CustomItemEvents.OnSearched(cur_item, ev.Player, ev.Pickup);
        cur_item.OnSearched(ev.Player, ev.Pickup);
    }
    public override void OnPlayerChangedSpectator(PlayerChangedSpectatorEventArgs ev)
    {
        if (ev.Player.Role != RoleTypeId.Spectator || ev.NewTarget == null)
            return;
        if (CustomItems.TryGetCustomItem(ev.NewTarget.CurrentItem, out CustomItemBase cur_item))
            cur_item.OnPlayerChangedSpectator(ev);
        else
            HintShow?.Invoke(ev.Player, string.Empty);
    }
}
