namespace SecretAPI.Features.Commands
{
    using System;
    using CommandSystem;

    /// <summary>
    /// Defines the base of a custom <see cref="ICommand"/>.
    /// </summary>
    public abstract class CustomCommand : ICommand
    {
        /// <inheritdoc />
        public abstract string Command { get; }

        /// <inheritdoc />
        public abstract string[] Aliases { get; }

        /// <inheritdoc />
        public abstract string Description { get; }

        /// <inheritdoc />
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
            => CustomCommandHandler.TryCall(this, sender, arguments, out response);
    }
}