using System;

namespace VTTiny.Extensions
{
    /// <summary>
    /// Extension methods for the "System.Type" class.
    /// </summary>
    internal static class TypeExtensions
    {
        /// <summary>
        /// Constructs a class from a given type and casts it to any parent class of it.
        /// </summary>
        /// <typeparam name="T">The base type to cast to.</typeparam>
        /// <param name="type">The type of the class.</param>
        /// <returns>Either the constructed class or null.</returns>
        public static T Construct<T>(this Type type) where T : class
        {
            // First ensure the <T> parameter is actually either the class or its parent class.
            if (type != typeof(T) &&
                !type.IsAssignableTo(typeof(T)))
            {
                throw new ArgumentException($"{type.FullName} is not / does not derive from {typeof(T).FullName}!", nameof(type));
            }

            var ctor = type.GetConstructor(Array.Empty<Type>());
            var klass = (T)ctor?.Invoke(Array.Empty<object>());

            return klass;
        }
    }
}
