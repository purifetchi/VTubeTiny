using System.IO;
using ImGuiNET;
using Raylib_cs;
using rlImGui_cs;
using VTTiny.Assets;
using VTTiny.Assets.Management;
using VTTiny.Components;
using VTTiny.Rendering;
using VTTiny.Scenery;

namespace VTTiny.Editor.UI
{
    /// <summary>
    /// The stage view.
    /// </summary>
    internal class StageViewWindow : EditorWindow, IStageAwareWindow
    {
        /// <summary>
        /// The stage this window is showing.
        /// </summary>
        public Stage Stage { get; private set; }

        /// <summary>
        /// The actor that was last held while moving.
        /// </summary>
        private StageActor _heldActor = null;

        /// <summary>
        /// The last mouse position while moving.
        /// </summary>
        private Vector2Int _lastMousePosition;

        /// <summary>
        /// Creates a new stage view for a given stage.
        /// </summary>
        /// <param name="stage">The stage to show.</param>
        public StageViewWindow(Stage stage)
            : base("Stage View", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoResize)
        {
            SetStage(stage);
        }

        /// <summary>
        /// Replaces the visible stage.
        /// </summary>
        /// <param name="stage">The new stage to show.</param>
        public void SetStage(Stage stage)
        {
            if (stage.RenderingContext is not FramebufferRenderingContext)
                stage.ReplaceRenderingContext(new FramebufferRenderingContext());

            Stage = stage;
        }

        /// <summary>
        /// Tries to get the position of a mouse within a stage.
        /// </summary>
        /// <param name="absolute">The absolute position of the mouse.</param>
        /// <param name="relative">The relative position within the stage view.</param>
        /// <returns>Whether the mouse even was in the stage.</returns>
        private bool TryGetPositionWithinStage(Vector2Int absolute, out Vector2Int relative)
        {
            var pos = ImGui.GetItemRectMin();
            var size = ImGui.GetItemRectSize();

            var rect = new Rectangle
            {
                x = pos.X,
                y = pos.Y,
                height = size.Y,
                width = size.X
            };

            relative = absolute - (Vector2Int)pos;
            return Raylib.CheckCollisionPointRec(absolute, rect);
        }

        /// <summary>
        /// Handles drag and dropping images into the VTubeTiny stage. Will automatically instantiate a
        /// new actor with a texture renderer set to the dragged image.
        /// </summary>
        private void HandleDragAndDropImages()
        {
            if (!Raylib.IsFileDropped() || !TryGetPositionWithinStage(Raylib.GetMousePosition(), out Vector2Int mousePos))
                return;

            var path = Raylib.GetDroppedFiles()[0];
            Raylib.ClearDroppedFiles();

            var asset = AssetHelper.LoadBasedOnExtension(path, Stage.AssetDatabase);
            if (asset is not Texture texture)
                return;

            var actor = Stage.CreateActor();
            var renderer = actor.AddComponent<TextureRendererComponent>();

            renderer.SetTexture(texture);

            // Offset the mouse cursor by half of the texture's size, putting the actor's center
            // at the position of the mouse.
            mousePos -= new Vector2Int(texture.Width / 2, texture.Height / 2);

            actor.Transform.LocalPosition = mousePos;
        }

        /// <summary>
        /// Handles dragging actors around with the mouse.
        /// </summary>
        private void HandleActorDragging()
        {
            var absolutePos = (Vector2Int)Raylib.GetMousePosition();
            var isPosWithinStage = TryGetPositionWithinStage(absolutePos, out Vector2Int relativePos);

            if (_heldActor == null)
            {
                if (!isPosWithinStage)
                    return;

                if (!TryFindNewActorForDragging(relativePos))
                    return;
            }

            if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT))
            {
                _heldActor.Paused = true;

                _heldActor.RenderBoundingBox();

                var delta = relativePos - _lastMousePosition;
                _heldActor.Transform.LocalPosition = _heldActor.Transform.LocalPosition + delta;

                _lastMousePosition = relativePos;
                return;
            }

            _heldActor.Paused = false;
            _heldActor = null;
        }

        /// <summary>
        /// Tries to get a new actor for dragging.
        /// </summary>
        /// <param name="position">The position to hit test from.</param>
        /// <returns>True if found, false otherwise.</returns>
        private bool TryFindNewActorForDragging(Vector2Int position)
        {
            if (!Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT) || !ImGui.IsWindowHovered())
                return false;

            var actor = Stage.HitTest(position);
            if (actor == null)
                return false;

            _heldActor = actor;
            _lastMousePosition = position;
            return true;
        }

        protected override void PreDrawUI()
        {
            var frameBuffer = Stage.RenderingContext.GetFramebuffer().Value;
            var size = new Vector2Int(frameBuffer.width, frameBuffer.height);

            ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2Int(0, 0));
            ImGui.SetNextWindowSizeConstraints(size, size);
        }

        protected override void DrawUI()
        {
            var frameBuffer = Stage.RenderingContext.GetFramebuffer().Value;
            var size = new Vector2Int(frameBuffer.width, frameBuffer.height);

            // Transform the render texture, since it's drawn upside down.
            var viewRect = new Rectangle
            {
                x = frameBuffer.width / 2 - size.X / 2,
                y = frameBuffer.height / 2 - size.Y / 2,
                width = size.X,
                height = -size.Y
            };

            // Draw the buffer in the center of the screen
            var center = (Vector2Int)((ImGui.GetWindowSize() - (System.Numerics.Vector2)size) * 0.5f);
            ImGui.SetCursorPos(center);
            rlImGui.ImageRect(frameBuffer, size.X, size.Y, viewRect);

            HandleDragAndDropImages();
            HandleActorDragging();
        }

        protected override void PostDrawUI()
        {
            ImGui.PopStyleVar();
        }

        public void OnStageChange(Stage stage)
        {
            SetStage(stage);
        }
    }
}
