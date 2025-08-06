namespace SecretAPI.Examples.Commands
{
    using LabApi.Features.Wrappers;
    using SecretAPI.Attribute;
    using SecretAPI.Features.Commands;

    /// <summary>
    /// An example of a <see cref="CustomCommand"/> that explodes a player.
    /// </summary>
    public class ExampleExplodeCommand : CustomCommand
    {
        /// <inheritdoc/>
        public override string Command { get; } = "explode";

        /// <inheritdoc/>
        public override string[] Aliases { get; } = [];

        /// <inheritdoc/>
        public override string Description { get; } = "Explodes a player";

        [ExecuteCommand]
        private void Run(Player sender, Player target)
        {
            TimedGrenadeProjectile.SpawnActive(target.Position, ItemType.GrenadeHE, sender);
        }
    }
}