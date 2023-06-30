using Raylib_cs;
using VTTiny.Assets;
using VTTiny.Assets.Management;
using VTTiny.Serialization;

namespace VTTiny.Components.Animator.Data;

/// <summary>
/// The config for an animator state.
/// </summary>
internal class AnimatorStateConfig : ISerializedConfigFor<AnimatorState>
{
    public string Name { get; set; }

    public bool IsDefaultState { get; set; } = false;

    public KeyboardKey Key { get; set; } = KeyboardKey.KEY_NULL;

    public AssetReference<Character>? Character { get; set; }

    /// <inheritdoc/>
    public ISerializedConfigFor<AnimatorState> From(AnimatorState obj)
    {
        Name = obj.Name;
        IsDefaultState = obj.IsDefaultState;
        Key = obj.Key;

        Character = obj.Character?.ToAssetReference<Character>();

        return this;
    }

    /// <inheritdoc/>
    public void Into(AnimatorState obj)
    {
        obj.Name = Name;
        obj.IsDefaultState = IsDefaultState;
        obj.Key = Key;

        obj.Character = Character?.Resolve(obj.Database);
    }
}
