namespace VTTiny.Editor.UI;

/// <summary>
/// The editor window responsible for drawing the properties of a currently selected object.
/// </summary>
internal class ObjectPropertiesWindow : EditorWindow
{
    /// <summary>
    /// The GUI drawable that will be rendered.
    /// </summary>
    public IEditorGUIDrawable GuiObject { get; set; }

    /// <summary>
    /// Creates a new actor properties window.
    /// </summary>
    public ObjectPropertiesWindow()
        : base("Properties")
    {

    }

    /// <inheritdoc/>
    protected override void DrawUI()
    {
        GuiObject?.RenderEditorGUI();
    }
}
