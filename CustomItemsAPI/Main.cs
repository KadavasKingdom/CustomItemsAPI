using CustomItemsAPI.EventHandlers;
using InventorySystem;
using InventorySystem.Items.MicroHID.Modules;
using InventorySystem.Items.ThrowableProjectiles;
using LabApi.Events.CustomHandlers;
using LabApi.Loader;
using LabApi.Loader.Features.Plugins;

namespace CustomItemsAPI;

/// <summary>
/// The main entry point for CustomItemsAPI.
/// </summary>
internal sealed class Main : Plugin<Config>
{
	private static Main? instance;
	private static readonly List<CustomEventsHandler> Handlers =
	[
		new CommonItemHandler(), new KeyCardItemHandler(), new NonItemRelatedHandler(),
		new UsableItemHandler(), new ThrowableItemHandler(), new DamageHandler(),
		new FirearmHandler(), new Scp914Handler(), new JailbirdHandler(),
		new RevolverHandler(), new Scp127Handler(), new CoinHandler(),
		new Scp1509Handler(), new ArmorHandler(),
	];

	/// <summary>
	/// Gets the instance of CustomItemsAPI.
	/// </summary>
	public static Main Instance
	{
		get
		{
			if (instance == null)
			{
				throw new ArgumentNullException(nameof(Instance));
			}

			return instance;
		}
	}

	/// <inheritdoc/>
	public override string Name => "CustomItemsAPI";

	/// <inheritdoc/>
	public override string Description => "Enabling creating custom items";

	/// <inheritdoc/>
	public override string Author => "SlejmUr & KadavasKingdom";

	/// <inheritdoc/>
	public override Version Version => Versioning.Version;

	/// <inheritdoc/>
	public override Version RequiredApiVersion => new(LabApi.Features.LabApiProperties.CompiledVersion);

	/// <inheritdoc/>
	public override void Enable()
	{
		instance = this;
		foreach (var item in Handlers)
		{
			CustomHandlersManager.RegisterEventsHandler(item);
		}

		InventoryExtensions.OnItemRemoved += Subscribed.OnItemRemoved;
		ThrownProjectile.OnProjectileSpawned += Subscribed.ProjectileSpawned;
		CycleController.OnPhaseChanged += Subscribed.PhaseChanged;
		BrokenSyncModule.OnBroken += Subscribed.BrokenSyncModule_OnBroken;
		CustomItems.RegisterCustomItems();
	}

	/// <inheritdoc/>
	public override void Disable()
	{
		foreach (var item in Handlers)
		{
			CustomHandlersManager.UnregisterEventsHandler(item);
		}

		InventoryExtensions.OnItemRemoved -= Subscribed.OnItemRemoved;
		ThrownProjectile.OnProjectileSpawned -= Subscribed.ProjectileSpawned;
		CycleController.OnPhaseChanged -= Subscribed.PhaseChanged;
		BrokenSyncModule.OnBroken -= Subscribed.BrokenSyncModule_OnBroken;
		CustomItems.UnRegisterAllCustomItems();

		instance = null;
	}

	/// <inheritdoc/>
	public override void LoadConfigs()
	{
		if (!this.TryLoadConfig(ConfigFileName, out Config? config))
		{
			CL.Warn("Failed to load the configuration file, using default values.");
			config = new();
		}

		Config = config;
	}
}
