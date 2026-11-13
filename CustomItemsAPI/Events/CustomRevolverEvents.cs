using CustomItemsAPI.Items;

namespace CustomItemsAPI.Events;

#pragma warning disable CA1003 // Use generic event handler instances

/// <summary>
/// Events for calling methods for <see cref="CustomRevolverBase"/>.
/// </summary>
public static class CustomRevolverEvents
{
	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.SpinnedRevolver"/>
	public static event Action<CustomRevolverBase, Player, RevolverFirearm>? Spinned;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.SpinningRevolver"/>
	public static event Action<CustomRevolverBase, Player, RevolverFirearm, TypeWrapper<bool>>? Spinning;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.SpinnedRevolver"/>
	public static void OnSpinned(CustomRevolverBase custom, Player player, RevolverFirearm revolver)
		=> Spinned?.Invoke(custom, player, revolver);

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.SpinningRevolver"/>
	public static void OnSpinning(CustomRevolverBase custom, Player player, RevolverFirearm revolver, TypeWrapper<bool> isAllowed)
		=> Spinning?.Invoke(custom, player, revolver, isAllowed);
}
