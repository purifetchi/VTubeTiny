using VTTiny.Editor;
using VTTiny.Scenery;
using VTTiny.Scripting.Pins;

namespace VTTiny.Scripting.Nodes;

public class ToggleActorRenderingNode : Node
{
    /// <summary>
    /// The actor we're interested in.
    /// </summary>
    private StageActor Actor { get; set; }

    /// <summary>
    /// Creates a new toggle actor rendering node.
    /// </summary>
    public ToggleActorRenderingNode()
        : base("Toggle actor rendering", NodeStyles.TailNode)
    {

    }

    public override void RenderEditorGUI()
    {
        if (EditorGUI.ActorDropdown(Graph.Stage, Actor, out var newActor))
            Actor = newActor;
    }

    public override void Update()
    {
        Actor.AllowRendering = !Actor.AllowRendering;
    }

    internal override void InitializePins()
    {
        AddInput<ActionInputPin>();
    }
}
