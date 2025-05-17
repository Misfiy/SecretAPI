namespace SecretAPI.Features.CustomKeycards
{
    /// <summary>
    /// Handles info related to <see cref="ItemType.KeycardCustomTaskForce"/>.
    /// </summary>
    public abstract class CustomTaskForceKeycardInfo : CustomKeycardInfo
    {
        /// <inheritdoc />
        public override ItemType ItemType => ItemType.KeycardCustomTaskForce;
    }
}