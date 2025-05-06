namespace SecretAPI.Extensions
{
    using Interactables.Interobjects.DoorUtils;
    using LabApi.Features.Wrappers;

    /// <summary>
    /// Extensions related to the player.
    /// </summary>
    public static class PlayerExtensions
    {
        /// <summary>
        /// Checks if a player has the permission.
        /// </summary>
        /// <param name="player">The player to check.</param>
        /// <param name="permission">The permission to check.</param>
        /// <returns>If player has permission.</returns>
        public static bool HasGamePermission(this Player player, PlayerPermissions permission)
        {
            if (player.UserGroup == null)
                return false;

            PlayerPermissions currentPerms = (PlayerPermissions)player.UserGroup.Permissions;
            return currentPerms.HasFlag(permission);
        }

        /// <summary>
        /// Checks whether a player has permission to access a <see cref="IDoorPermissionRequester"/>.
        /// </summary>
        /// <param name="player">The player to check.</param>
        /// <param name="requester">The requester to check the player for permissions on.</param>
        /// <returns>Whether a valid permission was found.</returns>
        public static bool HasDoorPermission(this Player player, IDoorPermissionRequester requester)
        {
            if (player.IsBypassEnabled)
                return true;

            if (player.RoleBase is IDoorPermissionProvider roleProvider && requester.PermissionsPolicy.CheckPermissions(roleProvider.GetPermissions(requester)))
                return true;

            foreach (Item? item in player.Items)
            {
                if (item?.Base is IDoorPermissionProvider itemProvider && requester.PermissionsPolicy.CheckPermissions(itemProvider.GetPermissions(requester)))
                    return true;
            }

            return false;
        }
    }
}