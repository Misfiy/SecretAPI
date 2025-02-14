﻿namespace SecretAPI.Features
{
    using System;
    using System.Collections.Generic;
    using CustomPlayerEffects;
    using LabApi.Features.Wrappers;
    using Mirror;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using Logger = LabApi.Features.Console.Logger;

    /// <summary>
    /// Handles custom player effects.
    /// <remarks>Must register to <see cref="EffectsToRegister"/> to work.</remarks>
    /// </summary>
    public abstract class CustomPlayerEffect : StatusEffectBase
    {
        private static bool isLoaded;

        /// <summary>
        /// Gets a list of types to register (Must inherit <see cref="StatusEffectBase"/>).
        /// <remarks>Must be <see cref="Type"/>, can be gotten through e.g. typeof(Scp207).</remarks>
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

        /// <inheritdoc/>
        public override string ToString() => $"{GetType().FullName}: Owner ({Owner}) - Intensity ({Intensity}) - Duration {Duration}";

        /// <summary>
        /// Initializes the <see cref="CustomPlayerEffect"/> to implement <see cref="EffectsToRegister"/>.
        /// </summary>
        internal static void Initialize()
        {
            SceneManager.sceneLoaded += (_, _) =>
            {
                if (isLoaded)
                    return;

                isLoaded = true;

                Transform playerEffects = NetworkManager.singleton.playerPrefab.GetComponent<PlayerEffectsController>().transform;
                foreach (Type type in EffectsToRegister)
                {
                    if (!typeof(StatusEffectBase).IsAssignableFrom(type))
                    {
                        Logger.Error($"[CustomPlayerEffect.CreateObjects] {type.FullName} is not a valid StatusEffectBase");
                        continue;
                    }

                    // register effect prefab, required
                    new GameObject(type.Name, type).transform.parent = playerEffects;
                }
            };
        }
    }
}