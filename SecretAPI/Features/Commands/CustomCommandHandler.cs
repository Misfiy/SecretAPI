namespace SecretAPI.Features.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using CommandSystem;
    using LabApi.Features.Wrappers;
    using SecretAPI.Attribute;

    /// <summary>
    /// Handles parsing <see cref="CustomCommand"/>.
    /// </summary>
    public static class CustomCommandHandler
    {
        /// <summary>
        /// 
        /// </summary>
        public const string SelfPlayerName = "self";

        private static Dictionary<CustomCommand, MethodInfo[]> commandExecuteMethods = new();

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
            Player senderPlayer = Player.Get(sender) ?? Server.Host!;

            CommandResult parseResult = TryParse(command, senderPlayer, arguments);
            if (!parseResult.CouldParse)
            {
                response = parseResult.FailedResponse;
                return false;
            }

            parseResult.Method.Invoke(null, parseResult.ProvidedArguments);

            // TODO: get result & put it into response
            return true;
        }

        private static CommandResult TryParse(CustomCommand command, Player sender, ArraySegment<string> arguments)
        {
            foreach (MethodInfo method in GetMethods(command))
            {
                CommandParseResult result = ValidateAllMethodParameters(method, sender, arguments);
                if (result.CouldParse)
                {
                    return new CommandResult()
                    {
                        CouldParse = true,
                        FailedResponse = string.Empty,
                    };
                }
            }
        }

        private static CommandParseResult ValidateAllMethodParameters(MethodInfo method, Player sender, ArraySegment<string> arguments)
        {
            for (int index = 0; index < method.GetParameters().Length; index++)
            {
                ParameterInfo parameter = method.GetParameters()[index];
                CommandParseResult result = ValidateParameter(parameter, sender, arguments.ElementAtOrDefault(index));
                if (!result.CouldParse)
                    return result;
            }
        }

        private static CommandParseResult ValidateParameter(ParameterInfo parameter, Player sender, string? argument)
        {
            // if arg doesnt exist & param is optional, then its validated
            if (argument == null && parameter.IsOptional)
            {
                return new CommandParseResult()
                {
                    CouldParse = true,
                    ParamArgument = parameter.DefaultValue,
                };
            }

            Type type = parameter.ParameterType;

            if (type.IsEnum)
            {
                if (Enum.TryParse(type, argument, true, out object? enumValue))
                {
                    return new CommandParseResult()
                    {
                        CouldParse = true,
                        ParamArgument = enumValue,
                    };
                }

                return new CommandParseResult()
                {
                    CouldParse = false,
                    FailedResponse = $"Could not pass into valid enum value. Enum required: {type.Name}.",
                };
            }

            if (parameter.Name == SelfPlayerName)
            {
                if (typeof(Player).IsAssignableFrom(parameter.ParameterType))
                {
                    return new CommandParseResult()
                    {
                        CouldParse = true,
                        ParamArgument = sender,
                    };
                }
                else if (typeof(ReferenceHub).IsAssignableFrom(parameter.ParameterType))
                {
                    return new CommandParseResult()
                    {
                        CouldParse = true,
                        ParamArgument = sender.ReferenceHub,
                    };
                }
            }

            return true;
        }

        private static MethodInfo[] GetMethods(CustomCommand command)
        {
            const BindingFlags methodFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

            if (!commandExecuteMethods.TryGetValue(command, out MethodInfo[] methods))
            {
                methods = command.GetType().GetMethods(methodFlags).Where(m => m.GetCustomAttribute<ExecuteCommandAttribute>() != null).ToArray();
                commandExecuteMethods.Add(command, methods);
            }

            return methods;
        }
    }
}