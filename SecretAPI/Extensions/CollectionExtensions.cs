namespace SecretAPI.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using LabApi.Features.Wrappers;
    using UnityEngine;

    /// <summary>
    /// Extensions for collections.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Gets a random value from the list.
        /// </summary>
        /// <param name="collection">The collection to pull from.</param>
        /// <typeparam name="T">The Type contained by the collection.</typeparam>
        /// <returns>A random value, default value when empty collection.</returns>
        public static T GetRandom<T>(this IEnumerable<T>? collection)
        {
            if (collection == null)
                return default!;

            T[] array = collection as T[] ?? collection.ToArray();
            return array[Random.Range(0, array.Length)];
        }
    }
}