﻿namespace SecretAPI.Features.UserSettings
{
    using System;
    using global::UserSettings.ServerSpecific;

    /// <summary>
    /// Custom <see cref="SSDropdownSetting"/> wrapper.
    /// </summary>
    public abstract class CustomDropdownSetting : CustomSetting, ISetting<SSDropdownSetting>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomDropdownSetting"/> class.
        /// </summary>
        /// <param name="setting">The base setting to create the wrapper with.</param>
        protected CustomDropdownSetting(SSDropdownSetting setting)
            : base(setting)
        {
            Base = setting;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomDropdownSetting"/> class.
        /// </summary>
        /// <param name="id">The ID of the setting.</param>
        /// <param name="label">The setting's label.</param>
        /// <param name="options">The array of string options to give.</param>
        /// <param name="defaultOptionIndex">The default option (int index).</param>
        /// <param name="entryType">The entry type.</param>
        /// <param name="hint">The hint to show.</param>
        protected CustomDropdownSetting(
            int? id,
            string label,
            string[] options,
            int defaultOptionIndex = 0,
            SSDropdownSetting.DropdownEntryType entryType = SSDropdownSetting.DropdownEntryType.Regular,
            string? hint = null)
            : this(new SSDropdownSetting(id, label, options, defaultOptionIndex, entryType, hint))
        {
        }

        /// <inheritdoc/>
        public new SSDropdownSetting Base { get; }

        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        public string[] Options
        {
            get => Base.Options;
            [Obsolete("Setting this value is not currently supported.")]
            set => Base.Options = value;
        }

        /// <summary>
        /// Gets the selected option as string.
        /// </summary>
        public string SelectedOption => Base.SyncSelectionText;
    }
}