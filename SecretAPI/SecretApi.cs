namespace SecretAPI
{
    using System;
    using System.Reflection;
    using HarmonyLib;
    using LabApi.Loader.Features.Plugins;
    using LabApi.Loader.Features.Plugins.Enums;
    using SecretAPI.Extensions;
    using SecretAPI.Features;
    using SecretAPI.Features.Effects;

    /// <summary>
    /// Main class handling loading API.
    /// </summary>
    public class SecretApi : Plugin
    {
        /// <inheritdoc/>
        public override string Name => "SecretAPI";

        /// <inheritdoc/>
        public override string Description => "API for SCP:SL";

        /// <inheritdoc/>
        public override string Author => "@misfiy";

        /// <inheritdoc/>
        public override LoadPriority Priority => LoadPriority.Highest;

        /// <inheritdoc/>
        public override Version Version { get; } = Assembly.GetName().Version;

        /// <inheritdoc/>
        public override Version RequiredApiVersion { get; } = new(LabApi.Features.LabApiProperties.CompiledVersion);

        /// <summary>
        /// Gets the harmony to use for the API.
        /// </summary>
        internal static Harmony? Harmony { get; private set; }

        /// <summary>
        /// Gets the Assembly of the API.
        /// </summary>
        internal static Assembly Assembly { get; } = typeof(SecretApi).Assembly;

        /// <inheritdoc/>
        public override void Enable()
        {
            Harmony = new Harmony("SecretAPI" + DateTime.Now);
            Harmony.PatchAllNoCategory(Assembly);
            PrefabManager.Initialize();
            CustomPlayerEffect.Initialize();
        }

        /// <inheritdoc/>
        public override void Disable()
        {
            Harmony?.UnpatchAll(Harmony.Id);
        }
    }
}