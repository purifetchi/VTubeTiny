using System;
using System.Collections.Generic;
using ImGuiNET;
using VTTiny.Editor;

namespace VTTiny.Scenery
{
    public partial class StageActor
    {
        /// <inheritdoc/>
        public bool HasChildren => _children?.Count > 0;

        /// <inheritdoc/>
        public bool HasParent => ParentActor != null;

        /// <inheritdoc/>
        public bool IsDragDropSource { get; } = true;

        /// <inheritdoc/>
        public bool IsDragDropTarget { get; } = true;

        /// <inheritdoc/>
        public void AcceptDragDrop(IEditorStageTreeNode node)
        {
            // Reparent the actor to us.
            if (node is StageActor actor)
                actor.TryReparent(this, true);
        }

        /// <inheritdoc/>
        public IEnumerable<IEditorStageTreeNode> GetChildren()
        {
            return _children;
        }

        /// <inheritdoc/>
        public void RenderContextMenu()
        {
            if (ImGui.MenuItem("Remove Actor"))
            {
                OwnerStage.RemoveActor(this);
                ImGui.EndPopup();
            }

            if (ImGui.MenuItem("Rename Actor"))
            {
                EditorGUI.ShowRenameWindow(this, OwnerStage.VTubeTiny);
                ImGui.EndPopup();
            }
        }

        /// <inheritdoc/>
        public void RenderEditorGUI()
        {
            AllowRendering = EditorGUI.Checkbox("Allow rendering", AllowRendering);

            ImGui.NewLine();

            DrawComponents();

            if (EditorGUI.ComponentDropdown(out Type componentType))
                ConstructComponentFromType(componentType);

            return;
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

                if (ImGui.SmallButton("X"))
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
