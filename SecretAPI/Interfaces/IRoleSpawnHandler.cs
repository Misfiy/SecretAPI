namespace SecretAPI.Interfaces
{
    /// <summary>
    /// Defines the spawn logic handler for <see cref="ICustomRole"/>.
    /// </summary>
    public interface IRoleSpawnHandler
    {
        /// <summary>
        /// Adds a customrole to the spawnhandling.
        /// </summary>
        /// <param name="role">The role to add.</param>
        public void AddRole(ICustomRole role);
    }
}