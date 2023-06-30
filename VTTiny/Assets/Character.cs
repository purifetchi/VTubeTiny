using System.Collections.Generic;
using System.Text.Json;
using VTTiny.Assets.Data;
using VTTiny.Assets.Management;
using VTTiny.Editor;

namespace VTTiny.Assets;

/// <summary>
/// A VTubeTiny character.
/// </summary>
public class Character : Asset,
    IResolveAssetAfter<Texture>,
    IAllowAssetDropdownCreation
{
    /// <summary>
    /// The state of the characterg
    /// </summary>
    public enum State
    {
        Idle,
        IdleBlink,
        Speaking,
        SpeakingBlink
    }

    /// <summary>
    /// The frequency of the blinking
    /// </summary>
    public float BlinkEvery { get; set; } = 1.5f;

    /// <summary>
    /// The length of each blink.
    /// </summary>
    public float BlinkLength { get; set; } = 0.1f;

    /// <summary>
    /// The texture for when the character is idling.
    /// </summary>
    public Texture Idle { get; set; }

    /// <summary>
    /// The texture for when the character is idling and blinking.
    /// </summary>
    public Texture IdleBlink { get; set; }

    /// <summary>
    /// The texture for when the character is speaking.
    /// </summary>
    public IList<Texture> Speaking { get; set; }

    /// <summary>
    /// The texture for when the character is speaking and blinking.
    /// </summary>
    public Texture SpeakingBlink { get; set; }

    /// <inheritdoc/>
    protected override void InternalRenderEditorGUI()
    {
        BlinkEvery = EditorGUI.DragFloat("Blink every", BlinkEvery);
        BlinkLength = EditorGUI.DragFloat("Blink length", BlinkLength);

        if (EditorGUI.AssetDropdown("Idle", Database, Idle, out var newIdle))
            Idle = newIdle;

        if (EditorGUI.AssetDropdown("Idle blinking", Database, IdleBlink, out var newBlinking))
            IdleBlink = newBlinking;

        if (EditorGUI.AssetDropdown("Speaking blinking", Database, SpeakingBlink, out var newSpeakingBlink))
            SpeakingBlink = newSpeakingBlink;

        Speaking ??= new List<Texture>()
        {
            new Texture()
        };

        if (Speaking.Count > 0)
        {
            var last = Speaking[Speaking.Count - 1];

            if (last != default &&
                last.Id != default)
            {
                // Add a button to add a new speaking texture
                Speaking.Add(new Texture());
            }
        }
        else
        {
            // Make a temporary speaking texture
            Speaking.Add(new Texture());
        }

        for (var i = 0; i < Speaking.Count; i++)
        {
            if (EditorGUI.AssetDropdown("Speaking " + i, Database, Speaking[i], out var newSpeaking))
                Speaking[i] = newSpeaking;
        }
    }

    /// <inheritdoc/>
    protected override object PackageParametersIntoConfig()
    {
        return new CharacterConfig()
            .From(this);
    }

    /// <inheritdoc/>
    public override void InheritParametersFromConfig(JsonElement? parameters)
    {
        JsonObjectToConfig<CharacterConfig>(parameters)?
            .Into(this);
    }

    /// <inheritdoc/>
    public override void RenderAssetPreview()
    {
        if (Idle == null)
        {
            base.RenderAssetPreview();
            return;
        }

        EditorGUI.ImageButton(Idle, ASSET_PREVIEW_SIZE, ASSET_PREVIEW_SIZE);
    }
}
