namespace SecretAPI.Features.CustomKeycards
{
    using Interactables.Interobjects.DoorUtils;

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
        public required string ItemName { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="KeycardPermissions"/> associated.
        /// </summary>
        public required KeycardLevels KeycardPermissions { get; set; }
    }
}