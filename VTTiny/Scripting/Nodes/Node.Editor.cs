using ImGuiNET;

namespace VTTiny.Scripting.Nodes;

public abstract partial class Node
{
    /// <inheritdoc/>
    public abstract void RenderEditorGUI();

    /// <inheritdoc/>
    public void RenderContextMenu()
    {
        if (ImGui.MenuItem("Remove node"))
        {
            Graph.RemoveNode(this);
            ImGui.EndPopup();
        }
    }
}
