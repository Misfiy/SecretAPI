namespace SecretAPI.Features
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CustomPlayerEffects;
    using LabApi.Features.Wrappers;
    using LiteNetLib.Utils;
    using MEC;
    using Mirror;
    using UnityEngine;

    /// <summary>
    /// Handles custom player effects.
    /// <remarks>Must register to <see cref="EffectsToRegister"/> to work.</remarks>
    /// </summary>
    public abstract class CustomPlayerEffect : StatusEffectBase
    {
        /// <summary>
        /// Gets a list of types to register (Must inherit <see cref="StatusEffectBase"/>).
        /// </summary>
        public static List<Type> EffectsToRegister { get; } = [];

        /// <summary>
        /// Gets the <see cref="Player"/> with this effect.
        /// </summary>
        public Player Owner { get; private set; } = null!;

        /// <inheritdoc/>
        public override void Start()
        {
            Owner = Player.Get(Hub);
            base.Start();
        }

        /// <summary>
        /// Creates objects.
        /// </summary>
        /// <returns>Coroutine thingy.</returns>
        /// <exception cref="InvalidTypeException">Type was not StatusEffectBase.</exception>
        internal static IEnumerator<float> CreateObjects()
        {
            yield return Timing.WaitUntilFalse(NetworkClient.prefabs.IsEmpty);

            foreach (Type type in EffectsToRegister)
            {
                if (!typeof(StatusEffectBase).IsAssignableFrom(type))
                    throw new InvalidTypeException($"[CustomPlayerEffect.CreateObjects] {type.FullName} is not a valid StatusEffectBase");

                GameObject playerPrefab = NetworkClient.prefabs.FirstOrDefault(p => p.Value.name.Contains("Player")).Value;
                Transform playerEffects = playerPrefab.transform.Find("PlayerEffects");

                _ = new GameObject(type.Name, type)
                {
                    transform =
                    {
                        parent = playerEffects,
                    },
                };
            }
        }
    }
}