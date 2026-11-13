using CustomItemsAPI.Items;
using InventorySystem.Items.Firearms.Modules.Scp127;

namespace CustomItemsAPI.Events;

#pragma warning disable CA1003 // Use generic event handler instances

/// <summary>
/// Events for calling methods for <see cref="CustomScp127Base"/>.
/// </summary>
public static class CustomScp127Events
{
	/// <inheritdoc cref="LabApi.Events.Handlers.Scp127Events.GainExperience"/>
	public static event Action<CustomScp127Base, Scp127Firearm, float>? GainExperience;

	/// <inheritdoc cref="LabApi.Events.Handlers.Scp127Events.GainingExperience"/>
	public static event Action<CustomScp127Base, Scp127Firearm, TypeWrapper<float>, TypeWrapper<bool>>? GainingExperience;

	/// <inheritdoc cref="LabApi.Events.Handlers.Scp127Events.LevelUp"/>
	public static event Action<CustomScp127Base, Scp127Firearm, Scp127Tier>? LevelUp;

	/// <inheritdoc cref="LabApi.Events.Handlers.Scp127Events.LevellingUp"/>
	public static event Action<CustomScp127Base, Scp127Firearm, Scp127Tier, TypeWrapper<bool>>? LevellingUp;

	/// <inheritdoc cref="LabApi.Events.Handlers.Scp127Events.Talked"/>
	public static event Action<CustomScp127Base, Scp127Firearm, Scp127VoiceLinesTranslation, Scp127VoiceTriggerBase.VoiceLinePriority>? Talked;

	/// <inheritdoc cref="LabApi.Events.Handlers.Scp127Events.Talking"/>
	public static event Action<CustomScp127Base, Scp127Firearm, TypeWrapper<Scp127VoiceLinesTranslation>, TypeWrapper<Scp127VoiceTriggerBase.VoiceLinePriority>, TypeWrapper<bool>>? Talking;

	/// <inheritdoc cref="LabApi.Events.Handlers.Scp127Events.GainExperience"/>
	public static void OnGainExperience(CustomScp127Base custom, Scp127Firearm scp127Firearm, float experienceGain)
		=> GainExperience?.Invoke(custom, scp127Firearm, experienceGain);

	/// <inheritdoc cref="LabApi.Events.Handlers.Scp127Events.GainingExperience"/>
	public static void OnGainingExperience(CustomScp127Base custom, Scp127Firearm scp127Firearm, TypeWrapper<float> experienceGain, TypeWrapper<bool> isAllowed)
		=> GainingExperience?.Invoke(custom, scp127Firearm, experienceGain, isAllowed);

	/// <inheritdoc cref="LabApi.Events.Handlers.Scp127Events.LevelUp"/>
	public static void OnLevelUp(CustomScp127Base custom, Scp127Firearm scp127Firearm, Scp127Tier tier)
		=> LevelUp?.Invoke(custom, scp127Firearm, tier);

	/// <inheritdoc cref="LabApi.Events.Handlers.Scp127Events.LevellingUp"/>
	public static void OnLevellingUp(CustomScp127Base custom, Scp127Firearm scp127Firearm, Scp127Tier tier, TypeWrapper<bool> isAllowed)
		=> LevellingUp?.Invoke(custom, scp127Firearm, tier, isAllowed);

	/// <inheritdoc cref="LabApi.Events.Handlers.Scp127Events.Talked"/>
	public static void OnTalked(CustomScp127Base custom, Scp127Firearm scp127Firearm, Scp127VoiceLinesTranslation voiceLine, Scp127VoiceTriggerBase.VoiceLinePriority priority)
		=> Talked?.Invoke(custom, scp127Firearm, voiceLine, priority);

	/// <inheritdoc cref="LabApi.Events.Handlers.Scp127Events.Talking"/>
	public static void OnTalking(CustomScp127Base custom, Scp127Firearm scp127Firearm, TypeWrapper<Scp127VoiceLinesTranslation> voiceLine, TypeWrapper<Scp127VoiceTriggerBase.VoiceLinePriority> priority, TypeWrapper<bool> isAllowed)
		=> Talking?.Invoke(custom, scp127Firearm, voiceLine, priority, isAllowed);
}
