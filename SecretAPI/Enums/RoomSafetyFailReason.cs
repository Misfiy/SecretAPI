namespace SecretAPI.Enums
{
    using System;
    using MapGeneration;

    /// <summary>
    /// Reasons why room safety should fail.
    /// </summary>
    [Flags]
    public enum RoomSafetyFailReason
    {
        /// <summary>
        /// No fail.
        /// </summary>
        None = 0,

        /// <summary>
        /// Room safety check will fail if warhead has gone off and this is not part of <see cref="FacilityZone.Surface"/>.
        /// </summary>
        Warhead = 1,

        /// <summary>
        /// Room safety check will fail if decontamination has gone off and this is part of <see cref="FacilityZone.LightContainment"/>.
        /// </summary>
        Decontamination = 2,

        /// <summary>
        /// Room safety check will fail if the listed room is <see cref="TeslaGate"/>.
        /// </summary>
        Tesla = 4,
    }
}