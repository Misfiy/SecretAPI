namespace SecretAPI.Extensions
{
    using System.Diagnostics.CodeAnalysis;
    using InventorySystem;
    using InventorySystem.Configs;
    using PlayerRoles;
    using PlayerRoles.FirstPersonControl;
    using UnityEngine;

    /// <summary>
    /// Extensions related to <see cref="RoleTypeId"/>.
    /// </summary>
    public static class RoleExtensions
    {
        /// <summary>
        /// Tries to get a role base from a <see cref="RoleTypeId"/>.
        /// </summary>
        /// <param name="roleTypeId">The <see cref="RoleTypeId"/> to get base of.</param>
        /// <param name="role">The <see cref="PlayerRoleBase"/> found.</param>
        /// <typeparam name="T">The <see cref="PlayerRoleBase"/>.</typeparam>
        /// <returns>The role base found, else null. </returns>
        public static bool TryGetRoleBase<T>(this RoleTypeId roleTypeId, [NotNullWhen(true)] out T? role)
            => PlayerRoleLoader.TryGetRoleTemplate(roleTypeId, out role);

        /// <summary>
        /// Tries to get a random spawn point from a <see cref="RoleTypeId"/>.
        /// </summary>
        /// <param name="role">The role to get spawn from.</param>
        /// <param name="position">The position found.</param>
        /// <param name="horizontalRot">The rotation found.</param>
        /// <returns>Whether a spawnpoint was found.</returns>
        public static bool GetRandomSpawnPosition(this RoleTypeId role, out Vector3 position, out float horizontalRot)
        {
            if (TryGetRoleBase(role, out IFpcRole? fpc))
                return fpc.SpawnpointHandler.TryGetSpawnpoint(out position, out horizontalRot);

            position = Vector3.zero;
            horizontalRot = 0f;
            return false;
        }

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