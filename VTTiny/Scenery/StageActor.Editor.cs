using System;
using ImGuiNET;
using VTTiny.Editor;

namespace VTTiny.Scenery
{
    public partial class StageActor
    {
        /// <summary>
        /// Renders this stage actor's editor GUI and all of the editor GUIs for the components within.
        /// </summary>
        /// <returns>
        /// Whether we've modified the actor collection before returning from the function.
        /// For example, by removing this actor from the actor list.
        /// </returns>
        internal bool RenderEditorGUI()
        {
            if (ImGui.TreeNode($"{Name}##{GetHashCode()}"))
            {
                if (ImGui.Button("Remove Actor"))
                {
                    OwnerStage.RemoveActor(this);
                    return true;
                }

                ImGui.Text($"Parented to:");
                ImGui.SameLine();

                if (EditorGUI.ActorDropdown(OwnerStage, ParentActor, out StageActor newParent))
                    TryReparent(newParent);

                DrawComponents();

                if (EditorGUI.ComponentDropdown(out Type componentType))
                    ConstructComponentFromType(componentType);

                ImGui.TreePop();
            }

            return false;
        }

        /// <summary>
        /// Draws the component list.
        /// </summary>
        private void DrawComponents()
        {
            ImGui.Text("Components");
            foreach (var component in _components)
            {
                if (ImGui.SmallButton($"X##{component.GetHashCode()}"))
                {
                    if (RemoveComponent(component))
                        break;
                }

                ImGui.SameLine();
                if (ImGui.TreeNode($"{component.GetType().Name}##{component.GetHashCode()}"))
                {
                    component.RenderEditorGUI();
                    ImGui.TreePop();
                }
            }

            return;
        }
    }
}
