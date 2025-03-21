namespace SecretAPI.Features
{
    using System;
    using System.Reflection;
    using HarmonyLib;
    using SecretAPI.Attribute;

    /// <summary>
    /// Handles patching.
    /// </summary>
    public static class GlobalPatcher
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="harmony"></param>
        /// <param name="assembly"></param>
        /// <param name="category"></param>
        public static void PatchAll(Harmony harmony, Assembly? assembly = null, string? category = null)
        {
            assembly ??= Assembly.GetEntryAssembly();

            foreach (Type type in assembly.GetTypes())
            {
                HarmonyPatchCategory categoryAttribute = type.GetCustomAttribute<HarmonyPatchCategory>();
                if (categoryAttribute != null || categoryAttribute?.Category == category)
                {
                    HarmonyPatchCondition patchCondition = type.GetCustomAttribute<HarmonyPatchCondition>();
                    if (patchCondition != null && !patchCondition.CheckCondition())
                    {
                        harmony.CreateClassProcessor(type).Patch();
                    }

                    continue;
                }

                foreach (MethodInfo method in type.GetMethods())
                {
                    HarmonyPatchCategory methodCategoryAttribute = method.GetCustomAttribute<HarmonyPatchCategory>();
                    HarmonyPatchCondition patchCondition = method.GetCustomAttribute<HarmonyPatchCondition>();

                    if (methodCategoryAttribute?.Category != category)
                        continue;

                    if (patchCondition == null || patchCondition.CheckCondition())
                    {
                        harmony.CreateProcessor(method.GetBaseDefinition()).Patch();
                    }
                }
            }
        }
    }
}