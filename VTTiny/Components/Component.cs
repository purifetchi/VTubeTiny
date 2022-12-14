using System.Text.Json;
using VTTiny.Data;
using VTTiny.Scenery;
using VTTiny.Serialization;

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
        internal T JsonObjectToConfig<T>(JsonElement? parameters) where T : new()
        {
            if (parameters.HasValue)
                return JsonSerializationHelper.JsonElementToType<T>(parameters.Value);
            return new T();
        }

        /// <summary>
        /// Packages this component into its config class for exporting.
        /// </summary>
        /// <returns>The component config.</returns>
        internal ComponentConfig PackageComponentIntoConfig()
        {
            var config = new ComponentConfig
            {
                Type = GetType().Name,
                Namespace = GetType().Namespace
            };

            var parameters = PackageParametersIntoConfig();
            if (parameters != null)
                config.Parameters = JsonSerializationHelper.ObjectToJsonElement(parameters);

            return config;
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
        /// Packages this component's parameters into a config class for exporting.
        /// </summary>
        /// <returns>The config class (or none).</returns>
        protected virtual object PackageParametersIntoConfig() { return null; }

        /// <summary>
        /// Inherits parameters from the config file.
        /// </summary>
        /// <param name="parameters">The raw JObject containing the parameters.</param>
        internal virtual void InheritParametersFromConfig(JsonElement? parameters) { }

        /// <summary>
        /// The method called whenever we're updating the editor GUI, called after the regular Update() when editor mode is on.
        /// </summary>
        internal virtual void RenderEditorGUI() { }
    }
}
