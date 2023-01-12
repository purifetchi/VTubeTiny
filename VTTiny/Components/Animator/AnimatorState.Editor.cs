using ImGuiNET;
using VTTiny.Editor;
using VTTiny.Scenery;

namespace VTTiny.Components.Animator
{
    public partial class AnimatorState
    {
        /// <summary>
        /// Draw the editor gui for this state.
        /// </summary>
        /// <param name="stage">The stage that owns the animator.</param>
        public void DrawEditorGUI(Stage stage)
        {
            ImGui.PushID(GetHashCode());

            if (ImGui.TreeNode(Name))
            {
                EditorGUI.Checkbox("Is default?", IsDefaultState);

                if (ImGui.Button("Rename state"))   
                    EditorGUI.ShowTextInputWindow($"Setting new name for state {Name}", Name, newName => Name = newName, stage.VTubeTiny);

                Key = EditorGUI.KeycodeDropdown(Key);

                ImGui.NewLine();

                Character.DrawEditorGUI(stage.AssetDatabase);

                ImGui.TreePop();
            }

            ImGui.PopID();
        }
    }
}
