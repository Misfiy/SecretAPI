namespace SecretAPI.Features.UserSettings
{
    using global::UserSettings.ServerSpecific;

    /// <summary>
    /// Handles <see cref="ServerSpecificSettingBase"/>.
    /// </summary>
    /// <typeparam name="T">The setting being wrapped.</typeparam>
    public interface ISetting<T>
        where T : ServerSpecificSettingBase
    {
        /// <summary>
        /// Gets the base of the setting.
        /// </summary>
        public T Base { get; }
    }
}