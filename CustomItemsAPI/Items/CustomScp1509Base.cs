using PlayerRoles;

namespace CustomItemsAPI.Items;

/// <summary>
/// Custom <see cref="Scp1509Item"/> base.
/// </summary>
public abstract class CustomScp1509Base : CustomItemBase
{
	/// <summary>
	/// Gets the rate at which shield regenerates (points per second) when SCP-1509 is held..
	/// </summary>
	public virtual MathValueFloat ShieldRegenRate { get; } = new();

	/// <summary>
	/// Gets the rate at which shield is removed (points per second) when SCP-1509 is no longer held.
	/// </summary>
	public virtual MathValueFloat ShieldDecayRate { get; } = new();

	/// <summary>
	/// Gets the number of seconds that have to elapse since the last time damage was taken.
	/// </summary>
	public virtual MathValueFloat ShieldOnDamagePause { get; } = new();

	/// <summary>
	/// Gets the number of seconds that have to elapse since the item was last equipped, for the shield to begin decaying.
	/// </summary>
	public virtual MathValueFloat UnequipDecayDelay { get; } = new();

	/// <summary>
	/// Gets the the revive cooldown after resurrection.
	/// </summary>
	public virtual MathValueFloat ReviveCooldown { get; } = new();

	/// <summary>
	/// Gets the <see cref="Item.CurrentOwner"/>'s Max HS value after resurrection.
	/// </summary>
	public virtual MathValueFloat EquippedHS { get; } = new();

	/// <summary>
	/// Gets the duration of the Blurred effect applied to revived players.
	/// </summary>
	public virtual MathValueFloat RevivedPlayerBlurTime { get; } = new();

	/// <summary>
	/// Gets the bonus AHP value for <see cref="Scp1509Item.RevivedPlayers"/> within <see cref="Scp1509Item.RevivedPlayerAOEBonusAHPDistance"/>..
	/// </summary>
	public virtual MathValueFloat RevivedPlayerAOEBonusAHP { get; } = new();

	/// <summary>
	/// Gets the minimum distance of giving AHP bonus to <see cref="Scp1509Item.RevivedPlayers"/>..
	/// </summary>
	public virtual MathValueFloat RevivedPlayerAOEBonusAHPDistance { get; } = new();

	/// <summary>
	/// Gets the melee damage value for SCP-1509.
	/// </summary>
	public virtual MathValueFloat MeleeDamage { get; } = new();

	/// <summary>
	/// Gets the melee delay value for SCP-1509.
	/// </summary>
	public virtual MathValueFloat MeleeDelay { get; } = new();

	/// <summary>
	/// Gets the melee cooldown value for SCP-1509.
	/// </summary>
	public virtual MathValueFloat MeleeCooldown { get; } = new();

	/// <inheritdoc/>
	public override void Parse(Item item)
	{
		base.Parse(item);
		if (item is not Scp1509Item scp1509)
		{
			throw new ArgumentException("Item must be Scp1509Item!");
		}

		scp1509.ShieldRegenRate = ShieldRegenRate.MathCalculation(scp1509.ShieldRegenRate);
		scp1509.ShieldDecayRate = ShieldDecayRate.MathCalculation(scp1509.ShieldDecayRate);
		scp1509.ShieldOnDamagePause = ShieldOnDamagePause.MathCalculation(scp1509.ShieldOnDamagePause);
		scp1509.UnequipDecayDelay = UnequipDecayDelay.MathCalculation(scp1509.UnequipDecayDelay);
		scp1509.ReviveCooldown = ReviveCooldown.MathCalculation((float)scp1509.ReviveCooldown);
		scp1509.EquippedHS = EquippedHS.MathCalculation(scp1509.EquippedHS);
		scp1509.RevivedPlayerBlurTime = RevivedPlayerBlurTime.MathCalculation(scp1509.RevivedPlayerBlurTime);
		scp1509.RevivedPlayerAOEBonusAHP = RevivedPlayerAOEBonusAHP.MathCalculation(scp1509.RevivedPlayerAOEBonusAHP);
		scp1509.RevivedPlayerAOEBonusAHPDistance = RevivedPlayerAOEBonusAHPDistance.MathCalculation(scp1509.RevivedPlayerAOEBonusAHPDistance);
		scp1509.Base.MeleeDamage = MeleeDamage.MathCalculation(scp1509.Base.MeleeDamage);
		scp1509.Base._meleeDelay = MeleeDelay.MathCalculation(scp1509.Base._meleeDelay);
		scp1509.Base._meleeCooldown = MeleeCooldown.MathCalculation(scp1509.Base._meleeCooldown);
	}

	/// <inheritdoc cref="Events.CustomScp1509Events.OnProcessedScp1509Message(CustomScp1509Base, Player, Scp1509Item, InventorySystem.Items.Scp1509.Scp1509MessageType)"/>
	public virtual void OnProcessedScp1509Message(Player player, Scp1509Item scp1509Item, InventorySystem.Items.Scp1509.Scp1509MessageType message)
	{
	}

	/// <inheritdoc cref="Events.CustomScp1509Events.OnProcessingScp1509Message(CustomScp1509Base, Player, Scp1509Item, TypeWrapper{InventorySystem.Items.Scp1509.Scp1509MessageType}, TypeWrapper{bool}, TypeWrapper{bool}, TypeWrapper{bool})"/>
	public virtual void OnProcessingScp1509Message(Player player, Scp1509Item scp1509Item, TypeWrapper<InventorySystem.Items.Scp1509.Scp1509MessageType> message, TypeWrapper<bool> allowInspectHelper, TypeWrapper<bool> allowAttackHelper, TypeWrapper<bool> isAllowedHelper)
	{
	}

	/// <inheritdoc cref="Events.CustomScp1509Events.OnResurrected(CustomScp1509Base, Player, Scp1509Item, Player, Player, RoleTypeId)"/>
	public virtual void OnResurrected(Player player, Scp1509Item item, Player killedPlayer, Player revivedPlayer, RoleTypeId respawnRole)
	{
	}

	/// <inheritdoc cref="Events.CustomScp1509Events.OnResurrecting(CustomScp1509Base, Player, Scp1509Item, Player, TypeWrapper{Player}, TypeWrapper{RoleTypeId}, TypeWrapper{bool})"/>
	public virtual void OnResurrecting(Player player, Scp1509Item item, Player killedPlayer, TypeWrapper<Player?> revivedPlayerHelper, TypeWrapper<RoleTypeId> respawnRoleHelper, TypeWrapper<bool> isAllowedHelper)
	{
	}
}
