namespace SecretAPI.Features.Commands
{
    using System;
    using CommandSystem;

    /// <summary>
    /// Handles parsing <see cref="CustomCommand"/>.
    /// </summary>
    public static class CustomCommandHandler
    {
        /// <summary>
        /// Attempts to pass a command message and gives a result.
        /// </summary>
        /// <param name="sender">The sender of the command.</param>
        /// <param name="arguments">The arguments provided to the command.</param>
        /// <returns>The <see cref="CommandResult"/>.</returns>
        public static CommandResult TryCall(ICommandSender sender, ArraySegment<string> arguments)
        {
        }
    }
}