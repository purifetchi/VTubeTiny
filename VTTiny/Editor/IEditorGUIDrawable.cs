namespace VTTiny.Editor;

/// <summary>
/// Implemented by all objects which want their GUI to be drawn.
/// </summary>
public interface IEditorGUIDrawable
{
    /// <summary>
    /// Draw the editor GUI for this object.
    /// </summary>
    void RenderEditorGUI();
}
