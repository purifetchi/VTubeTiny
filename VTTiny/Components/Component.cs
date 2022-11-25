using Newtonsoft.Json.Linq;
using VTTiny.Scenery;

namespace VTTiny.Components
{
    public abstract class Component
    {
        /// <summary>
        /// Parent actor of this component.
        /// </summary>
        public StageActor Parent { get; set; }

        /// <summary>
        /// Get a component given its type.
        /// </summary>
        /// <typeparam name="T">The type of the component (Must derive from VTTiny.Components.Component).</typeparam>
        /// <returns>Either null if not found, or the component.</returns>
        public T GetComponent<T>() where T : Component
        {
            return Parent.GetComponent<T>();
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
        /// The rendering method, called after Update.
        /// </summary>
        public virtual void Render() { }

        /// <summary>
        /// The destroy method, called at the end of an actor's lifetime.
        /// </summary>
        public virtual void Destroy() { }

        /// <summary>
        /// Inherits parameters from the config file.
        /// </summary>
        /// <param name="parameters">The raw JObject containing the parameters.</param>
        internal virtual void InheritParametersFromConfig(JObject parameters) { }
    }
}
