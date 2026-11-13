using CustomItemsAPI.Events;
using LabApi.Events.CustomHandlers;
using MEC;

namespace CustomItemsAPI.EventHandlers;

/// <summary>
/// Represent a handle that do distribution.
/// </summary>
internal sealed class NonItemRelatedHandler : CustomEventsHandler
{
	/// <inheritdoc/>
	public override void OnServerWaitingForPlayers()
	{
		CustomItems.ClearSerials();
		Timing.CallDelayed(3, () =>
		{
			// Map should be generated at this point
			foreach (var item in CustomItems.CustomItemBaseList)
			{
				CustomItemEvents.OnDistribute(item);
				item.OnDistribute();
			}
		});
	}
}
