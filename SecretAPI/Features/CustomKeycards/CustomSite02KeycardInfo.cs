namespace SecretAPI.Features.CustomKeycards
{
    /// <summary>
    /// Handles info related to <see cref="ItemType.KeycardCustomSite02"/>.
    /// </summary>
    public abstract class CustomSite02KeycardInfo : CustomKeycardInfo
    {
        /// <inheritdoc />
        public override ItemType ItemType => ItemType.KeycardCustomSite02;
    }
}