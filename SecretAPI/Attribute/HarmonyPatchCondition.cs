namespace SecretAPI.Attribute
{
    using System;

    /// <summary>
    /// Defines a harmony patch that requires a condition to patch.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class HarmonyPatchCondition : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HarmonyPatchCondition"/> class.
        /// </summary>
        /// <param name="checkCondition">The condition required for the patch.</param>
        public HarmonyPatchCondition(Func<bool> checkCondition)
        {
            CheckCondition = checkCondition;
        }

        /// <summary>
        /// Gets or sets the condition required to patch.
        /// </summary>
        public Func<bool> CheckCondition { get; protected set; }
    }
}