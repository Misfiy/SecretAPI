namespace SecretAPI.Attribute
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Defines the attribute for methods to call on load.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class CallOnLoadAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallOnLoadAttribute"/> class.
        /// </summary>
        /// <param name="priority">The priority of the load.</param>
        public CallOnLoadAttribute(int priority = 0)
        {
            Priority = priority;
        }

        /// <summary>
        /// Gets the priority of the loading.
        /// </summary>
        public int Priority { get; }

        /// <summary>
        /// Initializes and calls all <see cref="CallOnLoadAttribute"/>.
        /// </summary>
        /// <param name="assembly">The assembly to begin this on. Null will attempt to get calling, but may fail.</param>
        public static void Initialize(Assembly? assembly = null)
        {
            assembly ??= Assembly.GetCallingAssembly();

            const BindingFlags methodFlags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            Dictionary<CallOnLoadAttribute, MethodInfo> methods = new();

            // get all types
            foreach (Type type in assembly.GetTypes())
            {
                // get all static methods
                foreach (MethodInfo method in type.GetMethods(methodFlags))
                {
                    CallOnLoadAttribute? attribute = method.GetCustomAttribute<CallOnLoadAttribute>();
                    if (attribute == null)
                        continue;

                    methods.Add(attribute, method);
                }
            }

            foreach (KeyValuePair<CallOnLoadAttribute, MethodInfo> method in methods.OrderBy(static v => v.Key.Priority))
                method.Value.Invoke(null, null);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj) => obj == this;

        /// <inheritdoc/>
        public override int GetHashCode() => base.GetHashCode();
    }
}