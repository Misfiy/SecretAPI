namespace SecretAPI.Features.Effects
{
    using LabApi.Features.Wrappers;
    using PlayerStatsSystem;

    /// <summary>
    /// Handles modifying a players attack damage.
    /// </summary>
    public interface IAttackDamageModiferEffect
    {
        /// <summary>
        /// Gets the modifier to apply to the damage.
        /// </summary>
        /// <param name="target">The target being damaged.</param>
        /// <param name="baseDamage">The current damage.</param>
        /// <param name="handler">The damagehandler.</param>
        /// <param name="hitboxType">The <see cref="HitboxType"/> being hit.</param>
        /// <returns>The attack multiplier.</returns>
        float GetAttackDamageModifier(Player target, float baseDamage, DamageHandlerBase handler, HitboxType hitboxType);
    }
}