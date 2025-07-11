namespace SecretAPI.Examples.Settings
{
    using LabApi.Features.Wrappers;
    using SecretAPI.Features.UserSettings;
    using UnityEngine;
    using Logger = LabApi.Features.Console.Logger;

    /// <summary>
    /// Example setting for keybinds.
    /// </summary>
    public class ExampleKeybindSetting : CustomKeybindSetting
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExampleKeybindSetting"/> class.
        /// </summary>
        public ExampleKeybindSetting()
            : base(900, "Example Kill Button", KeyCode.G, allowSpectatorTrigger: false)
        {
        }

        /// <inheritdoc />
        public override CustomHeader Header { get; } = CustomHeader.Examples;

        /// <inheritdoc />
        protected override CustomSetting CreateDuplicate() => new ExampleKeybindSetting();

        /// <inheritdoc />
        protected override void HandleSettingUpdate(Player player)
        {
            if (!IsPressed)
                return;

            player.Kill();
        }
    }
}