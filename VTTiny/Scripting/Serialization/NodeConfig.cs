using VTTiny.Data;

namespace VTTiny.Scripting.Serialization;

/// <summary>
/// The config for a single node.
/// </summary>
public class NodeConfig
{
    /// <summary>
    /// Its id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The config of said node.
    /// </summary>
    public TypedObjectConfig Config { get; set; }
}
