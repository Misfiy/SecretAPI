namespace SecretAPI.Features
{
    using System.Collections.Generic;
    using Mirror;
    using PlayerRoles.Ragdolls;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    /// <summary>
    /// Manages prefabs and stores them.
    /// </summary>
    public static class PrefabManager
    {
        /// <summary>
        /// Gets the prefab for players.
        /// </summary>
        public static GameObject PlayerPrefab => NetworkManager.singleton.playerPrefab;

        /// <summary>
        /// Initializes the prefab manager.
        /// </summary>
        internal static void Initialize()
        {
            SceneManager.sceneLoaded += (_, _) =>
            {
                foreach (KeyValuePair<uint, GameObject> pair in NetworkClient.prefabs)
                    CheckGameObject(pair.Value);

                foreach (BasicRagdoll? pair in RagdollManager.AllRagdolls)
                    CheckGameObject(pair.gameObject);
            };
        }

        private static void CheckGameObject(GameObject gameObject)
        {
            // if (gameObject.TryGetComponent()))
        }
    }
}