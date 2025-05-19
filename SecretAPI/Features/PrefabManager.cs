namespace SecretAPI.Features
{
    using Interactables.Interobjects;
    using MapGeneration;
    using Mirror;
    using UnityEngine;

    /// <summary>
    /// Manages prefabs that don't work properly in <see cref="PrefabStore{TPrefab}"/>.
    /// </summary>
    public static class PrefabManager
    {
        private static bool isSet;

        private static ReferenceHub? playerPrefab;
        private static BasicDoor? lczDoor;
        private static BasicDoor? hczDoor;
        private static BasicDoor? hczBulkDoor;
        private static BasicDoor? ezDoor;

        /// <summary>
        /// Gets the <see cref="ReferenceHub"/> prefab.
        /// </summary>
        public static ReferenceHub PlayerPrefab
            => playerPrefab ??= NetworkManager.singleton.playerPrefab.GetComponent<ReferenceHub>();

        /// <summary>
        /// Gets the <see cref="BasicDoor"/> found in <see cref="FacilityZone.LightContainment"/>.
        /// </summary>
        public static BasicDoor LczDoor
        {
            get
            {
                SetDoors();
                return lczDoor!;
            }
        }

        /// <summary>
        /// Gets the <see cref="BasicDoor"/> found in <see cref="FacilityZone.HeavyContainment"/>.
        /// </summary>
        public static BasicDoor HczDoor
        {
            get
            {
                SetDoors();
                return hczDoor!;
            }
        }

        /// <summary>
        /// Gets the <see cref="BasicDoor"/> found in <see cref="FacilityZone.HeavyContainment"/>.
        /// </summary>
        public static BasicDoor HczBulkDoor
        {
            get
            {
                SetDoors();
                return hczBulkDoor!;
            }
        }

        /// <summary>
        /// Gets the <see cref="BasicDoor"/> found in <see cref="FacilityZone.Entrance"/>.
        /// </summary>
        public static BasicDoor EzDoor
        {
            get
            {
                SetDoors();
                return ezDoor!;
            }
        }

        private static void SetDoors()
        {
            if (isSet)
                return;

            foreach (GameObject gameObject in NetworkClient.prefabs.Values)
            {
                if (!gameObject.TryGetComponent(out BasicDoor door))
                    continue;

                switch (door.name)
                {
                    case "LCZ BreakableDoor":
                        lczDoor = door;
                        break;
                    case "HCZ BreakableDoor":
                        hczDoor = door;
                        break;
                    case "HCZ BulkDoor":
                        hczBulkDoor = door;
                        break;
                    case "EZ BreakableDoor":
                        ezDoor = door;
                        break;
                }
            }

            isSet = true;
        }
    }
}