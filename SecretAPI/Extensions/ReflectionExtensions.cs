namespace SecretAPI.Extensions
{
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
            foreach (PropertyInfo property in source.GetType().GetProperties())
            {
                destination.GetType().GetProperty(property.Name)?.SetValue(destination, property.GetValue(source));
            }
        }
    }
}