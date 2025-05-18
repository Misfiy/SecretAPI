namespace SecretAPI.Features.CustomKeycards
{
    using LabApi.Features.Wrappers;

    /// <summary>
    /// Interface for keycards with a Holder Name.
    /// </summary>
    public interface IHolderCardInfo
    {
        /// <summary>
        /// Gets a holder name based on a player.
        /// </summary>
        /// <param name="player">The player to get holder name based on.</param>
        /// <returns>The holder name to give.</returns>
        public string GetHolderName(Player player);
    }
}