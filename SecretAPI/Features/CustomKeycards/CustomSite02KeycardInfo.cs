namespace SecretAPI.Features.CustomKeycards
{
    using LabApi.Features.Wrappers;

    /// <summary>
    /// Handles info related to <see cref="ItemType.KeycardCustomSite02"/>.
    /// </summary>
    public abstract class CustomSite02KeycardInfo : CustomKeycardInfo
    {
        /// <inheritdoc />
        public override ItemType ItemType => ItemType.KeycardCustomSite02;

        /// <inheritdoc />
        public override KeycardItem GiveKeycard(Player player)
        {
        }
    }
}