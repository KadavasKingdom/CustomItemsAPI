using System.Diagnostics.CodeAnalysis;
using JailbirdItemBase = InventorySystem.Items.Jailbird.JailbirdItem;

namespace CustomItemsAPI;

/// <summary>
/// Override class for <see cref="JailbirdItemBase"/>.
/// </summary>
public class JailbirdItemOverride : BaseOverride<JailbirdItemBase>
{
	/// <summary>
	/// Gets or sets the changes for <see cref="JailbirdItemBase._meleeDelay"/>.
	/// </summary>
	public MathValueFloat MeleeDelay { get; set; } = new();

	/// <summary>
	/// Gets or sets the changes for <see cref="JailbirdItemBase._meleeCooldown"/>.
	/// </summary>
	public MathValueFloat MeleeCooldown { get; set; } = new();

	/// <summary>
	/// Gets or sets the changes for <see cref="JailbirdItemBase._chargeDuration"/>.
	/// </summary>
	public MathValueFloat ChargeDuration { get; set; } = new();

	/// <summary>
	/// Gets or sets the changes for <see cref="JailbirdItemBase._chargeReadyTime"/>.
	/// </summary>
	public MathValueFloat ChargeReadyTime { get; set; } = new();

	/// <summary>
	/// Gets or sets the changes for <see cref="JailbirdItemBase._chargeMovementSpeedMultiplier"/>.
	/// </summary>
	public MathValueFloat ChargeMovementSpeedMultiplier { get; set; } = new();

	/// <summary>
	/// Gets or sets the changes for <see cref="JailbirdItemBase._chargeMovementSpeedLimiter"/>.
	/// </summary>
	public MathValueFloat ChargeMovementSpeedLimiter { get; set; } = new();

	/// <summary>
	/// Gets or sets the changes for <see cref="JailbirdItemBase._chargeCancelVelocitySqr"/>.
	/// </summary>
	public MathValueFloat ChargeCancelVelocitySqr { get; set; } = new();

	/// <summary>
	/// Gets or sets the changes for <see cref="JailbirdItemBase._chargeAutoengageTime"/>.
	/// </summary>
	public MathValueFloat ChargeAutoengageTime { get; set; } = new();

	/// <summary>
	/// Gets or sets the changes for <see cref="JailbirdItemBase._chargeDetectionDelay"/>.
	/// </summary>
	public MathValueFloat ChargeDetectionDelay { get; set; } = new();

	/// <summary>
	/// Gets or sets the changes for <see cref="JailbirdItemBase._brokenRemoveTime"/>.
	/// </summary>
	public MathValueFloat BrokenRemoveTime { get; set; } = new();

	/// <summary>
	/// Gets or sets the how much damage should the melee attack deal.
	/// </summary>
	public MathValueFloat DamageMelee { get; set; } = new();

	/// <summary>
	/// Gets or sets the how much damage should the charge attack deal.
	/// </summary>
	public MathValueFloat DamageCharge { get; set; } = new();

	/// <summary>
	/// Gets or sets the how long in seconds the 'concussed' effect is applied for on attacked targets.
	/// </summary>
	public MathValueFloat ConcussionDuration { get; set; } = new();

	/// <summary>
	/// Gets or sets the how long in seconds the 'flashed' effect is applied for on attacked targets.
	/// </summary>
	public MathValueFloat FlashedDuration { get; set; } = new();

	/// <inheritdoc/>
	public override void Apply([NotNull] ref JailbirdItemBase jailbirdItem)
	{
		DamageMelee.MathCalculation(ref jailbirdItem.MeleeDamage);
		DamageCharge.MathCalculation(ref jailbirdItem._chargeDamage);
		ConcussionDuration.MathCalculation(ref jailbirdItem._concussionDuration);
		FlashedDuration.MathCalculation(ref jailbirdItem._flashedDuration);
		MeleeDelay.MathCalculation(ref jailbirdItem._meleeDelay);
		MeleeCooldown.MathCalculation(ref jailbirdItem._meleeCooldown);
		ChargeDuration.MathCalculation(ref jailbirdItem._chargeDuration);
		ChargeReadyTime.MathCalculation(ref jailbirdItem._chargeReadyTime);
		ChargeMovementSpeedMultiplier.MathCalculation(ref jailbirdItem._chargeMovementSpeedMultiplier);
		ChargeMovementSpeedLimiter.MathCalculation(ref jailbirdItem._chargeMovementSpeedLimiter);
		ChargeCancelVelocitySqr.MathCalculation(ref jailbirdItem._chargeCancelVelocitySqr);
		ChargeAutoengageTime.MathCalculation(ref jailbirdItem._chargeAutoengageTime);
		ChargeDetectionDelay.MathCalculation(ref jailbirdItem._chargeDetectionDelay);
		BrokenRemoveTime.MathCalculation(ref jailbirdItem._brokenRemoveTime);
	}
}
