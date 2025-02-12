namespace SecretAPI.Features
{
    using System;
    using System.Linq;
    using System.Reflection;
    using HarmonyLib;

    /// <summary>
    /// Handles setting field properties.
    /// </summary>
    /// <remarks>Should only be used for readonly fields.</remarks>
    public class FieldProperty
    {
        private readonly FieldInfo fieldInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldProperty"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="propertyName">Property name.</param>
        internal FieldProperty(Type type, string propertyName)
        {
            fieldInfo = AccessTools.Field(type, propertyName);
            Registry<FieldProperty>.Registered.Add(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="FieldProperty"/> class.
        /// </summary>
        ~FieldProperty() => Registry<FieldProperty>.Registered.Remove(this);

        /// <summary>
        /// Gets or creates the FieldProperty for the specified property.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="propertyName">The property.</param>
        /// <returns>The FieldProperty found/created.</returns>
        public static FieldProperty Get(Type type, string propertyName)
        {
            FieldProperty value = Registry<FieldProperty>.Registered.FirstOrDefault(property =>
                property.fieldInfo.FieldType == type && property.fieldInfo.Name == propertyName)
                ?? new FieldProperty(type, propertyName);

            return value;
        }

        /// <summary>
        /// Sets the value of a field.
        /// </summary>
        /// <param name="instance">The current instance to modify.</param>
        /// <param name="value">The value to set.</param>
        public void SetValue(object? instance, object value) => fieldInfo.SetValue(instance, value);

        /// <inheritdoc/>
        public override string ToString() => $"{fieldInfo.FieldType}::{fieldInfo.Name}";
    }
}