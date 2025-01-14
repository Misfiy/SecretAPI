namespace SecretAPI.Interfaces
{
    using LabApi.Features.Wrappers;
    using PlayerRoles;

    /// <summary>
    /// Base class for how to handle custom role.
    /// </summary>
    public interface ICustomRole
    {
        /// <summary>
        /// Gets the name of the CustomRole.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the id of the CustomRole.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Gets the <see cref="RoleTypeId"/> the role will spawn as.
        /// </summary>
        public RoleTypeId Role { get; }

        /// <summary>
        /// Adds the role to a player.
        /// </summary>
        /// <param name="player">Player to add to.</param>
        public void AddRole(Player player);

        /// <summary>
        /// Removes the role from a player.
        /// </summary>
        /// <param name="player">Player to remove from.</param>
        public void RemoveRole(Player player);

        /// <summary>
        /// Checks whether a player is part of this role.
        /// </summary>
        /// <param name="player">The player to check.</param>
        /// <returns>True if they are, false otherwise.</returns>
        public bool Check(Player player);
    }
}