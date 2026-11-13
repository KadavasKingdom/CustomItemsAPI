using CommandSystem;
using UnityEngine;

namespace CustomItemsAPI.Commands;

/// <summary>
/// Command for spawning custom item to the player.
/// </summary>
[CommandHandler(typeof(CustomItemsCommandBase))]
public sealed class SpawnCommand : ICommand, IUsageProvider
{
	/// <inheritdoc/>
	public string Command => "spawn";

	/// <inheritdoc/>
	public string[] Aliases => ["spawn"];

	/// <inheritdoc/>
	public string Description => "Spawn custom item";

	/// <inheritdoc/>
	public string[] Usage => ["itemname", "x", "y", "z"];

	/// <inheritdoc/>
	public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
	{
		if (!sender.CheckPermission(PlayerPermissions.GivingItems, out response))
		{
			return false;
		}

		if (arguments.Count < 2)
		{
			response = "To execute this command provide at least 2 arguments!\nUsage: " + arguments.Array[0] + " " + this.DisplayCommandUsage();
			return false;
		}

		string itemname = arguments.At(0);
		if (!CustomItems.IsItemNameExist(itemname, StringComparison.InvariantCultureIgnoreCase))
		{
			response = "ItemName not exists!";
			return false;
		}

		if (!float.TryParse(arguments.At(1), out float x))
		{
			response = "Spawn coordinate is wrong! (X)";
			return false;
		}

		if (!float.TryParse(arguments.At(2), out float y))
		{
			response = "Spawn coordinate is wrong! (Y)";
			return false;
		}

		if (!float.TryParse(arguments.At(3), out float z))
		{
			response = "Spawn coordinate is wrong! (Z)";
			return false;
		}

		var pickup = CustomItems.Spawn(itemname, new Vector3(x, y, z));
		if (pickup == null)
		{
			response = "Item creation failed!";
			return false;
		}

		response = $"Done!";
		return true;
	}
}
