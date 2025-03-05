namespace SecretAPI.Features.Modules
{
    using SecretAPI.Interfaces;

    /// <summary>
    /// The default <see cref="IRoleSpawnHandler"/> to use in <see cref="CustomRole"/>.
    /// </summary>
    public class DefaultRoleSpawnHandler : IRoleSpawnHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultRoleSpawnHandler"/> class.
        /// </summary>
        public DefaultRoleSpawnHandler()
        {
        }

        /// <summary>
        /// Gets the current instance of the role spawner.
        /// </summary>
        public static DefaultRoleSpawnHandler Instance { get; } = new();
    }
}