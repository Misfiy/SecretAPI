namespace SecretAPI.Features.UserSettings
{
    using System.Collections.Generic;
    using System.Linq;
    using global::UserSettings.ServerSpecific;
    using LabApi.Features.Wrappers;
    using SecretAPI.Interfaces;

    /// <summary>
    /// Wraps <see cref="ServerSpecificSettingBase"/>.
    /// </summary>
    public abstract class CustomSetting : ISetting<ServerSpecificSettingBase>
    {
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

        /// <inheritdoc />
        public ServerSpecificSettingBase Base { get; }

        /// <summary>
        /// Gets the <see cref="CustomHeader"/> of the setting.
        /// </summary>
        public abstract CustomHeader Header { get; }

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
        /// Checks if a player is able to view a setting.
        /// </summary>
        /// <param name="player">The player to check.</param>
        /// <returns>A value indicating whether a player is able to view the setting.</returns>
        protected virtual bool CanView(Player player) => true;

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

            ServerSpecificSettingsSync.SendToPlayer(player.ReferenceHub, [.. ordered], version);
        }

        private static void OnSettingsUpdated(ReferenceHub hub, ServerSpecificSettingBase settingBase)
        {
            Player player = Player.Get(hub);

            if (hub.IsHost)
                return;

            CustomSetting? setting = CustomSettings.FirstOrDefault(s => s.Base.SettingId == settingBase.SettingId);
            if (setting == null || !setting.CanView(player))
                return;

            setting.HandleSettingUpdate(player);
        }
    }
}