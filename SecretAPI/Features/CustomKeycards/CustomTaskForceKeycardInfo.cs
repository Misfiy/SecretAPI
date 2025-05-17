namespace SecretAPI.Features.CustomKeycards
{
    using LabApi.Features.Wrappers;

    /// <summary>
    /// Handles info related to <see cref="ItemType.KeycardCustomTaskForce"/>.
    /// </summary>
    public abstract class CustomTaskForceKeycardInfo : CustomKeycardInfo
    {
        /// <inheritdoc />
        public override ItemType ItemType => ItemType.KeycardCustomTaskForce;

        /// <inheritdoc />
        public override KeycardItem GiveKeycard(Player player)
        {
        }
    }
}