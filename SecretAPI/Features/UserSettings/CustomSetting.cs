namespace SecretAPI.Features.UserSettings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using global::UserSettings.ServerSpecific;
    using LabApi.Features.Console;
    using LabApi.Features.Wrappers;
    using Mirror;
    using SecretAPI.Interfaces;

    /// <summary>
    /// Wraps <see cref="ServerSpecificSettingBase"/>.
    /// </summary>
    public abstract class CustomSetting : ISetting<ServerSpecificSettingBase>
    {
        private static Dictionary<Player, List<CustomSetting>> customSettings = [];

        static CustomSetting()
        {
            ServerSpecificSettingsSync.SendOnJoinFilter = null;
            LabApi.Events.Handlers.PlayerEvents.Joined += ev => SendSettingsToPlayer(ev.Player);
            LabApi.Events.Handlers.PlayerEvents.GroupChanged += ev => SendSettingsToPlayer(ev.Player);
            ServerSpecificSettingsSync.ServerOnSettingValueReceived += OnSettingsUpdated;
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
        public static IReadOnlyDictionary<Player, List<CustomSetting>> PlayerSettings => customSettings;

        /// <inheritdoc />
        public ServerSpecificSettingBase Base { get; }

        /// <summary>
        /// Gets the <see cref="CustomHeader"/> of the setting.
        /// </summary>
        public abstract CustomHeader Header { get; }

        /// <summary>
        /// Gets or sets the current label.
        /// </summary>
        public string Label
        {
            get => Base.Label;
            set => Base.Label = value;
        }

        /// <summary>
        /// Gets or sets the current id.
        /// </summary>
        public int Id
        {
            get => Base.SettingId;
            set => Base.SettingId = value;
        }

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
        /// Gets a <see cref="CustomSetting"/>, used for validation.
        /// </summary>
        /// <param name="type">The type of the base setting.</param>
        /// <param name="id">The id of the setting.</param>
        /// <returns>The found <see cref="CustomSetting"/> matching the params, otherwise null.</returns>
        public static CustomSetting? Get(Type type, int id)
            => CustomSettings.FirstOrDefault(s => s.Base.SettingId == id && s.Base.GetType() == type);

        /// <summary>
        /// Checks if a player is able to view a setting.
        /// </summary>
        /// <param name="player">The player to check.</param>
        /// <returns>A value indicating whether a player is able to view the setting.</returns>
        protected virtual bool CanView(Player player) => true;

        /// <summary>
        /// Creates a duplicate of the current setting.
        /// </summary>
        /// <returns>The duplicate setting created.</returns>
        protected abstract CustomSetting CreateDuplicate();

        /// <summary>
        /// Handles the updating of a setting.
        /// </summary>
        /// <param name="player">The player to update.</param>
        protected abstract void HandleSettingUpdate(Player player);

        private static void SendSettingsToPlayer(Player player, int version = 1)
        {
            IEnumerable<CustomSetting> hasAccess = CustomSettings.Where(s => s.CanView(player));
            List<ServerSpecificSettingBase> ordered = [];
            foreach (IGrouping<CustomHeader, CustomSetting> grouping in hasAccess.GroupBy(setting => setting.Header))
            {
                ordered.Add(grouping.Key.Base);
                ordered.AddRange(grouping.Select(setting => setting.Base));
            }

            ordered.AddRange(ServerSpecificSettingsSync.DefinedSettings);

            ServerSpecificSettingsSync.SendToPlayer(player.ReferenceHub, [.. ordered], version);
        }

        private static void OnSettingsUpdated(ReferenceHub hub, ServerSpecificSettingBase settingBase)
        {
            if (hub.IsHost)
                return;

            Player player = Player.Get(hub);

            CustomSetting? setting = CustomSettings.FirstOrDefault(s => s.Base.SettingId == settingBase.SettingId);
            if (setting == null || !setting.CanView(player))
                return;

            CustomSetting newSettingPlayer = EnsurePlayerSpecificSetting(player, setting);

            NetworkWriter writer = new();
            setting.Base.SerializeEntry(writer);
            newSettingPlayer.Base.DeserializeEntry(new NetworkReader(writer.buffer));
            newSettingPlayer.HandleSettingUpdate(player);
        }

        private static CustomSetting EnsurePlayerSpecificSetting(Player player, CustomSetting toMatch)
        {
            List<CustomSetting> settings = customSettings.GetOrAdd(player, () => []);
            CustomSetting? currentSetting = settings.FirstOrDefault(s => s.Id == toMatch.Id);
            if (currentSetting == null)
            {
                currentSetting = toMatch.CreateDuplicate();
                settings.Add(currentSetting);
            }

            return currentSetting;
        }
    }
}