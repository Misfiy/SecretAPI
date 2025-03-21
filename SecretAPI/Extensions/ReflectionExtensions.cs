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