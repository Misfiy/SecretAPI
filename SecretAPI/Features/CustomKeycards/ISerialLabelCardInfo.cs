namespace SecretAPI.Features.CustomKeycards
{
    /// <summary>
    /// Interface for cards with a Serial Label.
    /// </summary>
    public interface ISerialLabelCardInfo
    {
        /// <summary>
        /// Gets or sets the label of the serial.
        /// </summary>
        public string SerialLabel { get; set; }
    }
}