using VTTiny.Assets.Management;

namespace VTTiny.Components.Animator;

public interface IEditableGUI
{
    /// <summary>
    /// Draw the editor gui for this character.
    /// </summary>
    /// <param name="assetDatabase">The asset database to resolve the textures from.</param>
    void DrawEditorGUI(AssetDatabase assetDatabase);
}