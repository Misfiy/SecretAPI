namespace SecretAPI.Features.CustomKeycards
{
    using LabApi.Features.Wrappers;
    using UnityEngine;

    /// <summary>
    /// Handles info related to <see cref="ItemType.KeycardCustomSite02"/>.
    /// </summary>
    public abstract class CustomSite02KeycardInfo : CustomKeycardInfo, IHolderCardInfo, ILabelCardInfo
    {
        /// <inheritdoc />
        public override ItemType ItemType => ItemType.KeycardCustomSite02;

        /// <inheritdoc />
        public abstract string CardLabel { get; set; }

        /// <inheritdoc />
        public abstract Color LabelColor { get; set; }

        /// <summary>
        /// Gets or sets the wear level.
        /// </summary>
        public abstract byte WearLevel { get; set; }

        /// <inheritdoc />
        public abstract string GetHolderName(Player player);

        /// <inheritdoc />
        public override KeycardItem GiveKeycard(Player player) => KeycardItem.CreateCustomKeycardSite02(
            player,
            ItemName,
            GetHolderName(player),
            CardLabel,
            KeycardPermissions,
            KeycardColor,
            PermissionsColor,
            LabelColor,
            WearLevel)!;
    }
}