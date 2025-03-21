namespace SecretAPI.Features.Effects
{
    using PlayerRoles.FirstPersonControl;

    /// <summary>
    /// Effect that disables sprinting for a player.
    /// </summary>
    public class SprintDisablerEffect : CustomPlayerEffect, IStaminaModifier
    {
        /// <inheritdoc />
        public bool StaminaModifierActive => IsEnabled;

        /// <inheritdoc />
        public bool SprintingDisabled => true;
    }
}