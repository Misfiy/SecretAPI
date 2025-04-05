namespace SecretAPI.Features.UserSettings
{
    using LabApi.Features.Wrappers;
    using UnityEngine;

    /// <summary>
    /// Example setting to use during testing.
    /// </summary>
    public class ExampleSetting : CustomKeybindSetting
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExampleSetting"/> class.
        /// </summary>
        public ExampleSetting()
            : base(900, "Example Kill Button", KeyCode.G)
        {
        }

        /// <inheritdoc />
        public override CustomHeader Header { get; } = CustomHeader.Examples;

        /// <inheritdoc />
        protected override CustomSetting CreateDuplicate() => new ExampleSetting();

        /// <inheritdoc />
        protected override void HandleSettingUpdate(Player player)
        {
            player.Kill();
        }
    }
}