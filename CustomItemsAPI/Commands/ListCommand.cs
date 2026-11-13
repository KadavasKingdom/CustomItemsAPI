using CommandSystem;

namespace CustomItemsAPI.Commands;

/// <summary>
/// Command for listing custom items.
/// </summary>
[CommandHandler(typeof(CustomItemsCommandBase))]
public sealed class ListCommand : ICommand
{
	/// <inheritdoc/>
	public string Command => "list";

	/// <inheritdoc/>
	public string[] Aliases => ["ls"];

	/// <inheritdoc/>
	public string Description => "List all available custom items";

	/// <inheritdoc/>
	public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
	{
		response = "\n--- Available Custom Items: ---\n";
		foreach (var item in CustomItems.CustomItemBaseList)
		{
			response += $" {item.CustomItemName}\t{item.Description}\n";
		}

		response += "--- Currently Existing items and its id: ---\n";
		foreach (var item in CustomItems.SerialToCustomItem)
		{
			response += $" {item.Key} = {(Pickup.SerialCache.ContainsKey(item.Key) ? "P" : "I")} {item.Value.CustomItemName}\n";
		}

		return true;
	}
}
