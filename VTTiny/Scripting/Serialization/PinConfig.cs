using System.Collections.Generic;

namespace VTTiny.Scripting.Serialization;

/// <summary>
/// The configs of the pins.
/// </summary>
public sealed class PinConfig
{
    /// <summary>
    /// The ids of the input pins for this node.
    /// </summary>
    public List<int> InputPinIds { get; set; }

    /// <summary>
    /// The ids of the output pins for this node.
    /// </summary>
    public List<int> OutputPinIds { get; set; }
}
