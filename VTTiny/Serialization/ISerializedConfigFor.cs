namespace VTTiny.Serialization;

/// <summary>
/// An interface derived by anything that wants to be a serialized config for some object.
/// </summary>
/// <typeparam name="T">The object.</typeparam>
public interface ISerializedConfigFor<T>
{
    /// <summary>
    /// Creates the config from the object.
    /// </summary>
    /// <param name="obj">The object.</param>
    ISerializedConfigFor<T> From(T obj);

    /// <summary>
    /// Deserializes all self data into the object.
    /// </summary>
    /// <param name="obj">The object to deserialize into.</param>
    void Into(T obj);
}
