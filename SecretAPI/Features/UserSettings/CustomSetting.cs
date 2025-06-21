namespace SecretAPI.Features.UserSettings
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using global::UserSettings.ServerSpecific;
    using LabApi.Events.Handlers;
    using LabApi.Features.Console;
    using LabApi.Features.Wrappers;
    using Mirror;
    using NorthwoodLib.Pools;
    using SecretAPI.Extensions;

    /// <summary>
    /// Wraps <see cref="ServerSpecificSettingBase"/>.
    /// </summary>
    public abstract class CustomSetting : ISetting<ServerSpecificSettingBase>
    {
        private static readonly Dictionary<Player, List<CustomSetting>> ReceivedPlayerSettings = [];

        static CustomSetting()
        {
            SecretApi.Harmony?.PatchCategory(nameof(CustomSetting));

            ServerSpecificSettingsSync.SendOnJoinFilter = null;
            ServerSpecificSettingsSync.DefinedSettings ??= []; // fix nw issue
            ServerSpecificSettingsSync.ServerOnSettingValueReceived += OnSettingsUpdated;

            PlayerEvents.Joined += ev => SendSettingsToPlayer(ev.Player);
            PlayerEvents.GroupChanged += ev => SendSettingsToPlayer(ev.Player);
            PlayerEvents.Left += ev => RemoveStoredPlayer(ev.Player);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomSetting"/> class.
        /// </summary>
        /// <param name="setting">The setting to use for custom setting.</param>
        protected CustomSetting(ServerSpecificSettingBase setting)
        {
            Base = setting;
        }

        /// <summary>
        /// Gets the registered custom settings.
        /// </summary>
        public static List<CustomSetting> CustomSettings { get; } = [];

        /// <summary>
        /// Gets a dictionary of player to their received custom settings.
        /// </summary>
        public static IReadOnlyDictionary<Player, List<CustomSetting>> PlayerSettings => ReceivedPlayerSettings;

        /// <inheritdoc />
        public ServerSpecificSettingBase Base { get; }

        /// <summary>
        /// Gets the known owner.
        /// </summary>
        /// <remarks>This is null on the original object .</remarks>
        public Player? KnownOwner { get; private set; }

        /// <summary>
        /// Gets the <see cref="CustomHeader"/> of the setting.
        /// </summary>
        public abstract CustomHeader Header { get; }

        /// <summary>
        /// Gets the current label.
        /// </summary>
        public string Label => Base.Label;

        /// <summary>
        /// Gets the current id.
        /// </summary>
        public int Id => Base.SettingId;

        /// <summary>
        /// Registers a collection of settings.
        /// </summary>
        /// <param name="settings">The settings to register.</param>
        public static void Register(params CustomSetting[] settings) => CustomSettings.AddRange(settings);

        /// <summary>
        /// Registers a collection of settings.
        /// </summary>
        /// <param name="settings">The settings to register.</param>
        public static void Register(IEnumerable<CustomSetting> settings) => CustomSettings.AddRange(settings);

        /// <summary>
        /// Unregisters collection of settings.
        /// </summary>
        /// <param name="settings">The settings to unregister.</param>
        public static void UnRegister(params CustomSetting[] settings) => CustomSettings.RemoveAll(s => settings.Contains(s));

        /// <summary>
        /// Unregisters a collection of settings.
        /// </summary>
        /// <param name="settings">The settings to unregister.</param>
        public static void UnRegister(IEnumerable<CustomSetting> settings) => CustomSettings.RemoveAll(s => settings.Contains(s));

        /// <summary>
        /// Tries to get player specific setting.
        /// </summary>
        /// <param name="player">The player to get settings of.</param>
        /// <param name="setting">The setting found.</param>
        /// <typeparam name="TSetting">The setting type to find.</typeparam>
        /// <returns>Whether setting was found.</returns>
        public static bool TryGetPlayerSetting<TSetting>(Player player, [NotNullWhen(true)] out TSetting? setting)
            where TSetting : CustomSetting
        {
            setting = null;

            if (!PlayerSettings.TryGetValue(player, out List<CustomSetting>? settings))
                return false;

            foreach (CustomSetting toCheck in settings)
            {
                if (toCheck is TSetting value)
                {
                    setting = value;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets a player's <see cref="CustomSetting"/>.
        /// </summary>
        /// <param name="id">The ID of the setting.</param>
        /// <param name="player">The player of which to get the setting from.</param>
        /// <typeparam name="T">The setting class to check for.</typeparam>
        /// <returns>The found <see cref="CustomSetting"/> matching the params, otherwise null.</returns>
        public static T? GetPlayerSetting<T>(int id, Player player)
            where T : CustomSetting => PlayerSettings.TryGetValue(player, out List<CustomSetting> settings)
            ? settings.FirstOrDefault(s => s.Base.SettingId == id && s.GetType() == typeof(T)) as T
            : null;

        /// <summary>
        /// Gets a <see cref="CustomSetting"/>, used for validation.
        /// </summary>
        /// <param name="type">The type of the base setting.</param>
        /// <param name="id">The ID of the setting.</param>
        /// <returns>The found <see cref="CustomSetting"/> matching the params, otherwise null.</returns>
        public static CustomSetting? Get(Type type, int id)
            => CustomSettings.FirstOrDefault(s => s.Base.SettingId == id && s.Base.GetType() == type);

        /// <summary>
        /// Gets a <see cref="CustomSetting"/>, used for validation.
        /// </summary>
        /// <param name="id">The ID of the setting.</param>
        /// <typeparam name="T">The setting class to check for.</typeparam>
        /// <returns>The found <see cref="CustomSetting"/> matching the params, otherwise null.</returns>
        public static T? Get<T>(int id)
            where T : CustomSetting => CustomSettings.FirstOrDefault(s => s.Base.SettingId == id && s.GetType() == typeof(T)) as T;

        /// <summary>
        /// Resyncs all settings to all players.
        /// </summary>
        /// <param name="version">The version of the setting. If null will use <see cref="ServerSpecificSettingsSync.Version"/>.</param>
        public static void ResyncServer(int? version = null)
        {
            foreach (Player player in Player.ReadyList)
                SendSettingsToPlayer(player, version);
        }

        /// <summary>
        /// Updates the settings of a player based on <see cref="CanView"/>.
        /// </summary>
        /// <param name="player">The player to update.</param>
        /// <param name="version">The version of the setting. If null will use <see cref="ServerSpecificSettingsSync.Version"/>.</param>
        /// <remarks>This will be automatically called on <see cref="PlayerEvents.Joined"/> and <see cref="PlayerEvents.GroupChanged"/>.</remarks>
        public static void SendSettingsToPlayer(Player player, int? version = null)
        {
            if (player.IsHost)
                return;

            List<CustomSetting> playerSettings = ListPool<CustomSetting>.Shared.Rent();
            foreach (CustomSetting setting in CustomSettings)
            {
                if (!setting.CanView(player))
                    continue;

                CustomSetting playerSpecific = EnsurePlayerSpecificSetting(player, setting);
                playerSpecific.UpdatePlayerSetting();
                playerSettings.Add(playerSpecific);
            }

            List<ServerSpecificSettingBase> ordered = ListPool<ServerSpecificSettingBase>.Shared.Rent();
            foreach (IGrouping<CustomHeader, CustomSetting> grouping in playerSettings.GroupBy(static setting => setting.Header))
            {
                ordered.Add(grouping.Key.Base);
                ordered.AddRange(grouping.Select(static setting => setting.Base));
            }

            if (ServerSpecificSettingsSync.DefinedSettings != null)
                ordered.AddRange(ServerSpecificSettingsSync.DefinedSettings);

            ServerSpecificSettingsSync.SendToPlayer(player.ReferenceHub, ordered.ToArray(), version);

            ListPool<CustomSetting>.Shared.Return(playerSettings);
            ListPool<ServerSpecificSettingBase>.Shared.Return(ordered);
        }

        /// <summary>
        /// Resyncs the setting to its owner.
        /// </summary>
        protected void ResyncToOwner()
        {
            if (KnownOwner != null)
                SendSettingsToPlayer(KnownOwner);
        }

        /// <summary>
        /// Checks if a player is able to view a setting.
        /// </summary>
        /// <param name="player">The player to check.</param>
        /// <returns>A value indicating whether a player is able to view the setting.</returns>
        protected virtual bool CanView(Player player) => true;

        /// <summary>
        /// Creates a duplicate of the current setting. Used to properly sync values and implement <see cref="PlayerSettings"/>.
        /// </summary>
        /// <returns>The duplicate setting created.</returns>
        protected abstract CustomSetting CreateDuplicate();

        /// <summary>
        /// Called before setting is sent to a player. Should be used to create player specific options.
        /// </summary>
        protected virtual void UpdatePlayerSetting()
        {
        }

        /// <summary>
        /// Called when client sends a new value on the setting.
        /// </summary>
        protected abstract void HandleSettingUpdate();

        private static void RemoveStoredPlayer(Player player) => ReceivedPlayerSettings.Remove(player);

        private static void OnSettingsUpdated(ReferenceHub hub, ServerSpecificSettingBase settingBase)
        {
            if (hub.IsHost)
                return;

            Player player = Player.Get(hub);

            CustomSetting? setting = CustomSettings.FirstOrDefault(s => s.Base.SettingId == settingBase.SettingId);
            if (setting == null || !setting.CanView(player))
                return;

            CustomSetting newSettingPlayer = EnsurePlayerSpecificSetting(player, setting);

            // NetworkWriter entryWriter = new();
            // settingBase.SerializeEntry(entryWriter);
            // newSettingPlayer.Base.DeserializeEntry(new NetworkReader(entryWriter.buffer));
            NetworkWriter valueWriter = new();
            settingBase.SerializeValue(valueWriter);
            newSettingPlayer.Base.DeserializeValue(new NetworkReader(valueWriter.buffer));

            newSettingPlayer.HandleSettingUpdate();
        }

        private static CustomSetting EnsurePlayerSpecificSetting(Player player, CustomSetting toMatch)
        {
            List<CustomSetting> settings = ReceivedPlayerSettings.GetOrAdd(player, () => []);
            CustomSetting? currentSetting = settings.FirstOrDefault(s => s.Id == toMatch.Id);
            if (currentSetting == null)
            {
                currentSetting = toMatch.CreateDuplicate();
                currentSetting.KnownOwner = player;
                settings.Add(currentSetting);
            }

            return currentSetting;
        }
    }
}
