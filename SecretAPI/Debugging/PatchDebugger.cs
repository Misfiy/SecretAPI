#if DEBUG

namespace SecretAPI.Debugging;

using SecretAPI.Attributes;

/// <summary>
/// Ensures all patches are loaded for debug purposes.
/// </summary>
internal static class PatchDebugger
{
    /// <summary>
    /// Loads the debug.
    /// </summary>
    [CallOnLoad]
    internal static void Load()
    {
        SecretApi.Harmony.PatchAll(SecretApi.Assembly);
    }
}

#endif