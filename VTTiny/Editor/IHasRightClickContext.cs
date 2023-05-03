namespace VTTiny.Editor;

/// <summary>
/// Implemented by everything that wants to have a right click context menu.
/// </summary>
public interface IHasRightClickContext
{
    /// <summary>
    /// Renders the context menu when right clicking on this node.
    /// </summary>
    void RenderContextMenu();
}
