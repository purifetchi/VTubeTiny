using System;
using VTTiny.Editor;
using VTTiny.Scripting.Pins;

namespace VTTiny.Scripting.Nodes;

internal class ConsoleMessageNode : Node
{
    public string Text { get; set; } = string.Empty;

    public ConsoleMessageNode()
        : base("Console Message", NodeStyles.TailNode)
    {

    }

    public override void RenderEditorGUI()
    {
        Text = EditorGUI.InputText("The text", Text);
    }

    public override void Update()
    {
        Console.WriteLine(Text);
    }

    internal override void InitializePins()
    {
        AddInput<ActionInputPin>();
    }
}
