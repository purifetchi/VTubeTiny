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
}
