namespace SecretAPI.Features.UserSettings
{
    using global::UserSettings.ServerSpecific;
    using SecretAPI.Interfaces;

    /// <summary>
    /// <see cref="SSButton"/> wrapper. Make this proper.
    /// </summary>
    public abstract class CustomButtonSetting : CustomSetting, ISetting<SSButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomButtonSetting"/> class.
        /// </summary>
        /// <param name="id">The id of the button.</param>
        /// <param name="label">The label of the button.</param>
        /// <param name="buttonText">The button text.</param>
        /// <param name="holdTimeSeconds">The time to hold.</param>
        /// <param name="hint">The hint to show.</param>
        protected CustomButtonSetting(int? id, string label, string buttonText, float? holdTimeSeconds = null, string? hint = null)
            : base(new SSButton(id, label, buttonText, holdTimeSeconds, hint))
        {
            Base = (SSButton)base.Base;
        }

        /// <inheritdoc/>
        public new SSButton Base { get; }
    }
}