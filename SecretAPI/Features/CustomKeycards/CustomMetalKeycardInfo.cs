namespace SecretAPI.Features.CustomKeycards
{
    /// <summary>
    /// Handles info related to <see cref="ItemType.KeycardCustomMetalCase"/>.
    /// </summary>
    public abstract class CustomMetalKeycardInfo : CustomKeycardInfo
    {
        /// <inheritdoc />
        public override ItemType ItemType => ItemType.KeycardCustomMetalCase;
    }
}