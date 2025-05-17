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

        /// <summary>
        /// Gets or sets the name of the holder of the card.
        /// </summary>
        public abstract string HolderName { get; set; }

        /// <summary>
        /// Gets or sets the label of the serial.
        /// </summary>
        public abstract string SerialLabel { get; set; }

        /// <summary>
        /// Gets or sets rank index.
        /// </summary>
        public abstract int RankIndex { get; set; }

        /// <inheritdoc />
        public override KeycardItem GiveKeycard(Player player) => KeycardItem.CreateCustomKeycardTaskForce(
            player,
            ItemName,
            HolderName,
            KeycardPermissions,
            KeycardColor,
            PermissionsColor,
            SerialLabel,
            RankIndex)!;
    }
}