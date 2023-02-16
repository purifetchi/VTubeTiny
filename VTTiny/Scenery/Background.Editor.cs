using ImGuiNET;
using VTTiny.Assets;
using VTTiny.Assets.Management;
using VTTiny.Editor;
using VTTiny.Extensions;

namespace VTTiny.Scenery
{
    public sealed partial class Background
    {
        /// <summary>
        /// Renders this background's editor.
        /// </summary>
        public void RenderEditorGUI(AssetDatabase database)
        {
            if (!ImGui.TreeNode("Background Settings"))
                return;

            Color = EditorGUI.ColorEdit("Color", Color);
            DrawTexture = EditorGUI.Checkbox("Use a texture?", DrawTexture);

            if (DrawTexture)
            {
                if (EditorGUI.AssetDropdown("Background Texture", database, Texture, out Texture newTexture))
                    Texture = newTexture;

                if (EditorGUI.Checkbox("Tiling?", TextureMode == BackgroundTextureMode.Tile))
                {
                    TextureMode = BackgroundTextureMode.Tile;

                    // Going below .02f seems to mess up Raylib and crashes the app for some reason(???)
                    TilingScale = EditorGUI.DragFloat("Tiling Scale", TilingScale, 0.01f)
                        .Clamp(.02f, float.PositiveInfinity);
                }
                else
                {
                    TextureMode = BackgroundTextureMode.Stretch;
                }
            }

            ImGui.TreePop();
        }
    }
}
