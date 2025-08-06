namespace SecretAPI.Examples.Cards
{
    using Interactables.Interobjects.DoorUtils;
    using SecretAPI.Features.CustomKeycards;
    using UnityEngine;

    /// <summary>
    /// Example for <see cref="CustomManagementKeycardInfo"/>.
    /// </summary>
    public class ExampleCustomCard : CustomManagementKeycardInfo
    {
        /// <inheritdoc/>
        public override string ItemName { get; set; } = "Example Custom Card";

        /// <inheritdoc/>
        public override KeycardLevels KeycardPermissions { get; set; } = new(0, 0, 0);

        /// <inheritdoc/>
        public override Color KeycardColor { get; set; } = Color.white;

        /// <inheritdoc/>
        public override Color PermissionsColor { get; set; } = Color.white;

        /// <inheritdoc/>
        public override string CardLabel { get; set; } = "Example Label";

        /// <inheritdoc/>
        public override Color LabelColor { get; set; } = Color.white;
    }
}