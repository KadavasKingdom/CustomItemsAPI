using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Modules;
using System.Diagnostics.CodeAnalysis;

namespace CustomItemsAPI.Extensions;

/// <summary>
/// Provides an extension for <see cref="Firearm"/>.
/// </summary>
public static class FirearmExtension
{
	/// <summary>
	/// Tries to get the module.
	/// </summary>
	/// <param name="firearm">The firearm.</param>
	/// <param name="type">The module type.</param>
	/// <param name="module">The module.</param>
	/// <param name="ignoreSubmodules">Wheter to ignore the submodules.</param>
	/// <returns><see langword="true"/> if success; otherwise <see langword="false"/>.</returns>
	public static bool TryGetModule([NotNull] this Firearm firearm, Type type, [NotNullWhen(true)] out object? module, bool ignoreSubmodules = true)
	{
		ModuleBase[] modules = firearm.Modules;
		foreach (ModuleBase moduleBase in modules)
		{
			if ((!ignoreSubmodules || !moduleBase.IsSubmodule) && moduleBase.GetType() == type)
			{
				module = moduleBase;
				return true;
			}
		}

		module = default;
		return false;
	}
}
