namespace SecretAPI.Examples.Patches
{
    using HarmonyLib;
    using SecretAPI.Attribute;

    /// <summary>
    /// An example harmony patch.
    /// </summary>
    [HarmonyPatchCategory(nameof(ExampleEntry))]
    /*[HarmonyPatch]*/
    public static class ExamplePatch
    {
        private static bool Prefix()
        {
            return false;
        }

        private static void Postfix()
        {
        }
    }
}