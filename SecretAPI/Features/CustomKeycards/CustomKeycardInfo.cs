namespace SecretAPI.Features.CustomKeycards
{
    using Interactables.Interobjects.DoorUtils;
    using LabApi.Features.Wrappers;
    using UnityEngine;

    /// <summary>
    /// Base class for handling information related to custom keycards.
    /// </summary>
    public abstract class CustomKeycardInfo
    {
        /// <summary>
        /// Gets the ItemType associated with the custom keycard.
        /// </summary>
        public abstract ItemType ItemType { get; }

        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        public abstract string ItemName { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="KeycardPermissions"/> associated.
        /// </summary>
        public abstract KeycardLevels KeycardPermissions { get; set; }

        /// <summary>
        /// Gets or sets the color of the keycard.
        /// </summary>
        public abstract Color KeycardColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the permissions on the card.
        /// </summary>
        public abstract Color PermissionsColor { get; set; }

        /// <summary>
        /// Creates and grants a keycard item based on the instance's information to a player.
        /// </summary>
        /// <param name="player">The player to give the keycard to.</param>
        /// <returns>The newly created keycard.</returns>
        public abstract KeycardItem GiveKeycard(Player player);
    }
}