using System.Collections.Generic;

namespace VTTiny.Scripting.Serialization;

/// <summary>
/// The serialized config for the stage graph.
/// </summary>
public class StageGraphConfig
{
    /// <summary>
    /// The nodes config.
    /// </summary>
    public List<NodeConfig> Nodes { get; set; }

    /// <summary>
    /// The links.
    /// </summary>
    public List<Link> Links { get; set; }

    /// <summary>
    /// The editor config.
    /// </summary>
    public string EditorConfig { get; set; }

    /// <summary>
    /// The last allocator id.
    /// </summary>
    public int LastAllocatorId { get; set; }
}
