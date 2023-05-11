using VTTiny.Assets;
using VTTiny.Editor;
using VTTiny.Scripting.Pins;

namespace VTTiny.Scripting.Nodes;

/// <summary>
/// A node which sets the frame of an animated texture.
/// </summary>
internal class SetTextureFrameNode : Node
{
    /// <summary>
    /// The texture asset.
    /// </summary>
    private GifTexture Texture { get; set; }

    /// <summary>
    /// The frame to set.
    /// </summary>
    private int Frame { get; set; } = 0;

    /// <summary>
    /// Creates a new toggle actor rendering node.
    /// </summary>
    public SetTextureFrameNode()
        : base("Set texture frame", NodeStyles.TailNode)
    {

    }

    public override void RenderEditorGUI()
    {
        if (EditorGUI.AssetDropdown("Texture", Graph.Stage.AssetDatabase, Texture, out var newTex))
            Texture = newTex;

        Frame = EditorGUI.DragInt("Frame", Frame);
    }

    public override void Update()
    {
        Texture?.SetFrame(Frame);
    }

    internal override void InitializePins()
    {
        AddInput<ActionInputPin>();
    }
}
