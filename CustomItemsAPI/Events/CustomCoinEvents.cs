using CustomItemsAPI.Items;

namespace CustomItemsAPI.Events;

#pragma warning disable CA1003 // Use generic event handler instances

/// <summary>
/// Events for calling methods for <see cref="CustomCoinBase"/>.
/// </summary>
public static class CustomCoinEvents
{
	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.FlippingCoin"/>
	public static event Action<CustomCoinBase, Player, CoinItem, TypeWrapper<bool>, TypeWrapper<bool>>? Flipping;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.FlippedCoin"/>
	public static event Action<CustomCoinBase, Player, CoinItem, bool>? Flipped;

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.FlippingCoin"/>
	public static void OnFlipping(CustomCoinBase customItem, Player player, CoinItem coin, TypeWrapper<bool> isTails, TypeWrapper<bool> isAllowedHelper)
		=> Flipping?.Invoke(customItem, player, coin, isTails, isAllowedHelper);

	/// <inheritdoc cref="LabApi.Events.Handlers.PlayerEvents.FlippedCoin"/>
	public static void OnFlipped(CustomCoinBase customItem, Player player, CoinItem coin, bool isTails)
		=> Flipped?.Invoke(customItem, player, coin, isTails);
}
