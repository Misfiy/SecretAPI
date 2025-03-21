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
    public class CustomSetting : ISetting<ServerSpecificSettingBase>
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
        /// <param name="header">The header to use.</param>
        /// <param name="onChanged">What to do when setting changes.</param>
        /// <param name="permissionCheck">The check for handling permissions. Null for no check needed.</param>
        public CustomSetting(ServerSpecificSettingBase setting, CustomHeader header, Action<Player, CustomSetting> onChanged, Predicate<Player>? permissionCheck = null)
        {
            Base = setting;
            Header = header;
            OnChanged = onChanged;
            PermissionCheck = permissionCheck;
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
        public CustomHeader Header { get; }

        /// <summary>
        /// Gets the func to validate permission on the player.
        /// </summary>
        public Predicate<Player>? PermissionCheck { get; }

        /// <summary>
        /// Gets the action to do on changes.
        /// </summary>
        public Action<Player, CustomSetting> OnChanged { get; }

        private static void SendSettingsToPlayer(Player player, int version = 1)
        {
            // NW won't validate unless the settings are defined.
            ServerSpecificSettingsSync.DefinedSettings ??= CustomSettings.Select(s => s.Base).ToArray();

            IEnumerable<CustomSetting> hasAccess = CustomSettings.Where(s => s.PermissionCheck == null || s.PermissionCheck(player));
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
            if (setting == null)
                return;

            setting.OnChanged(player, setting);
        }
    }
}