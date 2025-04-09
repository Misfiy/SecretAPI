namespace SecretAPI.Features.Effects
{
    using UnityEngine;

    /// <summary>
    /// Increases the gravity of the player.
    /// </summary>
    public class GravityIncreaseEffect : CustomPlayerEffect, IGravityEffect
    {
        /// <inheritdoc />
        public Vector3 GravityMultiplier
        {
            get
            {
                float amount = 1 * (Intensity * 0.1f);
                return new Vector3(amount, amount, amount);
            }
        }
    }
}