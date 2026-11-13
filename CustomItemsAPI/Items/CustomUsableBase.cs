namespace CustomItemsAPI.Items;

/// <summary>
/// Custom <see cref="UsableItem"/> base.
/// </summary>
public abstract class CustomUsableBase : CustomItemBase
{
	/// <inheritdoc/>
	public override void Parse(Item item)
	{
		base.Parse(item);
		if (item is not UsableItem)
		{
			throw new ArgumentException("Usable must not be null!");
		}
	}

	/// <summary>
	/// This <paramref name="player"/> who cancelled using this item.
	/// </summary>
	/// <param name="player">The Player who called this function.</param>
	/// <param name="usableItem">The usable item.</param>
	public virtual void OnCancelled(Player player, UsableItem usableItem)
	{
	}

	/// <summary>
	/// This <paramref name="player"/> who cancelling using this item.
	/// </summary>
	/// <param name="player">The Player who called this function.</param>
	/// <param name="usableItem">The usable item.</param>
	/// <param name="isAllowed">Can allow this action.</param>
	public virtual void OnCancelling(Player player, UsableItem usableItem, TypeWrapper<bool> isAllowed)
	{
	}

	/// <summary>
	/// This <paramref name="player"/> who used this item.
	/// </summary>
	/// <param name="player">The Player who called this function.</param>
	/// <param name="usableItem">The usable item.</param>
	public virtual void OnUsed(Player player, UsableItem usableItem)
	{
	}

	/// <summary>
	/// This <paramref name="player"/> who using this item.
	/// </summary>
	/// <param name="player">The Player who called this function.</param>
	/// <param name="usableItem">The usable item.</param>
	/// <param name="isAllowed">Can allow this action.</param>
	public virtual void OnUsing(Player player, UsableItem usableItem, TypeWrapper<bool> isAllowed)
	{
	}
}
