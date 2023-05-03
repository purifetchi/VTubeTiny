using System.Collections.Generic;
using System.Linq;
using ImGuiNET;
using VTTiny.Editor;
using VTTiny.Rendering;

namespace VTTiny.Scenery
{
    public partial class Stage
    {
        /// <inheritdoc/>
        public bool HasChildren => _actors.Count > 0;

        /// <inheritdoc/>
        public bool HasParent => false;

        /// <inheritdoc/>
        public bool IsDragDropSource { get; } = false;

        /// <inheritdoc/>
        public bool IsDragDropTarget { get; } = true;

        /// <inheritdoc/>
        public void AcceptDragDrop(IEditorStageTreeNode node)
        {
            // Unparent the actor.
            if (node is StageActor actor)
                actor.TryReparent(null);
        }

        /// <inheritdoc/>
        public IEnumerable<IEditorStageTreeNode> GetChildren()
        {
            return _actors.Where(actor => !actor.HasParent);
        }

        /// <inheritdoc/>
        public void RenderContextMenu()
        {
            if (ImGui.MenuItem("Create Actor"))
            {
                CreateActor();
                ImGui.EndPopup();
            }
        }

        /// <inheritdoc/>
        public void RenderEditorGUI()
        {
            ImGui.Text("Stage settings");

            Dimensions = EditorGUI.DragVector2("Scene dimensions", Dimensions);
            if (ImGui.IsItemDeactivatedAfterEdit())
                ResizeStage(Dimensions);

            Background.RenderEditorGUI(AssetDatabase);

            TargetFPS = EditorGUI.DragInt("Target FPS", TargetFPS);
            if (ImGui.IsItemDeactivatedAfterEdit())
                SetTargetFPS(TargetFPS);

            RenderBoundingBoxes = EditorGUI.Checkbox("Render bounding boxes", RenderBoundingBoxes);
            var newBroadcastVal = EditorGUI.Checkbox("Enable Spout Broadcasting", BroadcastViaSpout);
            if (newBroadcastVal != BroadcastViaSpout)
                SetSpoutOrDefaultContext<FramebufferRenderingContext>(newBroadcastVal);
        }
    }
}
