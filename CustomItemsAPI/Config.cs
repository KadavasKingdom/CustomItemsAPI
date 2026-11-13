using System.ComponentModel;

namespace CustomItemsAPI;

/// <summary>
/// Configuration for Custom Items.
/// </summary>
internal sealed class Config
{
	/// <summary>
	/// Gets or sets a value indicating whether should print out Debug related information.
	/// </summary>
	public bool Debug { get; set; }
#if DEBUG
	= true;
#endif

	/// <summary>
	/// Gets or sets a value indicating whether should show the hint when custom item being picked up.
	/// </summary>
	public bool ShowPickedUpHint { get; set; } = true;

	/// <summary>
	/// Gets or sets the picked up hint.
	/// </summary>
	[Description("Hint when you picked up the custom item. {0}: DisplayName {1}: Description.")]
	public string PickedUpHint { get; set; } = "You picked up {0}\n{1}";

	/// <summary>
	/// Gets or sets a value indicating whether should show the hint when custom item being selected.
	/// </summary>
	public bool ShowSelectedHint { get; set; } = true;

	/// <summary>
	/// Gets or sets the selected hint.
	/// </summary>
	[Description("Hint when you selected a the custom item. {0}: DisplayName {1}: Description.")]
	public string SelectedHint { get; set; } = "You selected {0}\n{1}";
}
