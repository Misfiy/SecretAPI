namespace SecretAPI.Features.CustomKeycards
{
    using Interactables.Interobjects.DoorUtils;

    /// <summary>
    /// Handles info related to <see cref="ItemType.KeycardCustomSite02"/>.
    /// </summary>
    public class CustomSite02KeycardInfo : CustomKeycardInfo
    {
        /// <inheritdoc />
        public override ItemType ItemType => ItemType.KeycardCustomSite02;

        public CustomTaskForceKeycardInfo(string itemName, KeycardLevels keycardPermissions)
            : base(itemName, keycardPermissions)
        {
        }
    }
}