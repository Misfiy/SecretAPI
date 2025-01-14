namespace SecretAPI.Features
{
    using System;
    using System.Linq;
    using System.Reflection;
    using HarmonyLib;

    /// <summary>
    /// Handles field properties, such as "readonly int".
    /// </summary>
    public class FieldProperty
    {
        private readonly FieldInfo fieldInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldProperty"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="propertyName">Property name.</param>
        public FieldProperty(Type type, string propertyName)
        {
            fieldInfo = AccessTools.Field(type, propertyName);
            Registry<FieldProperty>.Registered.Add(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="FieldProperty"/> class.
        /// </summary>
        ~FieldProperty() => Registry<FieldProperty>.Registered.Remove(this);

        /// <summary>
        /// Gets or creates the FieldProperty for the type & property name.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="propertyName">The property.</param>
        /// <returns>The FieldProperty found/created.</returns>
        public static FieldProperty Get(Type type, string propertyName)
            => Registry<FieldProperty>.Registered.FirstOrDefault() ?? new FieldProperty(type, propertyName);

        /// <summary>
        /// Sets the value of a field.
        /// </summary>
        /// <param name="owner">The owner of the field.</param>
        /// <param name="value">The value to set.</param>
        public void SetValue(object? owner, object value) => fieldInfo.SetValue(owner, value);
    }
}