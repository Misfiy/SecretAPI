namespace SecretAPI.Features
{
    using System.Linq;
    using Mirror;
    using PlayerRoles.Ragdolls;
    using UnityEngine;

    /// <summary>
    /// Handles the storing of a prefab.
    /// </summary>
    /// <typeparam name="TPrefab">The prefab to use.</typeparam>
    public static class PrefabStore<TPrefab>
        where TPrefab : MonoBehaviour
    {
        private static TPrefab? savedPrefab;

        /// <summary>
        /// Gets the prefab associated.
        /// </summary>
        public static TPrefab Prefab
        {
            get
            {
                if (savedPrefab)
                    return savedPrefab;

                foreach (GameObject gameObject in NetworkClient.prefabs.Values.Concat(RagdollManager.AllRagdollPrefabs.Select(r => r.gameObject)))
                {
                    if (gameObject.TryGetComponent(out savedPrefab))
                        return savedPrefab!;
                }

                return null!;
            }
        }
    }
}