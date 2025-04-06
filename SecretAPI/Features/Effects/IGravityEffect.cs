namespace SecretAPI.Features.Effects
{
    using CustomPlayerEffects;
    using LabApi.Events.Handlers;
    using UnityEngine;

    /// <summary>
    /// Handles an effect that affects gravity.
    /// </summary>
    public interface IGravityEffect
    {
        /// <summary>
        /// Gets the gravity multiplier.
        /// </summary>
        public Vector3 GravityMultiplier { get; }

        /// <summary>
        /// Initializes to properly handle the effect.
        /// </summary>
        internal static void Initialize()
        {
            PlayerEvents.UpdatedEffect += ev =>
            {
                Vector3 multi = Vector3.one;
                foreach (StatusEffectBase effectBase in ev.Player.ActiveEffects)
                {
                    if (effectBase is not IGravityEffect gravityEffect)
                        continue;

                    multi.x *= gravityEffect.GravityMultiplier.x;
                    multi.y *= gravityEffect.GravityMultiplier.y;
                    multi.z *= gravityEffect.GravityMultiplier.z;
                }

                ev.Player.Gravity = multi;
            };
        }
    }
}