using InventorySystem.Items.MicroHID.Modules;

namespace CustomItemsAPI.Items;

/// <summary>
/// Custom <see cref="MicroHIDItem"/> base.
/// </summary>
public abstract class CustomMicroHIDBase : CustomItemBase
{
	/// <inheritdoc/>
	public override void Parse(Item item)
	{
		base.Parse(item);
		if (item is null)
		{
			return;
		}

		if (item.Type != ItemType.MicroHID)
		{
			throw new ArgumentOutOfRangeException(nameof(item), item.Type, "Invalid MicroHID type.");
		}

		if (item is not MicroHIDItem)
		{
			throw new ArgumentException("MicroHIDItem must not be null!");
		}
	}

	/// <summary>
	/// Called when this <paramref name="microHIDItem"/>'s <see cref="MicroHidPhase"/> changed.
	/// </summary>
	/// <param name="microHIDItem">The item.</param>
	/// <param name="phase">The micro hid phase.</param>
	public virtual void OnPhaseChanged(MicroHIDItem microHIDItem, MicroHidPhase phase)
	{
	}

	/// <summary>
	/// Called when this <paramref name="microHIDItem"/> is broken by <see cref="BrokenSyncModule"/>.
	/// </summary>
	/// <param name="microHIDItem">The item.</param>
	public virtual void OnBroken(MicroHIDItem microHIDItem)
	{
	}
}
