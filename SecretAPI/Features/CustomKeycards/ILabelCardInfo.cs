namespace SecretAPI.Features.CustomKeycards
{
    using UnityEngine;

    /// <summary>
    /// Interface for cards with a Label.
    /// </summary>
    public interface ILabelCardInfo
    {
        /// <summary>
        /// Gets or sets the label of the card.
        /// </summary>
        public string CardLabel { get; set; }

        /// <summary>
        /// Gets or sets the color of the label.
        /// </summary>
        public Color LabelColor { get; set; }
    }
}