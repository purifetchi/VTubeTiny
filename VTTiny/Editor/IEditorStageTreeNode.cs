using System.Collections.Generic;
using VTTiny.Base;

namespace VTTiny.Editor;

/// <summary>
/// Inherited by everything that wants to be an editor stage tree node.
/// </summary>
public interface IEditorStageTreeNode : IEditorGUIDrawable,
    INamedObject,
    IHasRightClickContext
{
    /// <summary>
    /// Does this node have any children?
    /// </summary>
    bool HasChildren { get; }

    /// <summary>
    /// Does this node have any parents?
    /// </summary>
    bool HasParent { get; }

    /// <summary>
    /// Is this node a drag/drop source?
    /// </summary>
    bool IsDragDropSource { get; }

    /// <summary>
    /// Is this node a drag/drop target?
    /// </summary>
    bool IsDragDropTarget { get; }

    /// <summary>
    /// Accepts a drag'n'drop node.
    /// </summary>
    /// <param name="node">The node.</param>
    void AcceptDragDrop(IEditorStageTreeNode node);
    
    /// <summary>
    /// Get the children of this node.
    /// </summary>
    /// <returns>The children.</returns>
    IEnumerable<IEditorStageTreeNode> GetChildren();
}
