using Raylib_cs;
using VTTiny.Assets;
using VTTiny.Assets.Management;
using VTTiny.Base;

namespace VTTiny.Components.Animator;

/// <summary>
/// This class contains the single definition of a state for the character animator.
/// </summary>
public partial class AnimatorState : INamedObject
{
    /// <summary>
    /// The name of this animator state.
    /// </summary>
    public string Name { get; set; } = "Character State";

    /// <summary>
    /// Whether this is the default state.
    /// </summary>
    public bool IsDefaultState { get; set; } = false;

    /// <summary>
    /// The key that toggles this state.
    /// </summary>
    public KeyboardKey Key { get; set; } = KeyboardKey.KEY_NULL;

    /// <summary>
    /// The character.
    /// </summary>
    public Character Character { get; set; }

    /// <summary>
    /// The database which the character is in.
    /// </summary>
    public AssetDatabase Database { get; set; }

    /// <summary>
    /// Construct a new animator state with the given database.
    /// </summary>
    /// <param name="db">The database.</param>
    public AnimatorState(AssetDatabase db)
    {
        Database = db;
    }
}
