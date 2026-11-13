using CustomItemsAPI.Items;
using PlayerRoles;

namespace CustomItemsAPI.Events;

#pragma warning disable CA1003 // Use generic event handler instances

/// <summary>
/// Events for calling methods for <see cref="CustomScp1509Base"/>.
/// </summary>
public static class CustomScp1509Events
{
	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.ProcessingScp1509Message"/>
	public static event Action<CustomScp1509Base, Player, Scp1509Item, TypeWrapper<InventorySystem.Items.Scp1509.Scp1509MessageType>, TypeWrapper<bool>, TypeWrapper<bool>, TypeWrapper<bool>>? ProcessingScp1509Message;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.ProcessedScp1509Message"/>
	public static event Action<CustomScp1509Base, Player, Scp1509Item, InventorySystem.Items.Scp1509.Scp1509MessageType>? ProcessedScp1509Message;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.Scp1509Resurrecting"/>
	public static event Action<CustomScp1509Base, Player, Scp1509Item, Player, Player, RoleTypeId>? Resurrecting;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.Scp1509Resurrected"/>
	public static event Action<CustomScp1509Base, Player, Scp1509Item, Player, TypeWrapper<Player?>, TypeWrapper<RoleTypeId>, TypeWrapper<bool>>? Resurrected;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.ProcessingScp1509Message"/>
	public static void OnProcessingScp1509Message(CustomScp1509Base customItem, Player player, Scp1509Item scp1509Item, TypeWrapper<InventorySystem.Items.Scp1509.Scp1509MessageType> message, TypeWrapper<bool> allowInspectHelper, TypeWrapper<bool> allowAttackHelper, TypeWrapper<bool> isAllowedHelper)
		=> ProcessingScp1509Message?.Invoke(customItem, player, scp1509Item, message, allowInspectHelper, allowAttackHelper, isAllowedHelper);

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.ProcessedScp1509Message"/>
	public static void OnProcessedScp1509Message(CustomScp1509Base customItem, Player player, Scp1509Item scp1509Item, InventorySystem.Items.Scp1509.Scp1509MessageType message)
		=> ProcessedScp1509Message?.Invoke(customItem, player, scp1509Item, message);

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.Scp1509Resurrected"/>
	internal static void OnResurrected(CustomScp1509Base cur_item, Player player, Scp1509Item item, Player killedPlayer, Player revivedPlayer, RoleTypeId respawnRole)
		=> Resurrecting?.Invoke(cur_item, player, item, killedPlayer, revivedPlayer, respawnRole);

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.Scp1509Resurrected"/>
	internal static void OnResurrecting(CustomScp1509Base cur_item, Player player, Scp1509Item item, Player killedPlayer, TypeWrapper<Player?> revivedPlayerHelper, TypeWrapper<RoleTypeId> respawnRoleHelper, TypeWrapper<bool> isAllowedHelper)
		=> Resurrected?.Invoke(cur_item, player, item, killedPlayer, revivedPlayerHelper, respawnRoleHelper, isAllowedHelper);
}
