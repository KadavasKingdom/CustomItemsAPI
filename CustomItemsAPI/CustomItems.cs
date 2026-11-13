using CustomItemsAPI.Events;
using CustomItemsAPI.Items;
using InventorySystem;
using InventorySystem.Items;
using InventorySystem.Items.Pickups;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using UnityEngine;

namespace CustomItemsAPI;

/// <summary>
/// Static class for handling Custom Items, such as spawning, giving to player, checking and getting the item.
/// </summary>
public static class CustomItems
{
	/// <summary>
	/// Maps item serial numbers to their corresponding custom item instances.
	/// </summary>
	internal static readonly Dictionary<ushort, CustomItemBase> SerialToCustomItem = [];

	/// <summary>
	/// A set of registered custom items.
	/// </summary>
	internal static readonly HashSet<CustomItemBase> CustomItemBaseList = [];

	/// <summary>
	/// Creating a new item with as <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">Any <see cref="CustomItemBase"/>.</typeparam>
	/// <returns><see langword="null"/> if <typeparamref name="T"/> not found inside <see cref="CustomItemBaseList"/> or created <typeparamref name="T"/> item. </returns>
	public static T? CreateItem<T>()
		where T : CustomItemBase
	{
		var cib = CustomItemBaseList.FirstOrDefault(x => x.GetType() == typeof(T));
		if (cib == null)
		{
			return null;
		}

		var newItem = (T)cib;
		CustomItemEvents.OnNewCreated(newItem);
		newItem.OnNewCreated();
		return newItem;
	}

	/// <summary>
	/// Creating a new <see cref="CustomItemBase"/> from <paramref name="itemName"/>.
	/// </summary>
	/// <param name="itemName">A registered <see cref="CustomItemBase.CustomItemName"/>.</param>
	/// <returns><see langword="null"/> if <paramref name="itemName"/> not found inside <see cref="CustomItemBaseList"/>.</returns>
	public static CustomItemBase? CreateItem(string itemName)
	{
		var cib = CustomItemBaseList.FirstOrDefault(x => x.CustomItemName == itemName);
		if (cib == null)
		{
			return null;
		}

		CustomItemEvents.OnNewCreated(cib);
		cib.OnNewCreated();
		return cib;
	}

	/// <summary>
	/// Create and add Custom Item as <typeparamref name="T"/> to the <paramref name="player"/> Inventory.
	/// </summary>
	/// <typeparam name="T">Any <see cref="CustomItemBase"/>.</typeparam>
	/// <param name="player">The player to add item to.</param>
	/// <returns>The new item.</returns>
	public static Item? AddCustomItem<T>(Player player)
		where T : CustomItemBase
	{
		T? item = CreateItem<T>();
		return AddCustomItem(item, player);
	}

	/// <summary>
	/// Add <paramref name="customItemName"/> to the <paramref name="player"/> Inventory.
	/// </summary>
	/// <param name="customItemName">The Custom Item name.</param>
	/// <param name="player">The <see cref="Player"/> to add item to.</param>
	/// <returns>The new item.</returns>
	public static Item? AddCustomItem(string customItemName, Player player)
	{
		CustomItemBase? customItemBase = CreateItem(customItemName);
		return AddCustomItem(customItemBase, player);
	}

	/// <summary>
	/// Add <paramref name="item"/> to the <paramref name="player"/> Inventory.
	/// </summary>
	/// <param name="item">The Custom Item.</param>
	/// <param name="player">The <see cref="Player"/> to add item to.</param>
	/// <returns>The new item.</returns>
	public static Item? AddCustomItem(this CustomItemBase? item, Player player)
	{
		if (item is null || player is null)
		{
			return null;
		}

		Item? itemBase = player.AddItem(item.Type);
		if (itemBase is null)
		{
			return null;
		}

		CustomItemEvents.OnParseItem(item, itemBase);
		item.Parse(itemBase);
		player.ReferenceHub.inventory.UserInventory.Items[itemBase.Serial] = itemBase.Base;
		SerialToCustomItem.Add(itemBase.Serial, item);
		return itemBase;
	}

	/// <summary>
	/// Add <paramref name="item"/> to the <paramref name="hub"/> Inventory.
	/// </summary>
	/// <param name="item">The Custom Item.</param>
	/// <param name="hub">The <see cref="ReferenceHub"/> to add item to.</param>
	/// <returns>The new item.</returns>
	public static Item? AddCustomItem(this CustomItemBase? item, ReferenceHub hub)
	{
		if (item is null || hub == null)
		{
			return null;
		}

		Item? itemBase = Item.Get(hub.inventory.ServerAddItem(item.Type, ItemAddReason.AdminCommand));
		if (itemBase is null)
		{
			return null;
		}

		CustomItemEvents.OnParseItem(item, itemBase);
		item.Parse(itemBase);
		hub.inventory.UserInventory.Items[itemBase.Serial] = itemBase.Base;
		SerialToCustomItem.Add(itemBase.Serial, item);
		return itemBase;
	}

	/// <summary>
	/// Spawns a <see cref="CustomItemBase"/> to specified parameters.
	/// </summary>
	/// <param name="customItemName">The name of the custom item.</param>
	/// <param name="position">The position the pickup should spawn.</param>
	/// <param name="rotation">The rotation the pickup should spawn.</param>
	/// <param name="scale">The scale the pickup should spawn.</param>
	/// <param name="shouldSpawn">Whether the item should spawn.</param>
	/// <returns>Returns <see langword="null"/> if pickup cannot be created otherwise it is a <see cref="Pickup"/>.</returns>
	public static Pickup? Spawn(string customItemName, Vector3 position, Quaternion? rotation = null, Vector3? scale = null, bool shouldSpawn = true)
	{
		CustomItemBase? item = CreateItem(customItemName);
		return Spawn(item, position, rotation, scale, shouldSpawn);
	}

	/// <summary>
	/// Spawns a Custom Item as <typeparamref name="T"/> to specified parameters.
	/// </summary>
	/// <typeparam name="T">Any <see cref="CustomItemBase"/>.</typeparam>
	/// <param name="position">The position the pickup should spawn.</param>
	/// <param name="rotation">The rotation the pickup should spawn.</param>
	/// <param name="scale">The scale the pickup should spawn.</param>
	/// <param name="shouldSpawn">Whether the item should spawn.</param>
	/// <returns>Returns <see langword="null"/> if pickup cannot be created otherwise it is a <see cref="Pickup"/>.</returns>
	public static Pickup? Spawn<T>(Vector3 position, Quaternion? rotation = null, Vector3? scale = null, bool shouldSpawn = true)
		where T : CustomItemBase
	{
		T? item = CreateItem<T>();
		return Spawn(item, position, rotation, scale, shouldSpawn);
	}

	/// <summary>
	/// Spawns a <paramref name="customItem"/> to specified parameters.
	/// </summary>
	/// <param name="customItem">The custom item.</param>
	/// <param name="position">The position the pickup should spawn.</param>
	/// <param name="rotation">The rotation the pickup should spawn.</param>
	/// <param name="scale">The scale the pickup should spawn.</param>
	/// <param name="shouldSpawn">Whether the item should spawn.</param>
	/// <returns>Returns <see langword="null"/> if pickup cannot be created or <paramref name="customItem"/> is <see langword="null"/> otherwise it is a <see cref="Pickup"/>.</returns>
	public static Pickup? Spawn(this CustomItemBase? customItem, Vector3 position, Quaternion? rotation = null, Vector3? scale = null, bool shouldSpawn = true)
	{
		if (customItem == null)
		{
			return null;
		}

		if (rotation == null)
		{
			rotation = Quaternion.identity;
		}

		if (scale == null)
		{
			scale = Vector3.one;
		}

		Pickup? pickup = Pickup.Create(customItem.Type, position, rotation.Value, scale.Value);
		if (pickup is null)
		{
			return null;
		}

		if (shouldSpawn)
		{
			pickup.Spawn();
		}

		CustomItemEvents.OnParsePickup(customItem, pickup);
		customItem.Parse(pickup);
		SerialToCustomItem.Add(pickup.Serial, customItem);
		return pickup;
	}

	/// <summary>
	/// Checks if the <see cref="CustomItemBaseList"/> has any <see cref="CustomItemBase.CustomItemName"/> as <paramref name="itemName"/>.
	/// </summary>
	/// <param name="itemName">The Custom Item name.</param>
	/// <param name="comparison">String Comparison for easier check.</param>
	/// <returns><see langword="true"/> if found otherwise <see langword="false"/>.</returns>
	public static bool IsItemNameExist(string? itemName, StringComparison comparison = StringComparison.InvariantCulture)
	{
		if (string.IsNullOrEmpty(itemName))
		{
			return false;
		}

		return CustomItemBaseList.Any(x => x.CustomItemName.Equals(itemName, comparison));
	}

	/// <summary>
	/// Checks if the <paramref name="serial"/> contains inside the <see cref="SerialToCustomItem"/>.
	/// </summary>
	/// <param name="serial">The Item Serial Id.</param>
	/// <returns><see langword="true"/> if it is custom item, otherwise <see langword="false"/>.</returns>
	public static bool IsCustom(this ushort serial) => SerialToCustomItem.ContainsKey(serial);

	/// <summary>
	/// Checks if the <see cref="Item.Serial"/> contains inside the <see cref="SerialToCustomItem"/>.
	/// </summary>
	/// <param name="item">The <see cref="Item"/> to check its Serial.</param>
	/// <returns><see langword="true"/> if it is custom item, otherwise <see langword="false"/>.</returns>
	public static bool IsCustom(this Item? item) => item != null && SerialToCustomItem.ContainsKey(item.Serial);

	/// <summary>
	/// Checks if the <see cref="Item.Serial"/> contains inside the <see cref="SerialToCustomItem"/>.
	/// </summary>
	/// <param name="item">The <see cref="Item"/> to check its Serial.</param>
	/// <returns><see langword="true"/> if it is custom item, otherwise <see langword="false"/>.</returns>
	public static bool IsCustom(this ItemBase? item) => item != null && SerialToCustomItem.ContainsKey(item.ItemSerial);

	/// <summary>
	/// Checks if the <see cref="Pickup.Serial"/> contains inside the <see cref="SerialToCustomItem"/>.
	/// </summary>
	/// <param name="item">The <see cref="Pickup"/> to check its Serial.</param>
	/// <returns><see langword="true"/> if it is custom item, otherwise <see langword="false"/>.</returns>
	public static bool IsCustom(this Pickup? item) => item != null && SerialToCustomItem.ContainsKey(item.Serial);

	/// <summary>
	/// Checks if the <see cref="Pickup.Serial"/> contains inside the <see cref="SerialToCustomItem"/>.
	/// </summary>
	/// <param name="item">The <see cref="Pickup"/> to check its Serial.</param>
	/// <returns><see langword="true"/> if it is custom item, otherwise <see langword="false"/>.</returns>
	public static bool IsCustom(this ItemPickupBase? item) => item != null && SerialToCustomItem.ContainsKey(item.Info.Serial);

	/// <summary>
	/// Checks if <paramref name="player"/> holding a custom item.
	/// </summary>
	/// <param name="player">The player to check.</param>
	/// <returns><see langword="true"/> if it is custom item, otherwise <see langword="false"/>.</returns>
	public static bool IsHoldingCustomItem(this Player? player)
	{
		if (player == null)
		{
			return false;
		}

		return IsCustom(player.CurrentItem);
	}

	/// <summary>
	/// Get the custom item as <typeparamref name="T"/> from <paramref name="item"/>.
	/// </summary>
	/// <typeparam name="T">Any <see cref="CustomItemBase"/>.</typeparam>
	/// <param name="item">The <see cref="Item"/> to get from.</param>
	/// <returns><see langword="null"/> if could not get the item otherwise <typeparamref name="T"/>.</returns>
	public static T? GetCustomItem<T>(this Item? item)
		where T : CustomItemBase
	{
		if (item == null)
		{
			return null;
		}

		return GetCustomItem<T>(item.Serial);
	}

	/// <summary>
	/// Get the custom item as <typeparamref name="T"/> from <paramref name="item"/>.
	/// </summary>
	/// <typeparam name="T">Any <see cref="CustomItemBase"/>.</typeparam>
	/// <param name="item">The <see cref="Pickup"/> to get from.</param>
	/// <returns><see langword="null"/> if could not get the item otherwise <typeparamref name="T"/>.</returns>
	public static T? GetCustomItem<T>(this Pickup? item)
		where T : CustomItemBase
	{
		if (item == null)
		{
			return null;
		}

		return GetCustomItem<T>(item.Serial);
	}

	/// <summary>
	/// Get the custom item as <typeparamref name="T"/> from <paramref name="player"/> from it's <see cref="Player.CurrentItem"/>.
	/// </summary>
	/// <typeparam name="T">Any <see cref="CustomItemBase"/>.</typeparam>
	/// <param name="player">The <see cref="Pickup"/> to get from.</param>
	/// <returns><see langword="null"/> if could not get the item otherwise <typeparamref name="T"/>.</returns>
	public static T? GetCustomItem<T>(this Player player)
		where T : CustomItemBase
	{
		if (player is null || player.IsHost)
		{
			return null;
		}

		return GetCustomItem<T>(player.CurrentItem);
	}

	/// <summary>
	/// Get the custom item as <typeparamref name="T"/> from <paramref name="serial"/>.
	/// </summary>
	/// <typeparam name="T">Any <see cref="CustomItemBase"/>.</typeparam>
	/// <param name="serial">The serial id of the item/pickup.</param>
	/// <returns><see langword="null"/> if could not get the item otherwise <typeparamref name="T"/>.</returns>
	public static T? GetCustomItem<T>(this ushort serial)
		where T : CustomItemBase
	{
		if (serial == 0)
		{
			return null;
		}

		if (SerialToCustomItem.TryGetValue(serial, out var customItem) && customItem is T customItemType)
		{
			return customItemType;
		}

		return null;
	}

	/// <summary>
	/// Get the custom item as <see cref="CustomItemBase"/> from <paramref name="item"/>.
	/// </summary>
	/// <param name="item">The <see cref="Item"/> to get from.</param>
	/// <returns><see langword="null"/> if could not get the item otherwise <see cref="CustomItemBase"/>.</returns>
	public static CustomItemBase? GetCustomItem(this Item? item)
	{
		if (item is null)
		{
			return null;
		}

		return GetCustomItem(item.Serial);
	}

	/// <summary>
	/// Get the custom item as <see cref="CustomItemBase"/> from <paramref name="item"/>.
	/// </summary>
	/// <param name="item">The <see cref="ItemBase"/> to get from.</param>
	/// <returns><see langword="null"/> if could not get the item otherwise <see cref="CustomItemBase"/>.</returns>
	public static CustomItemBase? GetCustomItem(this ItemBase? item)
	{
		if (item == null)
		{
			return null;
		}

		return GetCustomItem(item.ItemSerial);
	}

	/// <summary>
	/// Get the custom item as <see cref="CustomItemBase"/> from <paramref name="item"/>.
	/// </summary>
	/// <param name="item">The <see cref="Pickup"/> to get from.</param>
	/// <returns><see langword="null"/> if could not get the item otherwise <see cref="CustomItemBase"/>.</returns>
	public static CustomItemBase? GetCustomItem(this Pickup? item)
	{
		if (item is null)
		{
			return null;
		}

		return GetCustomItem(item.Serial);
	}

	/// <summary>
	/// Get the custom item as <see cref="CustomItemBase"/> from <paramref name="item"/>.
	/// </summary>
	/// <param name="item">The <see cref="ItemPickupBase"/> to get from.</param>
	/// <returns><see langword="null"/> if could not get the item otherwise <see cref="CustomItemBase"/>.</returns>
	public static CustomItemBase? GetCustomItem(this ItemPickupBase? item)
	{
		if (item == null)
		{
			return null;
		}

		return GetCustomItem(item.Info.Serial);
	}

	/// <summary>
	/// Get the custom item as <see cref="CustomItemBase"/> from <paramref name="player"/> from it's <see cref="Player.CurrentItem"/>.
	/// </summary>
	/// <param name="player">The <see cref="Pickup"/> to get from.</param>
	/// <returns><see langword="null"/> if could not get the item otherwise <see cref="CustomItemBase"/>.</returns>
	public static CustomItemBase? GetCustomItem(this Player player)
	{
		if (player is null || player.IsHost)
		{
			return null;
		}

		return GetCustomItem(player.CurrentItem);
	}

	/// <summary>
	/// Get the custom item as <see cref="CustomItemBase"/> from <paramref name="serial"/>.
	/// </summary>
	/// <param name="serial">The serial id of the item/pickup.</param>
	/// <returns><see langword="null"/> if could not get the item otherwise <see cref="CustomItemBase"/>.</returns>
	public static CustomItemBase? GetCustomItem(this ushort serial)
	{
		if (serial == 0)
		{
			return null;
		}

		if (SerialToCustomItem.TryGetValue(serial, out var customItem))
		{
			return customItem;
		}

		return null;
	}

	/// <summary>
	/// Gets the <paramref name="customItem"/> associated with the <paramref name="player"/>.
	/// </summary>
	/// <param name="player">The player.</param>
	/// <param name="customItem">The custom item.</param>
	/// <returns><see langword="true"/> if success; otherwise <see langword="false"/>.</returns>
	public static bool TryGetCustomItem(this Player player, [NotNullWhen(true)] out CustomItemBase? customItem)
	{
		if (player is null)
		{
			customItem = null;
			return false;
		}

		return TryGetCustomItem(player.CurrentItem, out customItem);
	}

	/// <summary>
	/// Gets the <paramref name="customItem"/> associated with the <paramref name="item"/>.
	/// </summary>
	/// <param name="item">The item.</param>
	/// <param name="customItem">The custom item.</param>
	/// <returns><see langword="true"/> if success; otherwise <see langword="false"/>.</returns>
	public static bool TryGetCustomItem(this Item? item, [NotNullWhen(true)] out CustomItemBase? customItem)
	{
		customItem = null;
		if (item == null)
		{
			return false;
		}

		return TryGetCustomItem(item.Serial, out customItem);
	}

	/// <summary>
	/// Gets the <paramref name="customItem"/> associated with the <paramref name="pickup"/>.
	/// </summary>
	/// <param name="pickup">The pickup.</param>
	/// <param name="customItem">The custom item.</param>
	/// <returns><see langword="true"/> if success; otherwise <see langword="false"/>.</returns>
	public static bool TryGetCustomItem(this Pickup? pickup, [NotNullWhen(true)] out CustomItemBase? customItem)
	{
		customItem = null;
		if (pickup == null)
		{
			return false;
		}

		return TryGetCustomItem(pickup.Serial, out customItem);
	}

	/// <summary>
	/// Gets the <paramref name="customItem"/> associated with the <paramref name="serial"/>.
	/// </summary>
	/// <param name="serial">The item serial.</param>
	/// <param name="customItem">The custom item.</param>
	/// <returns><see langword="true"/> if success; otherwise <see langword="false"/>.</returns>
	public static bool TryGetCustomItem(this ushort serial, [NotNullWhen(true)] out CustomItemBase? customItem)
	{
		return SerialToCustomItem.TryGetValue(serial, out customItem);
	}

	/// <summary>
	/// Gets the <paramref name="customItem"/> associated with the <paramref name="player"/>.
	/// </summary>
	/// <typeparam name="T">Any <see cref="CustomItemBase"/> type.</typeparam>
	/// <param name="player">The player.</param>
	/// <param name="customItem">The custom item.</param>
	/// <returns><see langword="true"/> if success; otherwise <see langword="false"/>.</returns>
	public static bool TryGetCustomItem<T>(this Player player, [NotNullWhen(true)] out T? customItem)
		where T : CustomItemBase
	{
		if (player is null)
		{
			customItem = null;
			return false;
		}

		return TryGetCustomItem<T>(player.CurrentItem, out customItem);
	}

	/// <summary>
	/// Gets the <paramref name="customItem"/> associated with the <paramref name="item"/>.
	/// </summary>
	/// <typeparam name="T">Any <see cref="CustomItemBase"/> type.</typeparam>
	/// <param name="item">The item.</param>
	/// <param name="customItem">The custom item.</param>
	/// <returns><see langword="true"/> if success; otherwise <see langword="false"/>.</returns>
	public static bool TryGetCustomItem<T>(this Item? item, [NotNullWhen(true)] out T? customItem)
		where T : CustomItemBase
	{
		customItem = null;
		if (item == null)
		{
			return false;
		}

		return TryGetCustomItem(item.Serial, out customItem);
	}

	/// <summary>
	/// Gets the <paramref name="customItem"/> associated with the <paramref name="pickup"/>.
	/// </summary>
	/// <typeparam name="T">Any <see cref="CustomItemBase"/> type.</typeparam>
	/// <param name="pickup">The pickup.</param>
	/// <param name="customItem">The custom item.</param>
	/// <returns><see langword="true"/> if success; otherwise <see langword="false"/>.</returns>
	public static bool TryGetCustomItem<T>(this Pickup? pickup, [NotNullWhen(true)] out T? customItem)
		where T : CustomItemBase
	{
		customItem = null;
		if (pickup == null)
		{
			return false;
		}

		return TryGetCustomItem(pickup.Serial, out customItem);
	}

	/// <summary>
	/// Gets the <paramref name="customItem"/> associated with the <paramref name="serial"/>.
	/// </summary>
	/// <typeparam name="T">Any <see cref="CustomItemBase"/> type.</typeparam>
	/// <param name="serial">The item serial.</param>
	/// <param name="customItem">The custom item.</param>
	/// <returns><see langword="true"/> if success; otherwise <see langword="false"/>.</returns>
	public static bool TryGetCustomItem<T>(this ushort serial, [NotNullWhen(true)] out T? customItem)
		where T : CustomItemBase
	{
		if (SerialToCustomItem.TryGetValue(serial, out var customItemBase) && customItemBase is T customItemT)
		{
			customItem = customItemT;
			return true;
		}

		customItem = null;
		return false;
	}

	/// <summary>
	/// Register custom items with the Calling Assembly.
	/// </summary>
	public static void RegisterCustomItems()
	{
		Assembly assembly = Assembly.GetCallingAssembly();
		if (assembly == typeof(CustomItems).Assembly)
		{
			return;
		}

		List<Type> types = [.. assembly.GetTypes().
			Where(item =>
				!item.IsAbstract &&
				typeof(CustomItemBase).IsAssignableFrom(item))];
		foreach (Type type in types)
		{
			RegisterCustomItem(type);
		}
	}

	/// <summary>
	/// Register custom items with the Calling Assembly except <paramref name="exceptType"/>'s.
	/// </summary>
	/// <param name="exceptType">Parameter array of the <see cref="Type"/> of a <see cref="CustomItemBase"/>.</param>
	public static void RegisterCustomItemsExcept(params Type[] exceptType)
	{
		Assembly assembly = Assembly.GetCallingAssembly();
		if (assembly == typeof(CustomItems).Assembly)
		{
			return;
		}

		List<Type> types = [.. assembly.GetTypes().
			Where(item =>
				!item.IsAbstract &&
				typeof(CustomItemBase).IsAssignableFrom(item))];
		foreach (Type type in types)
		{
			if (exceptType.Contains(type))
			{
				continue;
			}

			RegisterCustomItem(type);
		}
	}

	/// <summary>
	/// Register only <paramref name="types"/> as custom items.
	/// </summary>
	/// <param name="types">Parameter array of the <see cref="Type"/> of a <see cref="CustomItemBase"/>.</param>
	public static void RegisterCustomItems(params Type[] types)
	{
		if (types == null)
		{
			return;
		}

		foreach (Type type in types)
		{
			RegisterCustomItem(type);
		}
	}

	/// <summary>
	/// Register individual <paramref name="type"/> as a custom item.
	/// </summary>
	/// <param name="type">The <see cref="Type"/> of a <see cref="CustomItemBase"/>.</param>
	public static void RegisterCustomItem(Type type)
	{
		if (type == null)
		{
			return;
		}

		if (type.Equals(null))
		{
			return;
		}

		CL.Debug(type, Main.Instance.Config.Debug);
		CustomItemBase customItem = (CustomItemBase)Activator.CreateInstance(type);
		RegisterCustomItem(customItem);
	}

	/// <summary>
	/// Register <paramref name="customItemBase"/> as a custom item.
	/// </summary>
	/// <param name="customItemBase">The instanciated <see cref="CustomItemBase"/>.</param>
	public static void RegisterCustomItem(this CustomItemBase? customItemBase)
	{
		if (customItemBase == null)
		{
			return;
		}

		CustomItemEvents.OnRegistered(customItemBase);
		customItemBase.OnRegistered();
		CustomItemBaseList.Add(customItemBase);
	}

	/// <summary>
	/// Unregister <paramref name="types"/> from custom items.
	/// </summary>
	/// <param name="types">Parameter array of the <see cref="Type"/> of a <see cref="CustomItemBase"/>.</param>
	public static void UnRegisterCustomItems(params Type[] types)
	{
		if (types == null)
		{
			return;
		}

		foreach (var type in types)
		{
			UnRegisterCustomItem(type);
		}
	}

	/// <summary>
	/// Unregister <paramref name="type"/> from custom items.
	/// </summary>
	/// <param name="type">The <see cref="Type"/> of a <see cref="CustomItemBase"/>.</param>
	public static void UnRegisterCustomItem(Type type)
	{
		CustomItemBaseList.RemoveWhere(x => x.GetType() == type);
	}

	/// <summary>
	/// Unregister <paramref name="customItemBase"/> from custom items.
	/// </summary>
	/// <param name="customItemBase">The instanciated <see cref="CustomItemBase"/>.</param>
	public static void UnRegisterCustomItem(this CustomItemBase customItemBase)
	{
		if (customItemBase == null)
		{
			return;
		}

		CustomItemEvents.OnUnregistered(customItemBase);
		customItemBase.OnUnRegistered();
		CustomItemBaseList.Remove(customItemBase);
	}

	/// <summary>
	/// Unregister all custom items.
	/// </summary>
	public static void UnRegisterCustomItems()
	{
		Assembly assembly = Assembly.GetCallingAssembly();
		List<Type> types = [.. assembly.GetTypes().Where(x => !x.IsAbstract).Where(item => item.BaseType == typeof(CustomItemBase) || item.BaseType.IsSubclassOf(typeof(CustomItemBase)))];
		foreach (var item in CustomItemBaseList.Where(x => types.Contains(x.GetType())).ToList())
		{
			UnRegisterCustomItem(item);
		}

		SerialToCustomItem.Clear();
	}

	/// <summary>
	/// Unregister all custom items.
	/// </summary>
	public static void UnRegisterAllCustomItems()
	{
		foreach (var item in CustomItemBaseList.ToList())
		{
			UnRegisterCustomItem(item);
		}

		SerialToCustomItem.Clear();
	}

	/// <summary>
	/// Clear custom items serials.
	/// </summary>
	public static void ClearSerials()
	{
		SerialToCustomItem.Clear();
	}

	/// <summary>
	/// Adding custom item to the serial.
	/// </summary>
	/// <param name="serial">The serial.</param>
	/// <param name="customItem">The custom item.</param>
	public static void AddCustomItemToSerial(ushort serial, CustomItemBase? customItem)
	{
		if (serial == 0)
		{
			return;
		}

		if (customItem == null)
		{
			return;
		}

		if (SerialToCustomItem.ContainsKey(serial))
		{
			return;
		}

		SerialToCustomItem.Add(serial, customItem);
	}
}
