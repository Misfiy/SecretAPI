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
        /// Gets an array of <see cref="ItemType"/> / <see cref="int"/>.
        /// </summary>
        public virtual object[] Items { get; } = [];

        /// <summary>
        /// Gets the spawn handler of the role.
        /// </summary>
        public virtual IRoleSpawnHandler? SpawnHandler { get; } = DefaultRoleSpawnHandler.Instance;

        /// <summary>
        /// Registers the CustomRole.
        /// </summary>
        public virtual void TryRegister()
        {
            Registry<ICustomRole>.Registered.Add(this);
            Init();
        }

        /// <inheritdoc />
        public virtual void AddRole(Player player)
        {
            players.Add(player);
            RoleAdded(player);
        }

        /// <inheritdoc />
        public virtual void RemoveRole(Player player)
        {
            if (!Check(player))
                return;

            players.Remove(player);
            RoleRemoved(player);
        }

        /// <inheritdoc/>
        public virtual bool Check(Player player) => players.Contains(player);

        /// <summary>
        /// Initializes the CustomRole.
        /// </summary>
        protected virtual void Init()
        {
            SpawnHandler?.AddRole(this);
        }

        /// <summary>
        /// Called when the role has been added to a player.
        /// </summary>
        /// <param name="player">The player.</param>
        protected virtual void RoleAdded(Player player)
        {
        }

        /// <summary>
        /// Called when the role has been removed from a player.
        /// </summary>
        /// <param name="player">The player.</param>
        protected virtual void RoleRemoved(Player player)
        {
        }
    }
}