using ImGuiNET;
using Raylib_cs;
using rlImGui_cs;
using VTTiny.Components;
using VTTiny.Scenery;

namespace VTTiny.Editor
{
    internal class VTubeTinyEditor
    {
        private VTubeTiny VTubeTiny { get; set; }

        private StageActor _heldActor = null;
        private Vector2Int _lastMousePosition;

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
            HandleActorDragging();

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

            var actor = VTubeTiny.ActiveStage.CreateActor();
            var renderer = actor.AddComponent<TextureRendererComponent>();

            var texture = new Texture(path);
            renderer.Texture = texture;

            // Offset the mouse cursor by half of the texture's size, putting the actor's center
            // at the position of the mouse.
            var mousePos = (Vector2Int)Raylib.GetMousePosition();
            mousePos -= new Vector2Int(texture.Width / 2, texture.Height / 2);

            actor.Transform.LocalPosition = mousePos;
        }

        /// <summary>
        /// Handles dragging actors around with the mouse.
        /// </summary>
        private void HandleActorDragging()
        {
            var mousePos = (Vector2Int)Raylib.GetMousePosition();

            if (_heldActor == null)
            {
                if (!TryFindNewActorForDragging(mousePos))
                    return;
            }

            if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT))
            {
                _heldActor.RenderBoundingBox();

                var delta = mousePos - _lastMousePosition;
                _heldActor.Transform.LocalPosition = _heldActor.Transform.LocalPosition + delta;

                _lastMousePosition = mousePos;
                return;
            }

            _heldActor = null;
        }

        /// <summary>
        /// Tries to get a new actor for dragging.
        /// </summary>
        /// <param name="position">The position to hit test from.</param>
        /// <returns>True if found, false otherwise.</returns>
        private bool TryFindNewActorForDragging(Vector2Int position)
        {
            if (!Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
                return false;

            if (ImGui.IsWindowHovered() || ImGui.IsAnyItemHovered())
                return false;

            var actor = VTubeTiny.ActiveStage.HitTest(position);
            if (actor == null)
                return false;

            _heldActor = actor;
            _lastMousePosition = position;
            return true;
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
