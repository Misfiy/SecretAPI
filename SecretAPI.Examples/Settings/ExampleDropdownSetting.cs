namespace SecretAPI.Examples.Settings
{
    using LabApi.Features.Console;
    using LabApi.Features.Wrappers;
    using SecretAPI.Features.UserSettings;

    /// <summary>
    /// Example version of <see cref="CustomDropdownSetting"/>.
    /// </summary>
    public class ExampleDropdownSetting : CustomDropdownSetting
    {
        private static string[] exampleOptions = ["hi", "test", "yum", "fish", "nugget"];

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
        protected override void HandleSettingUpdate(Player player)
        {
            Logger.Info(SelectedOption);
        }
    }
}