namespace SecretAPI.Features.CustomKeycards
{
    /// <summary>
    /// Handles info related to <see cref="ItemType.KeycardCustomManagement"/>.
    /// </summary>
    public abstract class CustomManagementKeycardInfo : CustomKeycardInfo
    {
        /// <inheritdoc />
        public override ItemType ItemType => ItemType.KeycardCustomManagement;
    }
}