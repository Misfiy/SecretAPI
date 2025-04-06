namespace SecretAPI.Features.Effects
{
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
    }
}