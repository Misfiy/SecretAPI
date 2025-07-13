namespace SecretAPI.Features.Commands
{
    /// <summary>
    /// Gets the result of a <see cref="CustomCommandHandler.TryParse"/>.
    /// </summary>
    public struct CommandParseResult
    {
        /// <summary>
        /// Gets a value indicating whether parsing was successful.
        /// </summary>
        public bool CouldParse;

        /// <summary>
        /// If parsing failed, will provide the fail reason, otherwise null.
        /// </summary>
        public string FailedResponse;

        /// <summary>
        /// The argument for the argument.
        /// </summary>
        public object? ParamArgument;
    }
}