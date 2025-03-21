namespace SecretAPI.Features.Effects
{
    using PlayerRoles.FirstPersonControl;

    /// <summary>
    /// Effect that disables stamina usage.
    /// </summary>
    public class DisabledStaminaEffect : CustomPlayerEffect, IStaminaModifier
    {
        /// <inheritdoc />
        public bool StaminaModifierActive => IsEnabled;

        /// <inheritdoc />
        public float StaminaUsageMultiplier => 0;
    }
}