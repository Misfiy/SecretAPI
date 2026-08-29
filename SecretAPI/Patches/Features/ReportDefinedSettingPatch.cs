namespace SecretAPI.Patches.Features;

using HarmonyLib;
using SecretAPI.Attributes;
using SecretAPI.Features.UserSettings;
using UserSettings.ServerSpecific;

/// <summary>
/// Logs issues with <see cref="ServerSpecificSettingsSync.DefinedSettings"/>.
/// </summary>
[HarmonyPatchCategory(nameof(CustomSetting))]
[HarmonyPatch(typeof(ServerSpecificSettingsSync), nameof(ServerSpecificSettingsSync.DefinedSettings), MethodType.Setter)]
internal static class ReportDefinedSettingPatch
{
    private static void Postfix(ref ServerSpecificSettingBase[] value)
    {
        value.ForEach(setting => CustomSetting.ReportSettingIssue(null, setting));
    }
}