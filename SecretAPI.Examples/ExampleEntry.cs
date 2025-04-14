namespace SecretAPI.Examples
{
    using System;
    using LabApi.Loader.Features.Plugins;

    /// <summary>
    /// Defines the entry for the plugin.
    /// </summary>
    public class ExampleEntry : Plugin
    {
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
        }

        /// <inheritdoc/>
        public override void Disable()
        {
        }
    }
}