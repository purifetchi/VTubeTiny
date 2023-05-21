using System.Numerics;
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

    /// <summary>
    /// The position of this node within the stage graph.
    /// </summary>
    public Vector2 Position { get; set; }

    /// <summary>
    /// The configs of the pins.
    /// </summary>
    public PinConfig Pins { get; set; }
}
