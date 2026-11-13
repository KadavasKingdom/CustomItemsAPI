using CustomItemsAPI.Items;

namespace CustomItemsAPI.Events;

#pragma warning disable CA1003 // Use generic event handler instances

/// <summary>
/// Events for calling methods for <see cref="CustomJailbirdBase"/>.
/// </summary>
public static class CustomJailbirdEvents
{
	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.ProcessingJailbirdMessage"/>
	public static event Action<CustomJailbirdBase, Player, JailbirdItem, TypeWrapper<InventorySystem.Items.Jailbird.JailbirdMessageType>, TypeWrapper<bool>, TypeWrapper<bool>, TypeWrapper<bool>>? ProcessingJailbirdMessage;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.ProcessedJailbirdMessage"/>
	public static event Action<CustomJailbirdBase, Player, JailbirdItem, InventorySystem.Items.Jailbird.JailbirdMessageType>? ProcessedJailbirdMessage;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.ProcessingJailbirdMessage"/>
	public static void OnProcessingJailbirdMessage(CustomJailbirdBase customItem, Player player, JailbirdItem jailbirdItem, TypeWrapper<InventorySystem.Items.Jailbird.JailbirdMessageType> message, TypeWrapper<bool> allowInspectHelper, TypeWrapper<bool> allowAttackHelper, TypeWrapper<bool> isAllowedHelper)
		=> ProcessingJailbirdMessage?.Invoke(customItem, player, jailbirdItem, message, allowInspectHelper, allowAttackHelper, isAllowedHelper);

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.ProcessedJailbirdMessage"/>
	public static void OnProcessedJailbirdMessage(CustomJailbirdBase customItem, Player player, JailbirdItem jailbirdItem, InventorySystem.Items.Jailbird.JailbirdMessageType message)
		=> ProcessedJailbirdMessage?.Invoke(customItem, player, jailbirdItem, message);
}
