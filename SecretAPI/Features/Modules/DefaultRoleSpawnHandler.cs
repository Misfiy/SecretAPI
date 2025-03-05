namespace SecretAPI.Features.Modules
{
    using System.Collections.Generic;
    using LabApi.Events.Arguments.PlayerEvents;
    using SecretAPI.Interfaces;

    /// <summary>
    /// The default <see cref="IRoleSpawnHandler"/> to use in <see cref="CustomRole"/>.
    /// </summary>
    public class DefaultRoleSpawnHandler : IRoleSpawnHandler
    {
        private List<ICustomRole> roles = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultRoleSpawnHandler"/> class.
        /// </summary>
        private DefaultRoleSpawnHandler()
        {
            LabApi.Events.Handlers.PlayerEvents.ChangedRole += OnChangedRole;
        }

        /// <summary>
        /// Gets the current instance of the role spawner.
        /// </summary>
        public static DefaultRoleSpawnHandler Instance { get; } = new();

        /// <inheritdoc />
        public void AddRole(ICustomRole role) => roles.Add(role);

        private void OnChangedRole(PlayerChangedRoleEventArgs args)
        {
        }
    }
}