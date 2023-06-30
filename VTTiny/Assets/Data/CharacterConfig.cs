using System.Collections.Generic;
using System.Linq;
using VTTiny.Assets.Management;
using VTTiny.Serialization;

namespace VTTiny.Assets.Data;

/// <summary>
/// The config for the character asset.
/// </summary>
internal class CharacterConfig : ISerializedConfigFor<Character>
{
    public AssetReference<Texture>? Idle { get; set; }
    public IEnumerable<AssetReference<Texture>?> Speaking { get; set; }
    public AssetReference<Texture>? Blinking { get; set; }
    public AssetReference<Texture>? SpeakingBlinking { get; set; }

    public float BlinkEvery { get; set; } = 1.5f;
    public float BlinkLength { get; set; } = 0.1f;

    /// <inheritdoc/>
    public ISerializedConfigFor<Character> From(Character obj)
    {
        BlinkEvery = obj.BlinkEvery;
        BlinkLength = obj.BlinkLength;

        Idle = obj.Idle?.ToAssetReference<Texture>();
        Blinking = obj.IdleBlink?.ToAssetReference<Texture>();
        SpeakingBlinking = obj.SpeakingBlink?.ToAssetReference<Texture>();

        var speakingList = new List<AssetReference<Texture>?>();
        if (obj.Speaking != null)
            speakingList.AddRange(obj.Speaking.Select(tex => tex?.ToAssetReference<Texture>()));

        Speaking = speakingList;

        return this;
    }

    /// <inheritdoc/>
    public void Into(Character obj)
    {
        obj.BlinkLength = BlinkLength;
        obj.BlinkEvery = BlinkEvery;

        obj.Idle = Idle?.Resolve(obj.Database);
        obj.IdleBlink = Blinking?.Resolve(obj.Database);
        obj.SpeakingBlink = SpeakingBlinking?.Resolve(obj.Database);

        var speaking = new List<Texture>();
        speaking.AddRange(
            Speaking.Select(texRef => texRef?.Resolve(obj.Database))
            .Where(tex => tex != null));

        obj.Speaking = speaking;
    }
}
