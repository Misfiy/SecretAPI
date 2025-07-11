namespace SecretAPI.Features.Commands
{
    /// <summary>
    /// Gets the result of <see cref="CustomCommandHandler.TryParse"/>.
    /// </summary>
    public struct CommandResult
    {
        /// <summary>
        /// Gets a value indicating whether parsing was successful.
        /// </summary>
        public bool CouldParse;

        /// <summary>
        /// If parsing failed, will provide the fail reason, otherwise null.
        /// </summary>
        public string? FailedResponse;
    }
}