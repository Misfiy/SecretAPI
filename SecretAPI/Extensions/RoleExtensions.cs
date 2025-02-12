namespace SecretAPI.Extensions
{
    using InventorySystem;
    using InventorySystem.Configs;
    using PlayerRoles;

    /// <summary>
    /// Extensions related to <see cref="RoleTypeId"/>.
    /// </summary>
    public static class RoleExtensions
    {
        /// <summary>
        /// Gets the inventory of the specified <see cref="RoleTypeId"/>.
        /// </summary>
        /// <param name="role">The <see cref="RoleTypeId"/>.</param>
        /// <returns>The <see cref="InventoryRoleInfo"/> found.</returns>
        public static InventoryRoleInfo GetInventory(this RoleTypeId role)
            => StartingInventories.DefinedInventories.TryGetValue(role, out InventoryRoleInfo info)
                ? info
                : new InventoryRoleInfo([], []);
    }
}