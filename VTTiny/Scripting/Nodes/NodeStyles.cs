using System.Numerics;
using ImGuiNET;

namespace VTTiny.Scripting.Nodes;

/// <summary>
/// Styling helpers for nodes.
/// </summary>
public static class NodeStyles
{
    /// <summary>
    /// The color for a source node.
    /// </summary>
    public static ImColor SourceNode { get; } = new ImColor { Value = new Vector4(0.5137254901960784f, 0.7019607843137254f, 0.8784313725490196f, 1f) };

    /// <summary>
    /// The color for a passthrough node.
    /// </summary>
    public static ImColor PassthroughNode { get; } = new ImColor { Value = new Vector4(0.9411764705882353f, 0.9215686274509803f, 0.6705882352941176f, 1f) };

    /// <summary>
    /// The color for a tail node.
    /// </summary>
    public static ImColor TailNode { get; } = new ImColor { Value = new Vector4(0.9215686274509803f, 0.6078431372549019f, 0.7411764705882353f, 1f) };
}
