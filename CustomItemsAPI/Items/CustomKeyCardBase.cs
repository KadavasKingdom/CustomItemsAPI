using Interactables.Interobjects.DoorUtils;
using InventorySystem;
using LabApi.Features.Wrappers;
using UnityEngine;
using IC = InventorySystem.Items.Keycards;

namespace CustomItemsAPI.Items;

/// <summary>
/// Custom Item Base for <see cref="ItemCategory.Keycard"/>.
/// </summary>
public abstract class CustomKeycardBase : CustomItemBase
{
    /// <summary>
    /// Sets the permissions for custom keycard.
    /// </summary>
    public virtual KeycardLevels? Levels { get; }
    /// <summary>
    /// Sets the Color of the Permisson if exists.
    /// </summary>
    public virtual Color? PermissionColor { get; } = Color.black;

    /// <summary>
    /// Sets the Color of the Tint if exists.
    /// </summary>
    public virtual Color? TintColor { get; }
    /// <summary>
    /// Sets the Wear Level of the keycard if exists.
    /// </summary>
    public virtual byte? WearLevel { get; }

    /// <summary>
    /// Sets the Rank Index of the keycard if exists.
    /// </summary>
    public virtual int RankIndex { get; }

    /// <summary>
    /// Sets the Custom Inventory Name of the keycard if exists.
    /// </summary>
    public virtual string CustomName { get; } = string.Empty;

    /// <summary>
    /// Sets the Custom Serial Id of the keycard if exists.
    /// </summary>
    public virtual string CustomSerial { get; } = string.Empty;

    /// <summary>
    /// Sets the Custom Name Tag of the keycard if exists.
    /// </summary>
    public virtual string CustomNameTag { get; } = string.Empty;

    /// <summary>
    /// Sets the Custom Label Name of the keycard if exists.
    /// </summary>
    public virtual string CustomLabelText { get; } = string.Empty;

    /// <summary>
    /// Sets the Custom Label Color of the keycard if exists.
    /// </summary>
    public virtual Color? CustomLabelColor { get; }

    /// <summary>
    /// Sets the the opening doors on throw property.
    /// </summary>
    public virtual bool? OpenDoorsOnThrow { get; }

    /// <inheritdoc/>
    public override void Parse(Pickup pickup)
    {
        base.Parse(pickup);
        if (pickup is not KeycardPickup keycardPickup)
            throw new Exception("Pickup is not a Keycard Pickup");
        if (OpenDoorsOnThrow.HasValue)
            keycardPickup.Base._openDoorsOnCollision = OpenDoorsOnThrow.Value;
        if (!keycardPickup.Base.TryGetTemplate<IC.KeycardItem>(out var template))
            throw new Exception("Cannot get a template for this item");
        if (!template.Customizable)
            return;

        SetCustomDetails(template);
    }

    /// <inheritdoc/>
    public override void Parse(Item item)
    {
        base.Parse(item);
        if (item.Category != ItemCategory.Keycard)
            throw new ArgumentOutOfRangeException("Type", item.Type, "Invalid Keycard type.");
        if (item.Base is not IC.KeycardItem keycard)
            throw new ArgumentException($"keycard must not be null! {item.GetType()}");
        if (OpenDoorsOnThrow.HasValue)
            keycard.OpenDoorsOnThrow = OpenDoorsOnThrow.Value;
        if (!keycard.Customizable)
            return;

        SetCustomDetails(keycard);
    }

    private void SetCustomDetails(IC.KeycardItem keycard)
    {
        CL.Info($"Setting custom details for keycard {keycard.ItemSerial}, {keycard.name}, {keycard.DisplayName}");
        // getting all than later checking
        // me when a keycard isnt customisable cant do shit with these.
        IC.NametagDetail? nametag = keycard.Details.OfType<IC.NametagDetail>().FirstOrDefault();
        IC.CustomPermsDetail? customPermsDetail = keycard.Details.OfType<IC.CustomPermsDetail>().FirstOrDefault();
        IC.CustomTintDetail? customTint = keycard.Details.OfType<IC.CustomTintDetail>().FirstOrDefault();
        IC.CustomWearDetail? customWear = keycard.Details.OfType<IC.CustomWearDetail>().FirstOrDefault();
        IC.CustomItemNameDetail? customItemName = keycard.Details.OfType<IC.CustomItemNameDetail>().FirstOrDefault();
        IC.CustomSerialNumberDetail? customSerialNumber = keycard.Details.OfType<IC.CustomSerialNumberDetail>().FirstOrDefault();
        IC.CustomLabelDetail? customLabel = keycard.Details.OfType<IC.CustomLabelDetail>().FirstOrDefault();
        IC.CustomRankDetail? customRank = keycard.Details.OfType<IC.CustomRankDetail>().FirstOrDefault();

        if (customRank != null)
            IC.CustomRankDetail._index = RankIndex;

        if (Levels.HasValue && customPermsDetail != null)
            IC.CustomPermsDetail._customLevels = Levels.Value;

        if (PermissionColor.HasValue && customPermsDetail != null)
            IC.CustomPermsDetail._customColor = PermissionColor;

        if (TintColor.HasValue && customTint != null)
            IC.CustomTintDetail._customColor = TintColor.Value;

        if (WearLevel.HasValue && customWear != null)
            IC.CustomWearDetail._customWearLevel = WearLevel.Value;

        if (nametag != null && !string.IsNullOrEmpty(CustomNameTag))
            IC.NametagDetail._customNametag = CustomNameTag;

        if (customItemName != null && !string.IsNullOrEmpty(CustomName))
            IC.CustomItemNameDetail._customText = CustomName;

        if (customSerialNumber != null && !string.IsNullOrEmpty(CustomSerial))
            IC.CustomSerialNumberDetail._customVal = CustomSerial;

        if (customLabel != null)
        {
            if (!string.IsNullOrEmpty(CustomLabelText))
                IC.CustomLabelDetail._customText = CustomLabelText;
            if (CustomLabelColor.HasValue)
                IC.CustomLabelDetail._customColor = CustomLabelColor.Value;   
        }

        IC.KeycardDetailSynchronizer.Database.Remove(keycard.ItemSerial);
        IC.KeycardDetailSynchronizer.ServerProcessItem(keycard);
    }

    /// <summary>
    /// Called when <paramref name="player"/> is interacted with the <paramref name="door"/>.
    /// </summary>
    /// <param name="player">The Player who called this function.</param>
    /// <param name="item"></param>
    /// <param name="door">The <see cref="Door"/> the <paramref name="player"/> interacted.</param>
    /// <param name="canOpen">The <paramref name="player"/> can open the <paramref name="door"/>.</param>
    public virtual void OnInteractedDoor(Player player, Item item, Door door, bool canOpen)
    {
        CL.Debug($"OnInteractedDoor {player.PlayerId} {door.DoorName} {canOpen}", Main.Instance.Config.Debug);
    }

    /// <summary>
    /// Called when <paramref name="player"/> is interacting with the <paramref name="door"/>.
    /// </summary>
    /// <param name="player">The Player who called this function.</param>
    /// <param name="item"></param>
    /// <param name="door">The <see cref="Door"/> the <paramref name="player"/> interacting.</param>
    /// <param name="canOpen">Can the <paramref name="player"/> open the <paramref name="door"/>.</param>
    /// <param name="isAllowed">Can allow this action.</param>
    public virtual void OnInteractingDoor(Player player, Item item, Door door, TypeWrapper<bool> canOpen, TypeWrapper<bool> isAllowed)
    {
        CL.Debug($"OnInteractingDoor {player.PlayerId} {door.DoorName} {canOpen.Value}", Main.Instance.Config.Debug);
    }

    /// <summary>
    /// Called when <paramref name="player"/> is interacted with the <paramref name="generator"/>.
    /// </summary>
    /// <param name="player">The Player who called this function.</param>
    /// <param name="item"></param>
    /// <param name="generator">The <see cref="Generator"/> the <paramref name="player"/> interacted.</param>
    /// <param name="colliderId"></param>
    public virtual void OnInteractedGenerator(Player player, Item item, Generator generator, MapGeneration.Distributors.Scp079Generator.GeneratorColliderId colliderId)
    {
        CL.Debug($"OnInteractedGenerator {player.PlayerId} {generator.Base}", Main.Instance.Config.Debug);
    }

    /// <summary>
    /// Called when <paramref name="player"/> is interacting with the <paramref name="generator"/>.
    /// </summary>
    /// <param name="player">The Player who called this function.</param>
    /// <param name="item"></param>
    /// <param name="generator">The <see cref="Generator"/> the <paramref name="player"/> interacting.</param>
    /// <param name="colliderIdHelper"></param>
    /// <param name="isAllowed">Can allow this action.</param>
    public virtual void OnInteractingGenerator(Player player, Item item, Generator generator, TypeWrapper<MapGeneration.Distributors.Scp079Generator.GeneratorColliderId> colliderIdHelper, TypeWrapper<bool> isAllowed)
    {
        CL.Debug($"OnInteractingGenerator {player.PlayerId} {generator.Base}", Main.Instance.Config.Debug);
    }

    /// <summary>
    /// Called when <paramref name="player"/> is interacted with the <paramref name="locker"/>.
    /// </summary>
    /// <param name="player">The Player who called this function.</param>
    /// <param name="item"></param>
    /// <param name="locker">The <see cref="Locker"/> the <paramref name="player"/> interacted.</param>
    /// <param name="lockerChamber">The targeted <see cref="LockerChamber"/>.</param>
    /// <param name="canOpen">The <paramref name="player"/> can open the <paramref name="locker"/>.</param>
    public virtual void OnInteractedLocker(Player player, Item item, Locker locker, LockerChamber lockerChamber, bool canOpen)
    {
        CL.Debug($"OnInteractedLocker {player.PlayerId} {locker} {lockerChamber.Id} {canOpen}", Main.Instance.Config.Debug);
    }

    /// <summary>
    /// Called when <paramref name="player"/> is interacting with the <paramref name="locker"/>.
    /// </summary>
    /// <param name="player">The Player who called this function.</param>
    /// <param name="item"></param>
    /// <param name="locker">The <see cref="Locker"/> the <paramref name="player"/> interacting.</param>
    /// <param name="lockerChamber">The targeted <see cref="LockerChamber"/>.</param>
    /// <param name="canOpen">Can the <paramref name="player"/> open the <paramref name="locker"/>.</param>
    /// <param name="isAllowed">Can allow this action.</param>
    public virtual void OnInteractingLocker(Player player, Item item, Locker locker, LockerChamber lockerChamber, TypeWrapper<bool> canOpen, TypeWrapper<bool> isAllowed)
    {
        CL.Debug($"OnInteractingLocker {player.PlayerId} {locker} {lockerChamber.Id} {canOpen.Value}", Main.Instance.Config.Debug);
    }

    /// <summary>
    /// Called when <paramref name="player"/> is inspected the <paramref name="keycardItem"/>.
    /// </summary>
    /// <param name="player"></param>
    /// <param name="keycardItem"></param>
    public virtual void OnInspectedKeycard(Player player, KeycardItem keycardItem)
    {
        CL.Debug($"OnInspectingKeycard {player.PlayerId} {keycardItem.Serial}", Main.Instance.Config.Debug);
    }

    /// <summary>
    /// Called when <paramref name="player"/> is inspecting the <paramref name="keycardItem"/>.
    /// </summary>
    /// <param name="player"></param>
    /// <param name="keycardItem"></param>
    /// <param name="isAllowedHelper"></param>
    public virtual void OnInspectingKeycard(Player player, KeycardItem keycardItem, TypeWrapper<bool> isAllowedHelper)
    {
        CL.Debug($"OnInspectingKeycard {player.PlayerId} {keycardItem.Serial}", Main.Instance.Config.Debug);
    }
}
