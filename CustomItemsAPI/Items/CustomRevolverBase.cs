namespace CustomItemsAPI.Items;

/// <summary>
/// Custom <see cref="RevolverFirearm"/> base.
/// </summary>
public abstract class CustomRevolverBase : CustomFirearmBase
{
	/// <inheritdoc/>
	public override void Parse(Item item)
	{
		base.Parse(item);
		if (item is not RevolverFirearm)
		{
			throw new ArgumentException("RevolverFirearm must not be null!");
		}
	}

	/// <summary>
	/// This <paramref name="player"/> who spinned the revolver.
	/// </summary>
	/// <param name="player">The Player who called this function.</param>
	/// <param name="revolver">The revolver.</param>
	public virtual void OnSpinned(Player player, RevolverFirearm revolver)
	{
	}

	/// <summary>
	/// This <paramref name="player"/> who spinning the revolver.
	/// </summary>
	/// <param name="player">The Player who called this function.</param>
	/// <param name="revolver">The revolver.</param>
	/// <param name="isAllowed">Can allow this action.</param>
	public virtual void OnSpinning(Player player, RevolverFirearm revolver, TypeWrapper<bool> isAllowed)
	{
	}
}
