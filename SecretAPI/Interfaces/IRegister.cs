namespace SecretAPI.Interfaces
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Interface used to define a type that should auto register.
    /// </summary>
    public interface IRegister
    {
        /// <summary>
        /// Attempts to register the object.
        /// </summary>
        /// <returns>If it was successfully registered.</returns>
        public bool TryRegister();

        /// <summary>
        /// Registers all <see cref="IRegister"/>.
        /// </summary>
        /// <param name="assembly">The assembly to register from.</param>
        public static void RegisterAllRegisters(Assembly? assembly = null)
        {
            assembly ??= Assembly.GetCallingAssembly();

            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsAbstract || type.IsInterface)
                    continue;

                if (!typeof(IRegister).IsAssignableFrom(type))
                    continue;

                object obj = Activator.CreateInstance(type);
                if (obj is IRegister register)
                    register.TryRegister();
            }
        }
    }
}