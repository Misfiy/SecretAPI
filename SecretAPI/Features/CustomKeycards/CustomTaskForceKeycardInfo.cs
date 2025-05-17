namespace SecretAPI.Features.CustomKeycards
{
    using Interactables.Interobjects.DoorUtils;

    /// <summary>
    /// Handles info related to <see cref="ItemType.KeycardCustomTaskForce"/>.
    /// </summary>
    public class CustomTaskForceKeycardInfo : CustomKeycardInfo
    {
        /// <inheritdoc />
        public override ItemType ItemType => ItemType.KeycardCustomTaskForce;

        public CustomTaskForceKeycardInfo(string itemName, KeycardLevels keycardPermissions)
            : base(itemName, keycardPermissions)
        {
        }
    }
}