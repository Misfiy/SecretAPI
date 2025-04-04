namespace SecretAPI.Features.UserSettings
{
    using global::UserSettings.ServerSpecific;
    using LabApi.Features.Wrappers;

    /// <summary>
    /// Example setting to use during testing.
    /// </summary>
    public class ExampleSetting : CustomButtonSetting
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExampleSetting"/> class.
        /// </summary>
        public ExampleSetting()
            : base(1, "Example Kill Button", "Kill")
        {
        }

        /// <inheritdoc />
        public override CustomHeader Header { get; } = new("Examples");

        /// <inheritdoc />
        protected override CustomSetting CreateDuplicate() => new ExampleSetting();

        /// <inheritdoc />
        protected override bool CanView(Player player) => true;

        /// <inheritdoc />
        protected override void HandleSettingUpdate(Player player)
        {
            player.Kill();
        }
    }
}