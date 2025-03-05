namespace SecretAPI.Features.Modules
{
    using System.Collections.Generic;
    using LabApi.Features.Wrappers;
    using SecretAPI.Interfaces;

    /// <summary>
    /// Base class for <see cref="ICustomRole"/>.
    /// </summary>
    public abstract class CustomRole : ICustomRole
    {
        private List<Player> players = [];

        /// <inheritdoc />
        public abstract string Name { get; }

        /// <inheritdoc />
        public int Id { get; } = ++Registry<ICustomRole>.CurrentId;

        /// <summary>
        /// Gets the spawn handler of the role.
        /// </summary>
        public virtual IRoleSpawnHandler SpawnHandler { get; } = DefaultRoleSpawnHandler.Instance;

        /// <inheritdoc />
        public virtual void AddRole(Player player)
        {
            // TODO: logic
        }

        /// <inheritdoc />
        public virtual void RemoveRole(Player player)
        {
            // TODO: logic
        }

        /// <inheritdoc/>
        public virtual bool Check(Player player) => players.Contains(player);
    }
}