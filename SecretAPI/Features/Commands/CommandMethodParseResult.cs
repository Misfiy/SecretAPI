namespace SecretAPI.Features.Commands
{
    /// <summary>
    /// Defines the return type of <see cref="CustomCommandHandler.ValidateAllMethodParameters"/>.
    /// </summary>
    internal struct CommandMethodParseResult
    {
#pragma warning disable SA1600 // Elements should be documented
        internal bool CouldParse;

        internal string FailedResponse;

        internal object[]? Arguments;
    }
}