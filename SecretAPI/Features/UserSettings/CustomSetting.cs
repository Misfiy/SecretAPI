namespace SecretAPI.Features.UserSettings
{
    using System;
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