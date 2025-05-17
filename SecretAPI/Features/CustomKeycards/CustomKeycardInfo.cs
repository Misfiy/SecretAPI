namespace SecretAPI.Features.CustomKeycards
{
    using Interactables.Interobjects.DoorUtils;

    /// <summary>
    /// Base class for handling information related to custom keycards.
    /// </summary>
    public abstract class CustomKeycardInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomKeycardInfo"/> class.
        /// </summary>
        /// <param name="itemName">The name to put as the item name.</param>
        /// <param name="keycardPermissions">The levels of the custom keycard.</param>
        protected CustomKeycardInfo(string itemName, KeycardLevels keycardPermissions)
        {
            ItemName = itemName;
            KeycardPermissions = keycardPermissions;
        }

        /// <summary>
        /// Gets the ItemType associated with the custom keycard.
        /// </summary>
        public abstract ItemType ItemType { get; }

        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="KeycardPermissions"/> associated.
        /// </summary>
        public KeycardLevels KeycardPermissions { get; set; }
    }
}