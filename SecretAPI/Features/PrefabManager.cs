namespace SecretAPI.Features
{
    using System.Collections.Generic;
    using AdminToys;
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
        /// Gets the prefab for <see cref="CapybaraToy"/>.
        /// </summary>
        public static CapybaraToy? CapybaraToy { get; private set; }

        /// <summary>
        /// Gets the prefab for <see cref="SpeakerToy"/>.
        /// </summary>
        public static SpeakerToy? SpeakerToy { get; private set; }

        /// <summary>
        /// Gets the prefab for <see cref="PrimitiveObjectToy"/>.
        /// </summary>
        public static PrimitiveObjectToy? PrimitiveObjectToy { get; private set; }

        /// <summary>
        /// Gets the prefab for <see cref="LightSourceToy"/>.
        /// </summary>
        public static LightSourceToy? LightSourceToy { get; private set; }

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
            if (gameObject.TryGetComponent(out CapybaraToy capybaraToy))
            {
                CapybaraToy = capybaraToy;
                return;
            }

            if (gameObject.TryGetComponent(out SpeakerToy speakerToy))
            {
                SpeakerToy = speakerToy;
                return;
            }

            if (gameObject.TryGetComponent(out PrimitiveObjectToy primitiveObjectToy))
            {
                PrimitiveObjectToy = primitiveObjectToy;
                return;
            }

            if (gameObject.TryGetComponent(out LightSourceToy lightSourceToy))
            {
                LightSourceToy = lightSourceToy;
                return;
            }
        }
    }
}