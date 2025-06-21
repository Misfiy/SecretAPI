namespace SecretAPI.Examples.Settings
{
    using LabApi.Features.Console;
    using LabApi.Features.Permissions;
    using SecretAPI.Features.UserSettings;

    /// <summary>
    /// Example version of <see cref="CustomDropdownSetting"/>.
    /// </summary>
    public class ExampleDropdownSetting : CustomDropdownSetting
    {
        private static string[] exampleOptions = ["hi", "test", "yum", "fish", "nugget"];
        private static string[] exampleSupporterOptions = ["bucket", "lava", "wanted", "globe"];

        /// <summary>
        /// Initializes a new instance of the <see cref="ExampleDropdownSetting"/> class.
        /// </summary>
        public ExampleDropdownSetting()
            : base(901, "Example dropdown", exampleOptions)
        {
        }

        /// <inheritdoc/>
        public override CustomHeader Header { get; } = CustomHeader.Examples;

        /// <inheritdoc/>
        protected override CustomSetting CreateDuplicate() => new ExampleDropdownSetting();

        /// <inheritdoc/>
        protected override void UpdatePlayerSetting()
        {
            if (KnownOwner == null || !KnownOwner.HasAnyPermission("example.supporter"))
                return;

            Options = exampleSupporterOptions;
        }

        /// <inheritdoc/>
        protected override void HandleSettingUpdate()
        {
            Logger.Info($"{KnownOwner?.DisplayName ?? "null reference"} selected {SelectedOption} (Index {ValidatedSelectedIndex}/{Options.Length})");
        }
    }
}