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
        /// Attempts to call the correct command and gives a result.
        /// </summary>
        /// <param name="command">The command currently being called from.</param>
        /// <param name="sender">The sender of the command.</param>
        /// <param name="arguments">The arguments provided to the command.</param>
        /// <param name="response">The response to give to the player.</param>
        /// <returns>Whether the command was a success.</returns>
        public static bool TryCall(CustomCommand command, ICommandSender sender, ArraySegment<string> arguments, out string response)
        {
            CommandParseResult parseResult = TryParse(command, arguments);
            if (!parseResult.CouldParse)
            {
                response = parseResult.FailedResponse;
                return false;
            }
        }

        public static CommandParseResult TryParse(CustomCommand command, ArraySegment<string> arguments)
        {
            // IDK!!!
            if (arguments.Count < 1)
            {
                return new CommandParseResult()
                {
                    CouldParse = false,
                    FailedResponse = "Could not parse.",
                };
            }
        }
    }
}