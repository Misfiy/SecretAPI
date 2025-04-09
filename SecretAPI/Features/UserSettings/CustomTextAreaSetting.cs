namespace SecretAPI.Features.UserSettings
{
    using global::UserSettings.ServerSpecific;
    using SecretAPI.Interfaces;
    using TMPro;

    /// <summary>
    /// Plain text setting.
    /// </summary>
    public abstract class CustomTextAreaSetting : CustomSetting, ISetting<SSTextArea>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomTextAreaSetting"/> class.
        /// </summary>
        /// <param name="setting">The setting to wrap.</param>
        protected CustomTextAreaSetting(SSTextArea setting)
            : base(setting)
        {
            Base = setting;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomTextAreaSetting"/> class.
        /// </summary>
        /// <param name="id">The id of the setting.</param>
        /// <param name="content">The content of the setting.</param>
        /// <param name="foldoutMode">The foldout mode.</param>
        /// <param name="collapsedText">The collapsed text.</param>
        /// <param name="textAlignment">The align for the text.</param>
        protected CustomTextAreaSetting(
            int? id,
            string content,
            SSTextArea.FoldoutMode foldoutMode = SSTextArea.FoldoutMode.NotCollapsable,
            string? collapsedText = null,
            TextAlignmentOptions textAlignment = TextAlignmentOptions.TopLeft)
            : this(new SSTextArea(id, content, foldoutMode, collapsedText, textAlignment))
        {
        }

        /// <inheritdoc />
        public new SSTextArea Base { get; }
    }
}