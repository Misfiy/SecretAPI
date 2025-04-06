namespace SecretAPI.Patches.Features
{
    using CustomPlayerEffects;
    using HarmonyLib;
    using PlayerRoles.FirstPersonControl;
    using SecretAPI.Attribute;
    using SecretAPI.Features.Effects;
    using UnityEngine;

    /// <summary>
    /// Implements <see cref="IGravityEffect"/>.
    /// </summary>
    [HarmonyPatchCategory(nameof(CustomPlayerEffect))]
    [HarmonyPatch(typeof(StatusEffectBase), nameof(StatusEffectBase.IntensityChanged))]
    internal static class GravityEffectMultiplier
    {
#pragma warning disable SA1313
        private static void Postfix(StatusEffectBase __instance)
#pragma warning restore SA1313
        {
            Vector3 multi = Vector3.one;
            foreach (StatusEffectBase effectBase in __instance.Hub.playerEffectsController.AllEffects)
            {
                if (!effectBase.IsEnabled || effectBase is not IGravityEffect gravityEffect)
                    continue;

                multi.x *= gravityEffect.GravityMultiplier.x;
                multi.y *= gravityEffect.GravityMultiplier.y;
                multi.z *= gravityEffect.GravityMultiplier.z;
            }

            if (__instance.Hub.roleManager.CurrentRole is IFpcRole currentRole)
                currentRole.FpcModule.Motor.GravityController.Gravity = multi;
        }
    }
}