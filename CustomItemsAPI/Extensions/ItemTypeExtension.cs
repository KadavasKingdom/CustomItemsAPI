namespace CustomItemsAPI.Extensions;

/// <summary>
/// Extension for <see cref="ItemType"/>.
/// </summary>
public static class ItemTypeExtension
{
	/// <summary>
	/// Check if <paramref name="itemType"/> is an Armor.
	/// </summary>
	/// <param name="itemType">The <see cref="ItemType"/> to check against.</param>
	/// <returns><see langword="true"/> if it is an Armor otherwise <see langword="false"/>.</returns>
	public static bool IsArmor(this ItemType itemType) =>
	itemType is ItemType.ArmorCombat or ItemType.ArmorHeavy or ItemType.ArmorLight;

	/// <summary>
	/// Check if <paramref name="itemType"/> is an Ammo.
	/// </summary>
	/// <param name="itemType">The <see cref="ItemType"/> to check against.</param>
	/// <returns><see langword="true"/> if it is an Ammo otherwise <see langword="false"/>.</returns>
	public static bool IsAmmo(this ItemType itemType) =>
	itemType is ItemType.Ammo9x19 or ItemType.Ammo556x45 or ItemType.Ammo12gauge or ItemType.Ammo44cal or ItemType.Ammo762x39;

	/// <summary>
	/// Check if <paramref name="itemType"/> is a Keycard.
	/// </summary>
	/// <param name="itemType">The <see cref="ItemType"/> to check against.</param>
	/// <returns><see langword="true"/> if it is an Keycard otherwise <see langword="false"/>.</returns>
	public static bool IsKeycard(this ItemType itemType) =>
	itemType is ItemType.KeycardJanitor or ItemType.KeycardScientist or ItemType.KeycardResearchCoordinator or ItemType.KeycardZoneManager
	or ItemType.KeycardGuard or ItemType.KeycardMTFPrivate or ItemType.KeycardContainmentEngineer or ItemType.KeycardMTFOperative
	or ItemType.KeycardMTFCaptain or ItemType.KeycardFacilityManager or ItemType.KeycardChaosInsurgency or ItemType.KeycardO5
	or ItemType.KeycardCustomManagement or ItemType.KeycardCustomMetalCase or ItemType.KeycardCustomSite02 or ItemType.KeycardCustomTaskForce;
}
