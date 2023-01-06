using VTTiny.Scenery;
using VTTiny.Serialization;

namespace VTTiny.Components
{
    public abstract class Component : TypedSerializedObject
    {
        /// <summary>
        /// Parent actor of this component.
        /// </summary>
        public StageActor Parent { get; set; }

        /// <summary>
        /// Get a component given its type.
        /// </summary>
        /// <typeparam name="T">The type of the component (or the interface it inherits).</typeparam>
        /// <returns>Either null if not found, or the component.</returns>
        public T GetComponent<T>()
        {
            return Parent.GetComponent<T>();
        }

        /// <summary>
        /// Gets all the components given their type (or inherited interface).
        /// </summary>
        /// <typeparam name="T">The type of the component (or the interface it inherits).</typeparam>
        /// <returns>A list of all of the components.</returns>
        public T[] GetComponents<T>()
        {
            return Parent.GetComponents<T>();
        }

        /// <summary>
        /// Set the parent actor of this component.
        /// </summary>
        /// <param name="parent">The parent actor.</param>
        public void SetParent(StageActor parent)
        {
            Parent = parent;
        }

        /// <summary>
        /// The start method, called on the component initialization.
        /// </summary>
        public virtual void Start() { }

        /// <summary>
        /// The update method, called each frame for the component.
        /// </summary>
        public virtual void Update() { }

        /// <summary>
        /// The destroy method, called at the end of an actor's lifetime.
        /// </summary>
        public virtual void Destroy() { }

        /// <summary>
        /// The method called whenever we're updating the editor GUI, called after the regular Update() when editor mode is on.
        /// </summary>
        internal virtual void RenderEditorGUI() { }
    }
}
