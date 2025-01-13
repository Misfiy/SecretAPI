namespace SecretAPI.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using SecretAPI.Interfaces;

    /// <summary>
    /// Extensions for Assembly.
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Registers all <see cref="IRegister"/>.
        /// </summary>
        /// <param name="assembly">The assembly to register from.</param>
        /// <returns>A collection of all registered.</returns>
        public static IEnumerable<IRegister> RegisterAllRegisters(this Assembly assembly)
        {
            List<IRegister> registers = new();

            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsAbstract || type.IsInterface)
                    continue;

                if (!typeof(IRegister).IsAssignableFrom(type))
                    continue;

                object obj = Activator.CreateInstance(type);
                if (obj is IRegister register && register.TryRegister())
                    registers.Add(register);
            }

            return registers;
        }
    }
}