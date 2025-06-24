namespace SecretAPI.Extensions
{
    using CustomPlayerEffects;
    using Interactables.Interobjects.DoorUtils;
    using InventorySystem;
    using InventorySystem.Items;
    using InventorySystem.Items.Usables.Scp330;
    using LabApi.Features.Wrappers;
    using SecretAPI.Enums;

    /// <summary>
    /// Extensions related to the player.
    /// </summary>
    public static class PlayerExtensions
    {
        /// <summary>
        /// Gets an effect of a player based on the effect name.
        /// </summary>
        /// <param name="player">The player to get effect from.</param>
        /// <param name="name">Name of the effect to find.</param>
        /// <returns>The effect.</returns>
        public static StatusEffectBase GetEffect(this Player player, string name)
            => player.ReferenceHub.playerEffectsController.TryGetEffect(name, out StatusEffectBase? effect) ? effect : null!;

        /// <summary>
        /// Gives a random candy to a player.
        /// </summary>
        /// <param name="player">The player to give candy to.</param>
        /// <param name="reason">The reason to give the candy.</param>
        public static void GiveRandomCandy(this Player player, ItemAddReason reason)
            => GiveCandy(player, Scp330Candies.GetRandom(), reason);

        /// <summary>
        /// Gives a candy to a player.
        /// </summary>
        /// <param name="player">The player to give candy to.</param>
        /// <param name="candy">The candy to give.</param>
        /// <param name="reason">The reason to give candy.</param>
        public static void GiveCandy(this Player player, CandyKindID candy, ItemAddReason reason)
            => player.ReferenceHub.GrantCandy(candy, reason);

        /// <summary>
        /// Checks if a player has the permission.
        /// </summary>
        /// <param name="player">The player to check.</param>
        /// <param name="permission">The permission to check.</param>
        /// <returns>If player has permission.</returns>
        public static bool HasGamePermission(this Player player, PlayerPermissions permission)
        {
            PlayerPermissions currentPerms = (PlayerPermissions)player.ReferenceHub.serverRoles.Permissions;
            return currentPerms.HasFlag(permission);
        }

        /// <summary>
        /// Checks whether a player has permission to access a <see cref="IDoorPermissionRequester"/>.
        /// </summary>
        /// <param name="player">The player to check.</param>
        /// <param name="requester">The requester to check for permissions.</param>
        /// <param name="checkFlags">The <see cref="DoorPermissionCheck"/> to use for checking if a player has it.</param>
        /// <returns>Whether a valid permission was found.</returns>
        public static bool HasDoorPermission(this Player player, IDoorPermissionRequester requester, DoorPermissionCheck checkFlags = DoorPermissionCheck.Default)
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

                if (!checkFlags.HasFlag(DoorPermissionCheck.InventoryExcludingCurrent) && !isCurrent)
                    continue;

                if (item.Base is IDoorPermissionProvider itemProvider && requester.PermissionsPolicy.CheckPermissions(itemProvider.GetPermissions(requester)))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Checks whether a player has permission to access a <see cref="Door"/>.
        /// </summary>
        /// <param name="player">The player to check.</param>
        /// <param name="door">The door to check for permissions.</param>
        /// <param name="checkFlags">The <see cref="DoorPermissionCheck"/> to use for checking if a player has it.</param>
        /// <returns>Whether a valid permission was found.</returns>
        public static bool HasDoorPermission(this Player player, Door door, DoorPermissionCheck checkFlags = DoorPermissionCheck.Default)
            => player.HasDoorPermission(door.Base, checkFlags);

        /// <summary>
        /// Checks whether a player has permission to access a <see cref="LockerChamber"/>.
        /// </summary>
        /// <param name="player">The player to check.</param>
        /// <param name="chamber">The locker chamber to check for permissions.</param>
        /// <param name="checkFlags">The <see cref="DoorPermissionCheck"/> to use for checking if a player has it.</param>
        /// <returns>Whether a valid permission was found.</returns>
        public static bool HasLockerChamberPermission(this Player player, LockerChamber chamber, DoorPermissionCheck checkFlags = DoorPermissionCheck.Default)
            => player.HasDoorPermission(chamber.Base, checkFlags);

        /// <summary>
        /// Checks whether a player has permission to access a <see cref="Generator"/>.
        /// </summary>
        /// <param name="player">The player to check.</param>
        /// <param name="generator">The generator to check for permissions.</param>
        /// <param name="checkFlags">The <see cref="DoorPermissionCheck"/> to use for checking if a player has it.</param>
        /// <returns>Whether a valid permission was found.</returns>
        public static bool HasGeneratorPermission(this Player player, Generator generator, DoorPermissionCheck checkFlags = DoorPermissionCheck.Default)
            => player.HasDoorPermission(generator.Base, checkFlags);
    }
}