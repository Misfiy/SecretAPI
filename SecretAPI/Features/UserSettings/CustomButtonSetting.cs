namespace SecretAPI.Features.UserSettings
{
    using System;
    using global::UserSettings.ServerSpecific;
    using LabApi.Features.Wrappers;
    using SecretAPI.Interfaces;

    /// <summary>
    /// <see cref="SSButton"/> wrapper. Make this proper.
    /// </summary>
    public class CustomButtonSetting : CustomSetting, ISetting<SSButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomButtonSetting"/> class.
        /// </summary>
        /// <param name="id">The id of the button.</param>
        /// <param name="label">The label of the button.</param>
        /// <param name="header">The header to use as a base.</param>
        /// <param name="onChanged">What to do on button using.</param>
        /// <param name="buttonText">The button text.</param>
        /// <param name="holdTimeSeconds">The time to hold.</param>
        /// <param name="hint">The hint to show.</param>
        /// <param name="permissionCheck">The permission check required.</param>
        public CustomButtonSetting(int? id, string label, CustomHeader header, Action<Player, CustomSetting> onChanged, string buttonText, float? holdTimeSeconds = null, string? hint = null, Predicate<Player>? permissionCheck = null)
            : base(new SSButton(id, label, buttonText, holdTimeSeconds, hint), header, onChanged, permissionCheck)
        {
            Base = (SSButton)base.Base;
        }

        /// <inheritdoc/>
        public new SSButton Base { get; }
    }
}