namespace SecretAPI.Features.CustomKeycards
{
    using LabApi.Features.Wrappers;
    using UnityEngine;

    /// <summary>
    /// Handles info related to <see cref="ItemType.KeycardCustomMetalCase"/>.
    /// </summary>
    public abstract class CustomMetalKeycardInfo : CustomKeycardInfo, IHolderCardInfo, ILabelCardInfo, ISerialLabelCardInfo
    {
        /// <inheritdoc />
        public override ItemType ItemType => ItemType.KeycardCustomMetalCase;

        /// <inheritdoc />
        public abstract string CardLabel { get; set; }

        /// <inheritdoc />
        public abstract Color LabelColor { get; set; }

        /// <summary>
        /// Gets or sets the wear level.
        /// </summary>
        public abstract byte WearLevel { get; set; }

        /// <inheritdoc />
        public abstract string SerialLabel { get; set; }

        /// <inheritdoc />
        public abstract string GetHolderName(Player player);

        /// <inheritdoc />
        public override KeycardItem GiveKeycard(Player player) => KeycardItem.CreateCustomKeycardMetal(
            player,
            ItemName,
            GetHolderName(player),
            CardLabel,
            KeycardPermissions,
            PermissionsColor,
            KeycardColor,
            LabelColor,
            WearLevel,
            SerialLabel)!;
    }
}