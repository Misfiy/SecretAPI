namespace SecretAPI.Features.CustomKeycards
{
    using LabApi.Features.Wrappers;
    using UnityEngine;

    /// <summary>
    /// Handles info related to <see cref="ItemType.KeycardCustomManagement"/>.
    /// </summary>
    public abstract class CustomManagementKeycardInfo : CustomKeycardInfo
    {
        /// <inheritdoc />
        public override ItemType ItemType => ItemType.KeycardCustomManagement;

        /// <summary>
        /// Gets or sets the label of the card.
        /// </summary>
        public abstract string CardLabel { get; set; }

        /// <summary>
        /// Gets or sets the color of the label.
        /// </summary>
        public abstract Color LabelColor { get; set; }

        /// <inheritdoc />
        public override KeycardItem GiveKeycard(Player player) => KeycardItem.CreateCustomKeycardManagement(
                player,
                ItemName,
                CardLabel,
                KeycardPermissions,
                KeycardColor,
                PermissionsColor,
                LabelColor)!;
    }
}