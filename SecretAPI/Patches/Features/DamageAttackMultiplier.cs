namespace SecretAPI.Patches.Features
{
    using CustomPlayerEffects;
    using HarmonyLib;
    using LabApi.Features.Wrappers;
    using PlayerStatsSystem;
    using SecretAPI.Attribute;
    using SecretAPI.Features.Effects;

    /// <summary>
    /// Implements <see cref="IAttackDamageModiferEffect"/>.
    /// </summary>
    [HarmonyPatchCategory(nameof(CustomPlayerEffect))]
    [HarmonyPatch(typeof(AttackerDamageHandler), nameof(AttackerDamageHandler.ProcessDamage))]
    internal static class DamageAttackMultiplier
    {
#pragma warning disable SA1313
        private static void Postfix(AttackerDamageHandler __instance, ReferenceHub ply)
#pragma warning restore SA1313
        {
            Player victim = Player.Get(ply);
            Player attacker = Player.Get(__instance.Attacker.Hub);

            foreach (StatusEffectBase effect in attacker.ActiveEffects)
            {
                if (effect is IAttackDamageModiferEffect modifier)
                {
                    __instance.Damage *= modifier.GetAttackDamageModifier(victim, __instance.Damage, __instance, __instance.Hitbox);
                }
            }
        }
    }
}