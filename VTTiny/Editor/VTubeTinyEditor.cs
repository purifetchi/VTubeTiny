using ImGuiNET;
using Raylib_cs;
using rlImGui_cs;
using VTTiny.Components;

namespace VTTiny.Editor
{
    internal class VTubeTinyEditor
    {
        private VTubeTiny VTubeTiny { get; set; }

        /// <summary>
        /// Instantiates a new VTubeTiny editor instance from a given VTubeTiny instance.
        /// </summary>
        /// <param name="instance">The VTubeTiny instance.</param>
        public VTubeTinyEditor(VTubeTiny instance)
        {
            VTubeTiny = instance;
        }

        /// <summary>
        /// Initializes the editor and all of the systems it needs.
        /// </summary>
        public void Initialize()
        {
            rlImGui.Setup();
        }

        /// <summary>
        /// Renders the editor.
        /// </summary>
        public void Render()
        {
            rlImGui.Begin();

            ImGui.Begin("VTubeTiny Editor");

            VTubeTiny.ActiveStage.RenderEditorGUI();
            HandleDragAndDropImages();

            ImGui.End();

            rlImGui.End();
        }

        /// <summary>
        /// Handles drag and dropping images into the VTubeTiny stage. Will automatically instantiate a
        /// new actor with a texture renderer set to the dragged image.
        /// 
        /// NOTE: Drag and dropping images onto texture buttons takes precedence over this behavior.
        /// </summary>
        private void HandleDragAndDropImages()
        {
            if (!Raylib.IsFileDropped())
                return;

            var path = Raylib.GetDroppedFiles()[0];
            Raylib.ClearDroppedFiles();

            var renderer = VTubeTiny.ActiveStage.CreateActor()
                                                .AddComponent<TextureRendererComponent>();

            renderer.Texture = Raylib.LoadTexture(path);
        }

        /// <summary>
        /// Destroys all of the systems and data used by the editor.
        /// </summary>
        public void Destroy()
        {
            rlImGui.Shutdown();
        }
    }
}
