using ImGuiNET;
using VTTiny.Editor;
using VTTiny.Rendering;

namespace VTTiny.Scenery
{
    public partial class Stage
    {
        /// <summary>
        /// Renders the editor GUI for this scene and all the actors within this scene.
        /// </summary>
        internal void RenderEditorGUI()
        {
            RenderStageSettingsGUI();

            ImGui.NewLine();

            RenderActorsEditorGUI();
        }

        /// <summary>
        /// Renders the GUI for the stage specific settings.
        /// </summary>
        private void RenderStageSettingsGUI()
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

        /// <summary>
        /// Renders the editor gui for the actor picker.
        /// </summary>
        private void RenderActorsEditorGUI()
        {
            ImGui.Text("Actors");

            foreach (var actor in _actors)
            {
                if (actor.RenderEditorGUI())
                    break;

                ImGui.Separator();
            }

            if (ImGui.Button("Add Actor"))
                CreateActor();
        }
    }
}
