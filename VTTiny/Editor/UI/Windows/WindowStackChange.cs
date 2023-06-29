namespace VTTiny.Editor.UI;

/// <summary>
/// A change of the window stack.
/// </summary>
internal struct WindowStackChange
{
    /// <summary>
    /// The type of the change.
    /// </summary>
    public enum Type
    {
        Add,
        Remove
    }

    /// <summary>
    /// The window.
    /// </summary>
    public EditorWindow Window { get; set; }

    /// <summary>
    /// The change type.
    /// </summary>
    public Type ChangeType { get; set; }

    /// <summary>
    /// Construct a new window stack change.
    /// </summary>
    /// <param name="window">The window.</param>
    /// <param name="changeType">The change type.</param>
    public WindowStackChange(EditorWindow window, Type changeType)
    {
        Window = window;
        ChangeType = changeType;
    }
}
