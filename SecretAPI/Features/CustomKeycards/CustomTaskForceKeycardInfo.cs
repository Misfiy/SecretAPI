namespace SecretAPI.Features.CustomKeycards
{
    using LabApi.Features.Wrappers;

    /// <summary>
    /// Handles info related to <see cref="ItemType.KeycardCustomTaskForce"/>.
    /// </summary>
    public abstract class CustomTaskForceKeycardInfo : CustomKeycardInfo, IHolderCardInfo, ISerialLabelCardInfo
    {
        /// <inheritdoc />
        public override ItemType ItemType => ItemType.KeycardCustomTaskForce;

        /// <inheritdoc/>
        public abstract string SerialLabel { get; set; }

        /// <summary>
        /// Gets or sets rank index.
        /// </summary>
        public abstract int RankIndex { get; set; }

        /// <inheritdoc />
        public abstract string GetHolderName(Player player);

        /// <inheritdoc />
        public override KeycardItem GiveKeycard(Player player) => KeycardItem.CreateCustomKeycardTaskForce(
            player,
            ItemName,
            GetHolderName(player),
            KeycardPermissions,
            KeycardColor,
            PermissionsColor,
            SerialLabel,
            RankIndex)!;
    }
}