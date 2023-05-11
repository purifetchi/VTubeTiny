namespace VTTiny.Scripting;

/// <summary>
/// A link between two nodes.
/// </summary>
public sealed class Link
{
    /// <summary>
    /// The starting attribute.
    /// </summary>
    public int StartAttribute { get; set; }

    /// <summary>
    /// The starting node of the link.
    /// </summary>
    public int StartNode { get; set; }

    /// <summary>
    /// The ending attribute.
    /// </summary>
    public int EndAttribute { get; set; }

    /// <summary>
    /// The ending node of the link.
    /// </summary>
    public int EndNode { get; set; }
}
