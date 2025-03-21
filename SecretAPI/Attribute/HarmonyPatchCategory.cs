namespace SecretAPI.Attribute
{
    using System;
    using SecretAPI.Features;

    /// <summary>
    /// Category handling for <see cref="GlobalPatcher"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class HarmonyPatchCategory : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HarmonyPatchCategory"/> class.
        /// </summary>
        /// <param name="category">The category of the patch.</param>
        public HarmonyPatchCategory(string? category)
        {
            Category = category;
        }

        /// <summary>
        /// Gets the patch category.
        /// </summary>
        public string? Category { get; }
    }
}