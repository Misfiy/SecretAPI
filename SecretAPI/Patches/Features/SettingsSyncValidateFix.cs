namespace SecretAPI.Patches.Features
{
    using System.Linq;
    using HarmonyLib;
    using SecretAPI.Features.UserSettings;
    using UserSettings.ServerSpecific;

    /// <summary>
    /// Fixes validation for <see cref="CustomSetting"/>.
    /// </summary>
    [HarmonyPatch(typeof(ServerSpecificSettingsSync), nameof(ServerSpecificSettingsSync.ServerPrevalidateClientResponse))]
    internal static class SettingsSyncValidateFix
    {
#pragma warning disable SA1313
        private static void Postfix(SSSClientResponse msg, ref bool __result)
#pragma warning restore SA1313
        {
            if (__result)
                return;

            CustomSetting? setting =
                CustomSetting.CustomSettings.
                    FirstOrDefault(s => s.Base.SettingId == msg.Id && s.Base.GetType() == msg.SettingType);

            __result = setting != null;
        }
    }
}