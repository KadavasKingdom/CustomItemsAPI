using CustomItemsAPI.Events;
using CustomItemsAPI.Items;
using InventorySystem.Items.Firearms.Modules.Scp127;
using LabApi.Events.Arguments.Scp127Events;
using LabApi.Events.CustomHandlers;

namespace CustomItemsAPI.EventHandlers;

/// <summary>
/// Represent a handle that process SCP-127 events.
/// </summary>
internal sealed class Scp127Handler : CustomEventsHandler
{
	/// <inheritdoc/>
	public override void OnScp127GainingExperience(Scp127GainingExperienceEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.Scp127Item, out CustomScp127Base? cur_item))
		{
			return;
		}

		TypeWrapper<bool> isAllowed = new(ev.IsAllowed);
		TypeWrapper<float> experienceGain = new(ev.ExperienceGain);
		CustomScp127Events.OnGainingExperience(cur_item, ev.Scp127Item, experienceGain, isAllowed);
		cur_item.OnGainingExperience(ev.Scp127Item, experienceGain, isAllowed);
		ev.IsAllowed = isAllowed.Value;
		ev.ExperienceGain = experienceGain.Value;
	}

	/// <inheritdoc/>
	public override void OnScp127GainExperience(Scp127GainExperienceEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.Scp127Item, out CustomScp127Base? cur_item))
		{
			return;
		}

		CustomScp127Events.OnGainExperience(cur_item, ev.Scp127Item, ev.ExperienceGain);
		cur_item.OnGainExperience(ev.Scp127Item, ev.ExperienceGain);
	}

	/// <inheritdoc/>
	public override void OnScp127LevellingUp(Scp127LevellingUpEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.Scp127Item, out CustomScp127Base? cur_item))
		{
			return;
		}

		TypeWrapper<bool> isAllowed = new(ev.IsAllowed);
		CustomScp127Events.OnLevellingUp(cur_item, ev.Scp127Item, ev.Tier, isAllowed);
		cur_item.OnLevellingUp(ev.Scp127Item, ev.Tier, isAllowed);
		ev.IsAllowed = isAllowed.Value;
	}

	/// <inheritdoc/>
	public override void OnScp127LevelUp(Scp127LevelUpEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.Scp127Item, out CustomScp127Base? cur_item))
		{
			return;
		}

		CustomScp127Events.OnLevelUp(cur_item, ev.Scp127Item, ev.Tier);
		cur_item.OnLevelUp(ev.Scp127Item, ev.Tier);
	}

	/// <inheritdoc/>
	public override void OnScp127Talking(Scp127TalkingEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.Scp127Item, out CustomScp127Base? cur_item))
		{
			return;
		}

		TypeWrapper<bool> isAllowed = new(ev.IsAllowed);
		TypeWrapper<Scp127VoiceLinesTranslation> voiceLine = new(ev.VoiceLine);
		TypeWrapper<Scp127VoiceTriggerBase.VoiceLinePriority> priority = new(ev.Priority);
		CustomScp127Events.OnTalking(cur_item, ev.Scp127Item, voiceLine, priority, isAllowed);
		cur_item.OnTalking(ev.Scp127Item, voiceLine, priority, isAllowed);
		ev.IsAllowed = isAllowed.Value;
		ev.VoiceLine = voiceLine.Value;
		ev.Priority = priority.Value;
	}

	/// <inheritdoc/>
	public override void OnScp127Talked(Scp127TalkedEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.Scp127Item, out CustomScp127Base? cur_item))
		{
			return;
		}

		CustomScp127Events.OnTalked(cur_item, ev.Scp127Item, ev.VoiceLine, ev.Priority);
		cur_item.OnTalked(ev.Scp127Item, ev.VoiceLine, ev.Priority);
	}
}
