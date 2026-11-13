using CommandSystem;
using Utils;

namespace CustomItemsAPI.Commands;

/// <summary>
/// Command for giving custom item to the player.
/// </summary>
[CommandHandler(typeof(CustomItemsCommandBase))]
public sealed class GiveCommand : ICommand, IUsageProvider
{
	/// <inheritdoc/>
	public string Command => "give";

	/// <inheritdoc/>
	public string[] Aliases => ["give"];

	/// <inheritdoc/>
	public string Description => "Give item to player(s)";

	/// <inheritdoc/>
	public string[] Usage => ["itemname", "%player% (Optional)"];

	/// <inheritdoc/>
	public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
	{
		if (!sender.CheckPermission(PlayerPermissions.GivingItems, out response))
		{
			return false;
		}

		if (arguments.Count < 1)
		{
			response = "To execute this command provide at least 1 arguments!\nUsage: " + this.DisplayCommandUsage();
			return false;
		}

		// Checking and getting all players to set.
		List<Player> players = [];
		Player? senderPlayer = Player.Get(sender);

		if (senderPlayer != null)
		{
			players.Add(senderPlayer);
		}

		if (arguments.Count == 2)
		{
			players = [.. RAUtils.ProcessPlayerIdOrNamesList(arguments, 1, out _).Select(Player.Get).Where(x => x is not null).Select(x => x!)];
		}

		if (players.Count == 0)
		{
			response = "No valid players found to be able to give custom items!";
			return false;
		}

		string itemname = arguments.At(0);
		if (!CustomItems.IsItemNameExist(itemname, StringComparison.InvariantCultureIgnoreCase))
		{
			response = "ItemName not exists!";
			return false;
		}

		foreach (Player p in players)
		{
			var customitem = CustomItems.CreateItem(itemname);
			if (customitem != null)
			{
				CustomItems.AddCustomItem(customitem, p);
			}
		}

		response = "Done!";
		return true;
	}
}
