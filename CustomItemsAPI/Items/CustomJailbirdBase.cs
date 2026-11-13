using JetBrains.Annotations;

namespace CustomItemsAPI.Items;

/// <summary>
/// Custom <see cref="JailbirdItem"/> base.
/// </summary>
public abstract class CustomJailbirdBase : CustomItemBase
{
	/// <summary>
	/// Gets or sets the override for <see cref="InventorySystem.Items.Jailbird.JailbirdItem"/>.
	/// </summary>
	public JailbirdItemOverride JailbirdItemOverride { get; set; } = new();

	/// <inheritdoc/>
	public override void Parse(Item item)
	{
		base.Parse(item);
		if (item is not JailbirdItem jailbird)
		{
			throw new ArgumentException("JailbirdItem must not be null!");
		}

		var jailbirdItemBase = jailbird.Base;
		JailbirdItemOverride.Apply(ref jailbirdItemBase);
	}

	/// <summary>
	/// Called Server processed a <paramref name="message"/> from <paramref name="jailbirdItem"/>.
	/// </summary>
	/// <param name="player">The Player who called this function.</param>
	/// <param name="jailbirdItem">The jailbird.</param>
	/// <param name="message">The jailbird message processed.</param>
	public virtual void OnProcessedJailbirdMessage(Player player, JailbirdItem jailbirdItem, InventorySystem.Items.Jailbird.JailbirdMessageType message)
	{
	}

	/// <summary>
	/// Called Server processing a <paramref name="message"/> from <paramref name="jailbirdItem"/>.
	/// </summary>
	/// <param name="player">The Player who called this function.</param>
	/// <param name="jailbirdItem">The jailbird.</param>
	/// <param name="message">The jailbird message to process.</param>
	/// <param name="allowInspectHelper">Can allow inspecting.</param>
	/// <param name="allowAttackHelper">Can allow attacking.</param>
	/// <param name="isAllowedHelper">Can allow this action.</param>
	public virtual void OnProcessingJailbirdMessage(
		Player player,
		JailbirdItem jailbirdItem,
		[NotNull] TypeWrapper<InventorySystem.Items.Jailbird.JailbirdMessageType> message,
		[NotNull] TypeWrapper<bool> allowInspectHelper,
		[NotNull] TypeWrapper<bool> allowAttackHelper,
		[NotNull] TypeWrapper<bool> isAllowedHelper)
	{
	}
}
