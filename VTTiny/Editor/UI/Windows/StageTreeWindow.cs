using System;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using ImGuiNET;
using VTTiny.Scenery;

namespace VTTiny.Editor.UI;

internal class StageTreeWindow : EditorWindow, 
    IStageAwareWindow
{
    /// <summary>
    /// The stage we're operating on.
    /// </summary>
    private Stage _stage;

    /// <summary>
    /// The selected node.
    /// </summary>
    private IEditorStageTreeNode _selectedNode;

    /// <summary>
    /// The drag'n'dropped node.
    /// </summary>
    private IEditorStageTreeNode _dragDroppedNode;

    /// <summary>
    /// Creates a new stage tree view.
    /// </summary>
    /// <param name="stage">The stage.</param>
    public StageTreeWindow(Stage stage)
        : base("Stage Tree View")
    {
        _selectedNode = _stage = stage;
    }

    /// <inheritdoc/>
    public void OnStageChange(Stage stage)
    {
        _selectedNode = _stage = stage;
    }

    /// <summary>
    /// Sets the new selected node.
    /// </summary>
    /// <param name="drawable">The new selected node.</param>
    public void SetSelectedNode(IEditorStageTreeNode drawable)
    {
        _selectedNode = drawable;
        Editor.GetWindow<ObjectPropertiesWindow>()
                .GuiObject = _selectedNode;
    }

    /// <summary>
    /// Gets the tree node flags for a tree node.
    /// </summary>
    /// <param name="node">This tree node.</param>
    /// <returns>The tree flags.</returns>
    private ImGuiTreeNodeFlags GetTreeNodeFlagsFor(IEditorStageTreeNode node)
    {
        var flags = ImGuiTreeNodeFlags.OpenOnArrow | ImGuiTreeNodeFlags.OpenOnDoubleClick;

        if (!node.HasChildren)
            flags |= ImGuiTreeNodeFlags.Leaf;

        if (node == _selectedNode)
            flags |= ImGuiTreeNodeFlags.Selected;

        return flags;
    }

    /// <summary>
    /// Handles node drag/dropping.
    /// </summary>
    private unsafe void HandleNodeDragDrop(IEditorStageTreeNode node)
    {
        const string TYPE = "TREENODE";

        // The target part of the drag dropping.
        if (node.IsDragDropTarget &&
            ImGui.BeginDragDropTarget())
        {
            var payload = ImGui.AcceptDragDropPayload(TYPE);

            // Ensure we actually have data before reading it.
            if ((nint)payload.NativePtr != 0)
                node.AcceptDragDrop(_dragDroppedNode);

            ImGui.EndDragDropTarget();
        }

        // The source part.
        if (node.IsDragDropSource &&
            ImGui.BeginDragDropSource())
        {
            _dragDroppedNode = node;

            ImGui.SetDragDropPayload(TYPE, 0, 0);
            ImGui.TextUnformatted(node.Name);
            ImGui.EndDragDropSource();
        }
    }

    /// <summary>
    /// Draws a single node.
    /// </summary>
    /// <param name="node">The node.</param>
    private void DrawNode(IEditorStageTreeNode node)
    {
        var foldout = ImGui.TreeNodeEx(node.Name, GetTreeNodeFlagsFor(node));
        HandleNodeDragDrop(node);

        if (ImGui.IsItemClicked())
            SetSelectedNode(node);

        Editor.DoContextMenuFor(node);

        if (foldout && node.HasChildren)
        {
            try
            {
                foreach (var child in node.GetChildren())
                    DrawNode(child);
            }
            catch (InvalidOperationException)
            {
                // If this happens then the collection was probably modified.
                // This is fine, it might happen from time to time when removing actors.
            }
        }

        ImGui.TreePop();
    }

    /// <inheritdoc/>
    protected override void DrawUI()
    {
        DrawNode(_stage);
    }
}
