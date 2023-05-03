using ImGuiNET;

namespace VTTiny.Editor.UI;

/// <summary>
/// A message box.
/// </summary>
internal class MessageBoxWindow : EditorWindow
{
    /// <summary>
    /// The message that this message box has.
    /// </summary>
    private readonly string _message;

    /// <summary>
    /// Creates a new message box.
    /// </summary>
    /// <param name="title">The title.</param>
    /// <param name="message">The message.</param>
    public MessageBoxWindow(string title, string message)
        : base(title, ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoDocking | ImGuiWindowFlags.NoSavedSettings)
    {
        _message = message;
    }

    /// <inheritdoc/>
    protected override void PreDrawUI()
    {
        ImGui.SetNextWindowSizeConstraints(new Vector2Int(300, 20), new Vector2Int(int.MaxValue, int.MaxValue));
    }

    /// <inheritdoc/>
    protected override void DrawUI()
    {
        ImGui.Text(_message);

        if (ImGui.Button("OK"))
            Editor.RemoveWindow(this);
    }
}
