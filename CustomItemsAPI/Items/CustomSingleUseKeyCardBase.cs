using Interactables.Interobjects.DoorUtils;
using InventorySystem.Items.Keycards;

namespace CustomItemsAPI.Items;

/// <summary>
/// Custom Item Base for <see cref="ItemCategory.Keycard"/> that is <see cref="ItemType.SurfaceAccessPass"/>.
/// </summary>
public abstract class CustomSingleUseKeyCardBase : CustomKeycardBase
{
	/// <summary>
	/// Gets the single usage <see cref="DoorPermissionFlags"/> for this keycard.
	/// </summary>
	public virtual DoorPermissionFlags SingleUsePermission { get; } = DoorPermissionFlags.None;

	/// <summary>
	/// Gets the value for allowing closing doors.
	/// </summary>
	public virtual bool? AllowClosingDoors { get; }

	/// <summary>
	/// Gets the time to destroy this item.
	/// </summary>
	public virtual float TimeToDestroy { get; } = float.NaN;

	/// <inheritdoc/>
	public override void Parse(Item item)
	{
		base.Parse(item);
		if (item is null)
		{
			return;
		}

		if (item.Base is not SingleUseKeycardItem single)
		{
			throw new InvalidOperationException("Item must be SingleUseKeycardItem!");
		}

		single._singleUsePermissions = SingleUsePermission;
		if (AllowClosingDoors.HasValue)
		{
			single._allowClosingDoors = AllowClosingDoors.Value;
		}

		if (!float.IsNaN(TimeToDestroy))
		{
			single._timeToDestroy = TimeToDestroy;
		}
	}
}
