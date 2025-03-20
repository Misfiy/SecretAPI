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
        /// <param name="id"></param>
        /// <param name="label"></param>
        /// <param name="header"></param>
        /// <param name="onChanged"></param>
        /// <param name="buttonText"></param>
        /// <param name="holdTimeSeconds"></param>
        /// <param name="hint"></param>
        /// <param name="permissionCheck"></param>
        public CustomButtonSetting(int? id, string label, CustomHeader header, Action<Player, CustomSetting> onChanged, string buttonText, float? holdTimeSeconds = null, string? hint = null, Predicate<Player>? permissionCheck = null)
            : base(new SSButton(id, label, buttonText, holdTimeSeconds, hint), header, onChanged, permissionCheck)
        {
            Base = (SSButton)base.Base;
        }

        /// <inheritdoc/>
        public new SSButton Base { get; }
    }
}