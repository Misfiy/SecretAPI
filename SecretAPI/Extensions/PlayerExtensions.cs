namespace SecretAPI.Extensions
{
    using Interactables.Interobjects.DoorUtils;
    using LabApi.Features.Wrappers;
    using SecretAPI.Enums;

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
        /// <param name="checkFlags">The <see cref="DoorPermissionCheck"/> to use for checking if a player has it.</param>
        /// <returns>Whether a valid permission was found.</returns>
        public static bool HasDoorPermission(this Player player, IDoorPermissionRequester requester, DoorPermissionCheck checkFlags = DoorPermissionCheck.Bypass | DoorPermissionCheck.Role | DoorPermissionCheck.CurrentItem)
        {
            if (checkFlags.HasFlag(DoorPermissionCheck.Bypass) && player.IsBypassEnabled)
                return true;

            if (checkFlags.HasFlag(DoorPermissionCheck.Role) && player.RoleBase is IDoorPermissionProvider roleProvider && requester.PermissionsPolicy.CheckPermissions(roleProvider.GetPermissions(requester)))
                return true;

            foreach (Item item in player.Items)
            {
                bool isCurrent = item == player.CurrentItem;
                if (!checkFlags.HasFlag(DoorPermissionCheck.CurrentItem) && isCurrent)
                    continue;

                if (!checkFlags.HasFlag(DoorPermissionCheck.InventoryExludingCurrent) && !isCurrent)
                    continue;

                if (item?.Base is IDoorPermissionProvider itemProvider && requester.PermissionsPolicy.CheckPermissions(itemProvider.GetPermissions(requester)))
                    return true;
            }

            return false;
        }
    }
}