namespace SecretAPI
{
    using System;
    using LabApi.Loader.Features.Plugins;
    using LabApi.Loader.Features.Plugins.Enums;
    using SecretAPI.Features;

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
        public override Version Version => new(0, 0, 2);

        /// <inheritdoc/>
        public override Version RequiredApiVersion => new(1, 0, 0);

        /// <inheritdoc/>
        public override void Enable()
        {
            CustomPlayerEffect.Initialize();
        }

        /// <inheritdoc/>
        public override void Disable()
        {
        }
    }
}