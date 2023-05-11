using VTTiny.Assets;
using VTTiny.Editor;
using VTTiny.Scripting.Pins;

namespace VTTiny.Scripting.Nodes;
internal class PlaySoundNode : Node
{
    /// <summary>
    /// The sound.
    /// </summary>
    private Sound _sound;

    /// <summary>
    /// Creates a new play sound node.
    /// </summary>
    public PlaySoundNode()
        : base("Play sound", NodeStyles.TailNode)
    {

    }

    public override void RenderEditorGUI()
    {
        if (EditorGUI.AssetDropdown("Sound", Graph.Stage.AssetDatabase, _sound, out var newSound))
            _sound = newSound;
    }

    public override void Update()
    {
        _sound?.PlayOnce();
    }

    internal override void InitializePins()
    {
        AddInput<ActionInputPin>();
    }
}
