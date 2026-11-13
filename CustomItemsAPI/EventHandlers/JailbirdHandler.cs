using CustomItemsAPI.Events;
using CustomItemsAPI.Items;
using InventorySystem.Items.Jailbird;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;

namespace CustomItemsAPI.EventHandlers;

/// <summary>
/// Represent a handle that process jailbird events.
/// </summary>
internal sealed class JailbirdHandler : CustomEventsHandler
{
	/// <inheritdoc/>
	public override void OnPlayerProcessingJailbirdMessage(PlayerProcessingJailbirdMessageEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.JailbirdItem, out CustomJailbirdBase? cur_item))
		{
			return;
		}

		TypeWrapper<bool> isAllowedHelper = new(ev.IsAllowed);
		TypeWrapper<JailbirdMessageType> message = new(ev.Message);
		TypeWrapper<bool> allowInspectHelper = new(ev.AllowInspect);
		TypeWrapper<bool> allowAttackHelper = new(ev.AllowAttack);
		CustomJailbirdEvents.OnProcessingJailbirdMessage(cur_item, ev.Player, ev.JailbirdItem, message, allowInspectHelper, allowAttackHelper, isAllowedHelper);
		cur_item.OnProcessingJailbirdMessage(ev.Player, ev.JailbirdItem, message, allowInspectHelper, allowAttackHelper, isAllowedHelper);
		ev.IsAllowed = isAllowedHelper.Value;
		ev.Message = message.Value;
		ev.AllowAttack = allowAttackHelper.Value;
		ev.AllowInspect = allowInspectHelper.Value;
	}

	/// <inheritdoc/>
	public override void OnPlayerProcessedJailbirdMessage(PlayerProcessedJailbirdMessageEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.JailbirdItem, out CustomJailbirdBase? cur_item))
		{
			return;
		}

		CustomJailbirdEvents.OnProcessedJailbirdMessage(cur_item, ev.Player, ev.JailbirdItem, ev.Message);
		cur_item.OnProcessedJailbirdMessage(ev.Player, ev.JailbirdItem, ev.Message);
	}
}
