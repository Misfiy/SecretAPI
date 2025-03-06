namespace SecretAPI.Extensions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;

    /// <summary>
    /// Extensions for reflection.
    /// </summary>
    public static class ReflectionExtensions
    {
        /// <summary>
        /// Attempts to cast an object to another type.
        /// </summary>
        /// <param name="obj">The object to attempt cast.</param>
        /// <param name="value">The value it was cast to. Null if returned false.</param>
        /// <typeparam name="T">The type to attempt casting to.</typeparam>
        /// <returns>Whether the obj is T.</returns>
        public static bool Is<T>(this object obj, [NotNullWhen(true)] out T? value)
        {
            value = default;

            if (obj is not T cast)
                return false;

            value = cast;
            return true;
        }

        /// <summary>
        /// Copies the properties.
        /// </summary>
        /// <param name="source">The source of the properties to copy.</param>
        /// <param name="destination">Where to copy to.</param>
        public static void CopyProperties(this object source, object destination)
        {
            Type destinationType = destination.GetType();
            foreach (PropertyInfo property in source.GetType().GetProperties())
            {
                destinationType.GetProperty(property.Name)?.SetValue(destination, property.GetValue(source));
            }
        }
    }
}