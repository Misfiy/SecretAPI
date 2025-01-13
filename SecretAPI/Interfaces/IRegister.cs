namespace SecretAPI.Interfaces
{
    /// <summary>
    /// Interface used to define a type that should auto register.
    /// </summary>
    public interface IRegister
    {
        /// <summary>
        /// Attempts to register the object.
        /// </summary>
        /// <returns>If it was actually registers.</returns>
        public bool TryRegister();
    }
}