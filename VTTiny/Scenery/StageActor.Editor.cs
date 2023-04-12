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

                ImGui.SameLine();

                if (ImGui.Button("Rename Actor"))
                    EditorGUI.ShowRenameWindow(this, OwnerStage.VTubeTiny);

                AllowRendering = EditorGUI.Checkbox("Allow rendering", AllowRendering);

                ImGui.Text($"Parented to:");
                ImGui.SameLine();

                if (EditorGUI.ActorDropdown(OwnerStage, ParentActor, out StageActor newParent))
                    TryReparent(newParent);

                ImGui.NewLine();

                DrawComponents();

                if (EditorGUI.ComponentDropdown(out Type componentType))
                {
                    ConstructComponentFromType(componentType);
                    
                }

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
                ImGui.PushID(component.GetHashCode().ToString());

                if (ImGui.SmallButton($"X"))
                {
                    if (RemoveComponent(component))
                    {
                        ImGui.PopID();
                        break;
                    }
                }

                ImGui.SameLine();
                if (ImGui.TreeNode($"{component.GetType().Name}"))
                {
                    component.RenderEditorGUI();
                    ImGui.TreePop();
                }

                ImGui.PopID();
            }

            return;
        }
    }
}
