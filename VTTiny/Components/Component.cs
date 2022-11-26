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
        /// Returns the config for a component from a json object.
        /// </summary>
        /// <typeparam name="T">The type of the config.</typeparam>
        /// <param name="parameters">The parameters JObject (supplied inside of InheritParametersFromConfig)</param>
        /// <returns>Either a blank default config if parameters was null, or the decoded config.</returns>
        internal T JsonObjectToConfig<T>(JObject parameters) where T: new()
        {
            if (parameters != null)
                return parameters.ToObject<T>();
            return new T();
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

        /// <summary>
        /// The method called whenever we're updating the editor GUI, called after the regular Update() when editor mode is on.
        /// </summary>
        internal virtual void RenderEditorGUI() { }
    }
}
