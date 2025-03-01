namespace SecretAPI
{
    using System;
    using HarmonyLib;
    using LabApi.Loader.Features.Plugins;
    using LabApi.Loader.Features.Plugins.Enums;
    using SecretAPI.Features.Effects;

    /// <summary>
    /// Main class handling loading API.
    /// </summary>
    public class SecretApi : Plugin
    {
        private Harmony? harmony;

        /// <inheritdoc/>
        public override string Name => "SecretAPI";

        /// <inheritdoc/>
        public override string Description => "API for SCP:SL";

        /// <inheritdoc/>
        public override string Author => "@misfiy";

        /// <inheritdoc/>
        public override LoadPriority Priority => LoadPriority.Highest;

        /// <inheritdoc/>
        public override Version Version { get; } = typeof(SecretApi).Assembly.GetName().Version;

        /// <inheritdoc/>
        public override Version RequiredApiVersion => new(1, 0, 0);

        /// <inheritdoc/>
        public override void Enable()
        {
            harmony = new Harmony(nameof(SecretAPI) + DateTime.Now.Ticks);
            harmony.PatchAll();
            CustomPlayerEffect.Initialize();
        }

        /// <inheritdoc/>
        public override void Disable()
        {
            harmony?.UnpatchAll(harmony.Id);
        }
    }
}