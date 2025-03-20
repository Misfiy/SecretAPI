namespace SecretAPI.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using Random = UnityEngine.Random;

    /// <summary>
    /// Extensions for collections.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Gets a random value from the collection.
        /// </summary>
        /// <param name="collection">The collection to pull from.</param>
        /// <typeparam name="T">The Type contained by the collection.</typeparam>
        /// <returns>A random value, default value when empty collection.</returns>
        public static T GetRandomValue<T>(this IEnumerable<T> collection)
        {
            IList<T> list = collection as IList<T> ?? collection.ToList();
            return list[Random.Range(0, list.Count)];
        }
    }
}