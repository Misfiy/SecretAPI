namespace SecretAPI.Examples
{
    using System;
    using HarmonyLib;
    using LabApi.Loader.Features.Plugins;
    using SecretAPI.Examples.Settings;
    using SecretAPI.Extensions;
    using SecretAPI.Features.UserSettings;

    /// <summary>
    /// Defines the entry for the plugin.
    /// </summary>
    public class ExampleEntry : Plugin
    {
        private Harmony? _harmony;

        /// <inheritdoc/>
        public override string Name { get; } = "SecretAPI.Example";

        /// <inheritdoc/>
        public override string Description { get; } = "An example plugin";

        /// <inheritdoc/>
        public override string Author { get; } = "@misfiy";

        /// <inheritdoc/>
        public override Version Version { get; } = typeof(SecretApi).Assembly.GetName().Version;

        /// <inheritdoc/>
        public override Version RequiredApiVersion { get; } = new(LabApi.Features.LabApiProperties.CompiledVersion);

        /// <inheritdoc/>
        public override void Enable()
        {
            CustomSetting.Register(new ExampleKeybindSetting());
            _harmony = new Harmony(nameof(ExampleEntry) + DateTime.Now.Ticks);
            _harmony.PatchAllNoCategory();
            _harmony.PatchCategory(nameof(ExampleEntry));
        }

        /// <inheritdoc/>
        public override void Disable()
        {
        }
    }
}