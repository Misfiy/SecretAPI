﻿namespace SecretAPI.Features.UserSettings
{
    using global::UserSettings.ServerSpecific;
    using SecretAPI.Interfaces;

    /// <summary>
    /// Wraps <see cref="SSGroupHeader"/>.
    /// </summary>
    public class CustomHeader : ISetting<SSGroupHeader>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomHeader"/> class.
        /// </summary>
        /// <param name="label">The label to show.</param>
        /// <param name="reducedPadding">Reduced padding.</param>
        /// <param name="hint">Hint displayed.</param>
        public CustomHeader(string label, bool reducedPadding = false, string? hint = null)
        {
            Base = new SSGroupHeader(label, reducedPadding, hint);
        }

        /// <inheritdoc />
        public SSGroupHeader Base { get; }
    }
}