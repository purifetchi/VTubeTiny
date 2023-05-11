using System;
using ImGuiNET;
using VTTiny.Editor;
using VTTiny.Scripting.Nodes;

namespace VTTiny.Scripting.Pins;

/// <summary>
/// A pin.
/// </summary>
public abstract class Pin : IEditorGUIDrawable
{
    /// <summary>
    /// The id of the pin.
    /// </summary>
    public int Id { get; internal set; }

    /// <summary>
    /// The node it's a part of.
    /// </summary>
    public Node Node { get; internal set; }

    /// <inheritdoc/>
    public virtual void RenderEditorGUI()
    {
        ImGui.NewLine();
    }
}
