namespace SecretAPI.Features.Effects
{
    using System;
    using CustomPlayerEffects;
    using LabApi.Events.Handlers;
    using PlayerRoles.FirstPersonControl;
    using UnityEngine;

    /// <summary>
    /// Handles an effect that affects gravity.
    /// </summary>
    [Obsolete("This will be removed in the future.")]
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
                Vector3 multi = FpcGravityController.DefaultGravity;
                bool isAffected = false;
                foreach (StatusEffectBase effectBase in ev.Player.ActiveEffects)
                {
                    if (effectBase is not IGravityEffect gravityEffect)
                        continue;

                    isAffected = true;
                    multi.x *= gravityEffect.GravityMultiplier.x;
                    multi.y *= gravityEffect.GravityMultiplier.y;
                    multi.z *= gravityEffect.GravityMultiplier.z;
                }

                if (isAffected)
                    ev.Player.Gravity = multi;
            };
        }
    }
}
