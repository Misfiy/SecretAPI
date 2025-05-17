namespace SecretAPI.Features.CustomKeycards
{
    using LabApi.Features.Wrappers;
    using UnityEngine;

    /// <summary>
    /// Handles info related to <see cref="ItemType.KeycardCustomMetalCase"/>.
    /// </summary>
    public abstract class CustomMetalKeycardInfo : CustomKeycardInfo
    {
        /// <inheritdoc />
        public override ItemType ItemType => ItemType.KeycardCustomMetalCase;

        /// <summary>
        /// Gets or sets the name of the holder of the card.
        /// </summary>
        public abstract string HolderName { get; set; }

        /// <summary>
        /// Gets or sets the label of the card.
        /// </summary>
        public abstract string CardLabel { get; set; }

        /// <summary>
        /// Gets or sets the color of the label.
        /// </summary>
        public abstract Color LabelColor { get; set; }

        /// <summary>
        /// Gets or sets the wear level.
        /// </summary>
        public abstract byte WearLevel { get; set; }

        /// <summary>
        /// Gets or sets the label of the serial.
        /// </summary>
        public abstract string SerialLabel { get; set; }

        /// <inheritdoc />
        public override KeycardItem GiveKeycard(Player player) => KeycardItem.CreateCustomKeycardMetal(
            player,
            ItemName,
            HolderName,
            CardLabel,
            KeycardPermissions,
            PermissionsColor,
            KeycardColor,
            LabelColor,
            WearLevel,
            SerialLabel)!;
    }
}