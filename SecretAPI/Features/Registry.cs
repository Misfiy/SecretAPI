namespace SecretAPI.Features
{
    using System.Collections.Generic;

    /// <summary>
    /// Handles the registration of objects.
    /// </summary>
    /// <typeparam name="T">The type of the registry.</typeparam>
    public static class Registry<T>
    {
        /// <summary>
        /// Gets the registered objects.
        /// </summary>
        public static List<T> Registered { get; } = new();
    }
}