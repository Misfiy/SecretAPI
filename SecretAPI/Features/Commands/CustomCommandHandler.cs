namespace SecretAPI.Features.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
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
            const BindingFlags methodFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

            // IDK!!!
            if (arguments.Count < 1)
            {
                return new CommandParseResult()
                {
                    CouldParse = false,
                    FailedResponse = "Could not parse.",
                };
            }

            IEnumerable<MethodInfo> methods = command.GetType().GetMethods(methodFlags).Where(IsValidExecuteMethod);
            foreach (MethodInfo method in methods)
            {
                CommandParseResult result = ValidateAllMethodParameters(method, arguments);
                if (result.CouldParse)
                {
                    return new CommandParseResult()
                    {
                        CouldParse = true,
                        FailedResponse = string.Empty,
                    };
                }
            }
        }

        private static CommandParseResult ValidateAllMethodParameters(MethodInfo method, ArraySegment<string> arguments)
        {
            for (int index = 0; index < method.GetParameters().Length; index++)
            {
                ParameterInfo parameter = method.GetParameters()[index];
                CommandParseResult result = ValidateParameter(parameter, arguments.ElementAtOrDefault(index));
                if (!result.CouldParse)
                    return result;
            }
        }

        private static CommandParseResult ValidateParameter(ParameterInfo parameter, string? argument)
        {
            // if arg doesnt exist & param is optional, then its validated
            if (argument == null && parameter.IsOptional)
            {
                return new CommandParseResult()
                {
                    CouldParse = true,
                };
            }

            try
            {
                Type type = parameter.ParameterType;

                if (type.IsEnum)
                {
                    if (Enum.TryParse(type, argument, true, out object? enumValue))
                    {
                    }

                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool IsValidExecuteMethod(MethodInfo method)
        {
            // isnt an Execute command
            if (method.Name != "Execute")
                return false;

            ParameterInfo[] parameters = method.GetParameters();

            // params isnt 3, so its not default
            if (parameters.Length != 3)
                return true;

            // make sure params arent the types of the original default to prevent infinite loop
            return !(parameters[0].ParameterType == typeof(ArraySegment<string>)
                     && parameters[1].ParameterType == typeof(ICommandSender)
                     && parameters[2].IsOut
                     && parameters[2].ParameterType == typeof(string));
        }
    }
}