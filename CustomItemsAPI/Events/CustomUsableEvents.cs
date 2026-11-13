using CustomItemsAPI.Items;

namespace CustomItemsAPI.Events;

#pragma warning disable CA1003 // Use generic event handler instances

/// <summary>
/// Events for calling methods for <see cref="CustomUsableBase"/>.
/// </summary>
public static class CustomUsableEvents
{
	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.CancelledUsingItem"/>
	public static event Action<CustomUsableBase, Player, UsableItem>? Cancelled;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.CancellingUsingItem"/>
	public static event Action<CustomUsableBase, Player, UsableItem, TypeWrapper<bool>>? Cancelling;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.UsedItem"/>
	public static event Action<CustomUsableBase, Player, UsableItem>? Used;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.UsingItem"/>
	public static event Action<CustomUsableBase, Player, UsableItem, TypeWrapper<bool>>? Using;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.CancelledUsingItem"/>
	public static void OnCancelled(CustomUsableBase custom, Player player, UsableItem usableItem)
		=> Cancelled?.Invoke(custom, player, usableItem);

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.CancellingUsingItem"/>
	public static void OnCancelling(CustomUsableBase custom, Player player, UsableItem usableItem, TypeWrapper<bool> isAllowed)
		=> Cancelling?.Invoke(custom, player, usableItem, isAllowed);

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.UsedItem"/>
	public static void OnUsed(CustomUsableBase custom, Player player, UsableItem usableItem)
		=> Used?.Invoke(custom, player, usableItem);

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.UsingItem"/>
	public static void OnUsing(CustomUsableBase custom, Player player, UsableItem usableItem, TypeWrapper<bool> isAllowed)
		=> Using?.Invoke(custom, player, usableItem, isAllowed);
}
