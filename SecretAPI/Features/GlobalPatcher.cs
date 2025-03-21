namespace SecretAPI.Features
{
    using System;
    using System.Reflection;
    using HarmonyLib;
    using LabApi.Features.Console;
    using SecretAPI.Attribute;

    /// <summary>
    /// Handles patching.
    /// </summary>
    public static class GlobalPatcher
    {
        /// <summary>
        /// Patches all.
        /// </summary>
        /// <param name="harmonyId">The harmony id to use for the patch.</param>
        /// <param name="assembly">The assembly to look for patches.</param>
        /// <param name="category">The category to patch. Null for all non categorized.</param>
        public static void PatchAll(string harmonyId, Assembly? assembly = null, string? category = null)
        {
            PatchAll(new Harmony(harmonyId), assembly ?? Assembly.GetCallingAssembly(), category);
        }

        /// <summary>
        /// Patches all.
        /// </summary>
        /// <param name="harmony">The harmony to use for the patch.</param>
        /// <param name="assembly">The assembly to look for patches.</param>
        /// <param name="category">The category to patch. Null for all non categorized.</param>
        public static void PatchAll(Harmony harmony, Assembly? assembly = null, string? category = null)
        {
            assembly ??= Assembly.GetCallingAssembly();

            try
            {
                foreach (Type type in assembly.GetTypes())
                {
                    HarmonyPatchCategory categoryAttribute = type.GetCustomAttribute<HarmonyPatchCategory>();
                    if ((category == null && categoryAttribute == null) || categoryAttribute.Category == category)
                    {
                        harmony.CreateClassProcessor(type).Patch();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
            }
        }
    }
}