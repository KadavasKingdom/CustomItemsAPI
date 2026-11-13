using CustomItemsAPI.Items;
using InventorySystem.Items.MicroHID.Modules;

namespace CustomItemsAPI.Events;

#pragma warning disable CA1003 // Use generic event handler instances

/// <summary>
/// Events for calling methods for <see cref="CustomMicroHIDBase"/>.
/// </summary>
public static class CustomMicroHIDEvents
{
	/// <inheritdoc cref="CustomMicroHIDBase.OnPhaseChanged(MicroHIDItem, MicroHidPhase)"/>
	public static event Action<CustomMicroHIDBase, MicroHIDItem, MicroHidPhase>? PhaseChanged;

	/// <inheritdoc cref="CustomMicroHIDBase.OnBroken(MicroHIDItem)"/>
	public static event Action<CustomMicroHIDBase, MicroHIDItem>? Broken;

	/// <inheritdoc cref="CustomMicroHIDBase.OnPhaseChanged(MicroHIDItem, MicroHidPhase)"/>
	public static void OnPhaseChanged(CustomMicroHIDBase customMicro, MicroHIDItem microHIDItem, MicroHidPhase phase)
		=> PhaseChanged?.Invoke(customMicro, microHIDItem, phase);

	/// <inheritdoc cref="CustomMicroHIDBase.OnBroken(MicroHIDItem)"/>
	public static void OnBroken(CustomMicroHIDBase customMicro, MicroHIDItem microHIDItem)
		=> Broken?.Invoke(customMicro, microHIDItem);
}
