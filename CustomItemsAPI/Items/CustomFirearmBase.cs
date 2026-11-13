using CustomItemsAPI.Extensions;
using InventorySystem.Items.Firearms.Attachments;
using PlayerStatsSystem;
using System.Diagnostics.CodeAnalysis;

namespace CustomItemsAPI.Items;

/// <summary>
/// Custom <see cref="FirearmItem"/> base.
/// </summary>
public abstract class CustomFirearmBase : CustomItemBase
{
	/// <summary>
	/// Gets the <see cref="FirearmItem"/>'s Damage.
	/// </summary>
	public virtual MathValueFloat Damage { get; } = new();

	/// <summary>
	/// Gets override certain Classes.
	/// </summary>
	public virtual List<IOverride> Overridables { get; } = [];

	/// <summary>
	/// Gets the Attachment of the gun.
	/// </summary>
	public virtual List<AttachmentName> AttachmentNames { get; } = [];

	/// <inheritdoc/>
	public override void Parse(Item item)
	{
		base.Parse(item);
		if (item is not FirearmItem firearmItem)
		{
			throw new ArgumentException("FirearmItem must not be null!");
		}

		firearmItem.AttachmentsCode = firearmItem.GetCodeFromAttachmentNamesRaw([.. AttachmentNames]);
		foreach (var @override in Overridables)
		{
			if (FirearmExtension.TryGetModule(firearmItem.Base, @override.OverrideType, out object? module, false))
			{
				@override.Apply(ref module);
			}
		}
	}

	/// <inheritdoc/>
	public override void Parse(Pickup pickup)
	{
		base.Parse(pickup);
		if (pickup is not FirearmPickup firearmPickup)
		{
			throw new ArgumentException("FirearmPickup must not be null!");
		}

		firearmPickup.AttachmentCode = FirearmItem.Get(firearmPickup.Base.Template).GetCodeFromAttachmentNamesRaw([.. AttachmentNames]);
	}

	/// <summary>
	/// This <paramref name="player"/> who aimed.
	/// </summary>
	/// <param name="player">The Player who called this function.</param>
	/// <param name="weapon">The weapon.</param>
	/// <param name="aiming">Is aiming or not.</param>
	public virtual void OnAim(Player player, FirearmItem weapon, bool aiming)
	{
	}

	/// <summary>
	/// This <paramref name="player"/> who dry fired.
	/// </summary>
	/// <param name="player">The Player who called this function.</param>
	/// <param name="weapon">The weapon.</param>
	public virtual void OnDryFired(Player player, FirearmItem weapon)
	{
	}

	/// <summary>
	/// This <paramref name="player"/> who dry firing.
	/// </summary>
	/// <param name="player">The Player who called this function.</param>
	/// <param name="weapon">The weapon.</param>
	/// <param name="isAllowedHelper">Can allow this action.</param>
	public virtual void OnDryFiring(Player player, FirearmItem weapon, [NotNull] TypeWrapper<bool> isAllowedHelper)
	{
	}

	/// <summary>
	/// This <paramref name="player"/> who reloaded.
	/// </summary>
	/// <param name="player">The Player who called this function.</param>
	/// <param name="weapon">The weapon.</param>
	public virtual void OnReloaded(Player player, FirearmItem weapon)
	{
	}

	/// <summary>
	/// This <paramref name="player"/> who reloading.
	/// </summary>
	/// <param name="player">The Player who called this function.</param>
	/// <param name="weapon">The weapon.</param>
	/// <param name="isAllowedHelper">Can allow this action.</param>
	public virtual void OnReloading(Player player, FirearmItem weapon, [NotNull] TypeWrapper<bool> isAllowedHelper)
	{
	}

	/// <summary>
	/// This <paramref name="player"/> who shot.
	/// </summary>
	/// <param name="player">The Player who called this function.</param>
	/// <param name="weapon">The weapon.</param>
	public virtual void OnShot(Player player, FirearmItem weapon)
	{
	}

	/// <summary>
	/// This <paramref name="player"/> who shooting.
	/// </summary>
	/// <param name="player">The Player who called this function.</param>
	/// <param name="weapon">The weapon.</param>
	/// <param name="isAllowedHelper">Can allow this action.</param>
	public virtual void OnShooting(Player player, FirearmItem weapon, [NotNull] TypeWrapper<bool> isAllowedHelper)
	{
	}

	/// <summary>
	/// This <paramref name="player"/> who toggled the flashlight.
	/// </summary>
	/// <param name="player">The Player who called this function.</param>
	/// <param name="weapon">The weapon.</param>
	/// <param name="newState">State of flashlight.</param>
	public virtual void OnToggledFlashlight(Player player, FirearmItem weapon, bool newState)
	{
	}

	/// <summary>
	/// This <paramref name="player"/> who toggling the flashlight.
	/// </summary>
	/// <param name="player">The Player who called this function.</param>
	/// <param name="weapon">The weapon.</param>
	/// <param name="newState">State of flashlight.</param>
	/// <param name="isAllowedHelper">Can allow this action.</param>
	public virtual void OnTogglingFlashlight(Player player, FirearmItem weapon, [NotNull] TypeWrapper<bool> newState, [NotNull] TypeWrapper<bool> isAllowedHelper)
	{
	}

	/// <summary>
	/// This <paramref name="player"/> who unloaded.
	/// </summary>
	/// <param name="player">The Player who called this function.</param>
	/// <param name="weapon">The weapon.</param>
	public virtual void OnUnloaded(Player player, FirearmItem weapon)
	{
	}

	/// <summary>
	/// This <paramref name="player"/> who unloading.
	/// </summary>
	/// <param name="player">The Player who called this function.</param>
	/// <param name="weapon">The weapon.</param>
	/// <param name="isAllowedHelper">Can allow this action.</param>
	public virtual void OnUnloading(Player player, FirearmItem weapon, [NotNull] TypeWrapper<bool> isAllowedHelper)
	{
	}

	/// <summary>
	/// This <paramref name="player"/> who got hurt.
	/// </summary>
	/// <param name="player">The Player who called this function.</param>
	/// <param name="attacker">The Player who attacker the <paramref name="player"/>.</param>
	/// <param name="firearmDamage">Firearm Damage.</param>
	public virtual void OnHurt(Player player, Player attacker, FirearmDamageHandler firearmDamage)
	{
	}

	/// <summary>
	/// This <paramref name="player"/> who got hurt.
	/// </summary>
	/// <param name="player">The Player who called this function.</param>
	/// <param name="attacker">The Player who attacker the <paramref name="player"/>.</param>
	/// <param name="firearmDamage">Firearm Damage.</param>
	/// <param name="isAllowedHelper">Can allow this action.</param>
	public virtual void OnHurting(Player player, Player attacker, FirearmDamageHandler firearmDamage, [NotNull] TypeWrapper<bool> isAllowedHelper)
	{
		if (firearmDamage is null)
		{
			return;
		}

		firearmDamage.Damage = Damage.MathCalculation(firearmDamage.Damage);
	}

	/// <summary>
	/// This <paramref name="player"/> changed <paramref name="firearmItem"/> attachment from <paramref name="oldAttachments"/> to <paramref name="newAttachments"/>.
	/// </summary>
	/// <param name="player">The Player who called this function.</param>
	/// <param name="firearmItem">The firearm.</param>
	/// <param name="oldAttachments">The old attachments.</param>
	/// <param name="newAttachments">The new attachments.</param>
	public virtual void OnChangedAttachments(Player player, FirearmItem firearmItem, uint oldAttachments, uint newAttachments)
	{
	}

	/// <summary>
	/// This <paramref name="player"/> changing <paramref name="firearmItem"/> attachment from <paramref name="oldAttachments"/> to <paramref name="newAttachments"/>.
	/// </summary>
	/// <param name="player">The Player who called this function.</param>
	/// <param name="firearmItem">The firearm.</param>
	/// <param name="oldAttachments">The old attachments.</param>
	/// <param name="newAttachments">The new attachments.</param>
	/// <param name="isAllowedHelper">Can allow this action.</param>
	public virtual void OnChangingAttachments(Player player, FirearmItem firearmItem, uint oldAttachments, [NotNull] TypeWrapper<uint> newAttachments, [NotNull] TypeWrapper<bool> isAllowedHelper)
	{
	}
}
