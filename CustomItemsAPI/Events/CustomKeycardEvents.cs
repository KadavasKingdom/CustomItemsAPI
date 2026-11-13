using CustomItemsAPI.Items;
using GeneratorColliderId = MapGeneration.Distributors.Scp079Generator.GeneratorColliderId;

namespace CustomItemsAPI.Events;

#pragma warning disable CA1003 // Use generic event handler instances

/// <summary>
/// Events for calling methods for <see cref="CustomKeycardBase"/>.
/// </summary>
public static class CustomKeycardEvents
{
	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.InteractedDoor"/>
	public static event Action<CustomKeycardBase, Player, Item, Door, bool>? InteractedDoor;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.InteractingDoor"/>
	public static event Action<CustomKeycardBase, Player, Item, Door, TypeWrapper<bool>, TypeWrapper<bool>>? InteractingDoor;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.InteractedGenerator"/>
	public static event Action<CustomKeycardBase, Player, Item, Generator, GeneratorColliderId>? InteractedGenerator;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.InteractingGenerator"/>
	public static event Action<CustomKeycardBase, Player, Item, Generator, TypeWrapper<GeneratorColliderId>, TypeWrapper<bool>>? InteractingGenerator;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.InteractedLocker"/>
	public static event Action<CustomKeycardBase, Player, Item, Locker, LockerChamber, bool>? InteractedLocker;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.InteractingLocker"/>
	public static event Action<CustomKeycardBase, Player, Item, Locker, LockerChamber, TypeWrapper<bool>, TypeWrapper<bool>>? InteractingLocker;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.InspectedKeycard"/>
	public static event Action<CustomKeycardBase, Player, KeycardItem>? InspectedKeycard;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.InspectingKeycard"/>
	public static event Action<CustomKeycardBase, Player, KeycardItem, TypeWrapper<bool>>? InspectingKeycard;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.InteractedDoor"/>
	public static void OnInteractedDoor(CustomKeycardBase customItem, Player player, Item item, Door door, bool canOpen)
		=> InteractedDoor?.Invoke(customItem, player, item, door, canOpen);

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.InteractingDoor"/>
	public static void OnInteractingDoor(CustomKeycardBase customItem, Player player, Item item, Door door, TypeWrapper<bool> canOpen, TypeWrapper<bool> isAllowed)
		=> InteractingDoor?.Invoke(customItem, player, item, door, canOpen, isAllowed);

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.InteractedGenerator"/>
	public static void OnInteractedGenerator(CustomKeycardBase customItem, Player player, Item item, Generator generator, GeneratorColliderId colliderId)
		=> InteractedGenerator?.Invoke(customItem, player, item, generator, colliderId);

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.InteractingGenerator"/>
	public static void OnInteractingGenerator(CustomKeycardBase customItem, Player player, Item item, Generator generator, TypeWrapper<GeneratorColliderId> colliderId, TypeWrapper<bool> isAllowed)
		=> InteractingGenerator?.Invoke(customItem, player, item, generator, colliderId, isAllowed);

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.InteractedLocker"/>
	public static void OnInteractedLocker(CustomKeycardBase customItem, Player player, Item item, Locker locker, LockerChamber lockerChamber, bool canOpen)
		=> InteractedLocker?.Invoke(customItem, player, item, locker, lockerChamber, canOpen);

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.InteractingLocker"/>
	public static void OnInteractingLocker(CustomKeycardBase customItem, Player player, Item item, Locker locker, LockerChamber lockerChamber, TypeWrapper<bool> canOpen, TypeWrapper<bool> isAllowed)
		=> InteractingLocker?.Invoke(customItem, player, item, locker, lockerChamber, canOpen, isAllowed);

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.InspectedKeycard"/>
	public static void OnInspectedKeycard(CustomKeycardBase customItem, Player player, KeycardItem keycardItem)
		=> InspectedKeycard?.Invoke(customItem, player, keycardItem);

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.InspectingKeycard"/>
	public static void OnInspectingKeycard(CustomKeycardBase customItem, Player player, KeycardItem keycardItem, TypeWrapper<bool> isAllowed)
		=> InspectingKeycard?.Invoke(customItem, player, keycardItem, isAllowed);
}
