using System.Text.Json;
using VTTiny.Data;

namespace VTTiny.Serialization;

/// <summary>
/// Represents an object that's serialized by its type and custom serialized parameters.
/// </summary>
public abstract class TypedSerializedObject
{
    /// <summary>
    /// Returns the config for a component from a json object.
    /// </summary>
    /// <typeparam name="T">The type of the config.</typeparam>
    /// <param name="parameters">The parameters JObject (supplied inside of InheritParametersFromConfig)</param>
    /// <returns>Either a blank default config if parameters was null, or the decoded config.</returns>
    public T JsonObjectToConfig<T>(JsonElement? parameters) where T : new()
    {
        if (parameters.HasValue)
            return JsonSerializationHelper.JsonElementToType<T>(parameters.Value);
        return new T();
    }

    /// <summary>
    /// Packages this component into its config class for exporting.
    /// </summary>
    /// <returns>The component config.</returns>
    public TypedObjectConfig PackageIntoConfig()
    {
        var config = new TypedObjectConfig
        {
            Type = GetType().Name,
            Namespace = GetType().Namespace,
            Name = GetNameForSerialization()
        };

        var parameters = PackageParametersIntoConfig();
        if (parameters != null)
            config.Parameters = JsonSerializationHelper.ObjectToJsonElement(parameters);

        return config;
    }

    /// <summary>
    /// Gets the name for the serialization.
    /// </summary>
    /// <returns>The name.</returns>
    protected virtual string GetNameForSerialization() { return null; }

    /// <summary>
    /// Packages this component's parameters into a config class for exporting.
    /// </summary>
    /// <returns>The config class (or none).</returns>
    protected virtual object PackageParametersIntoConfig() { return null; }

    /// <summary>
    /// Inherits parameters from the config file.
    /// </summary>
    /// <param name="parameters">The raw JObject containing the parameters.</param>
    public virtual void InheritParametersFromConfig(JsonElement? parameters) { }
}