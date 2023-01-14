using System;

namespace VTTiny.Base
{
    /// <summary>
    /// This attribute describes a single dependency for a component.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class DependsOnComponentAttribute : Attribute
    {
        /// <summary>
        /// The type that this component depends on.
        /// </summary>
        public Type ComponentType { get; init; }

        public DependsOnComponentAttribute(Type componentType)
        {
            ComponentType = componentType;
        }
    }
}
