using CustomItemsAPI.Events;
using CustomItemsAPI.Items;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;

namespace CustomItemsAPI.EventHandlers;

/// <summary>
/// Represent a handle that process revoler event.
/// </summary>
internal sealed class RevolverHandler : CustomEventsHandler
{
	/// <inheritdoc/>
	public override void OnPlayerSpinningRevolver(PlayerSpinningRevolverEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.Revolver, out CustomRevolverBase? cur_item))
		{
			return;
		}

		TypeWrapper<bool> isAllowed = new(ev.IsAllowed);
		CustomRevolverEvents.OnSpinning(cur_item, ev.Player, ev.Revolver, isAllowed);
		cur_item.OnSpinning(ev.Player, ev.Revolver, isAllowed);
		ev.IsAllowed = isAllowed.Value;
	}

	/// <inheritdoc/>
	public override void OnPlayerSpinnedRevolver(PlayerSpinnedRevolverEventArgs ev)
	{
		if (!CustomItems.TryGetCustomItem(ev.Revolver, out CustomRevolverBase? cur_item))
		{
			return;
		}

		CustomRevolverEvents.OnSpinned(cur_item, ev.Player, ev.Revolver);
		cur_item.OnSpinned(ev.Player, ev.Revolver);
	}
}
