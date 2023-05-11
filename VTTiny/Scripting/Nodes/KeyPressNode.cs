using Raylib_cs;
using VTTiny.Editor;
using VTTiny.Scripting.Pins;

namespace VTTiny.Scripting.Nodes;

/// <summary>
/// A node that fires on each key press.
/// </summary>
public class KeyPressNode : Node,
    ISourceNode
{
    public KeyboardKey Key { get; set; }

    private ActionOutputPin _output;

    /// <summary>
    /// Creates a new key press node.
    /// </summary>
    public KeyPressNode()
        : base("Key Press", NodeStyles.SourceNode)
    {

    }

    public override void RenderEditorGUI()
    {
        Key = EditorGUI.KeycodeDropdown(Key);
    }

    public override void Update()
    {
        if (Raylib.IsKeyPressed(Key))
            _output?.Fire();
    }

    internal override void InitializePins()
    {
        _output = AddOutput<ActionOutputPin>();
    }
}
