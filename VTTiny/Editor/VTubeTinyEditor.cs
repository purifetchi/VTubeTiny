using ImGuiNET;
using Raylib_cs;
using rlImGui_cs;
using VTTiny.Components;
using VTTiny.Editor.Native;
using VTTiny.Rendering;
using VTTiny.Scenery;

namespace VTTiny.Editor
{
    /// <summary>
    /// The VTubeTiny editor.
    /// </summary>
    internal class VTubeTinyEditor
    {
        private VTubeTiny VTubeTiny { get; set; }

        private IRenderingContext RenderingContext { get; set; }

        private StageActor _heldActor = null;
        private Vector2Int _lastMousePosition;
        private bool _didLayoutEditorDocks = false;

        /// <summary>
        /// Instantiates a new VTubeTiny editor instance from a given VTubeTiny instance.
        /// </summary>
        /// <param name="instance">The VTubeTiny instance.</param>
        public VTubeTinyEditor(VTubeTiny instance)
        {
            VTubeTiny = instance;
            RenderingContext = new GenericRaylibRenderingContext();
        }

        /// <summary>
        /// Initializes the editor and all of the systems it needs.
        /// </summary>
        public void Initialize()
        {
            rlImGui.Setup();
            ImGui.GetIO().ConfigWindowsMoveFromTitleBarOnly = true;
            ImGui.GetIO().ConfigFlags |= ImGuiConfigFlags.DockingEnable;

            Raylib.SetWindowState(ConfigFlags.FLAG_WINDOW_RESIZABLE);
            Raylib.MaximizeWindow();

            // Switch the stage to running in framebuffer mode, so we can draw it as a window.
            VTubeTiny.ActiveStage.RenderingContext = new FramebufferRenderingContext();
            VTubeTiny.ActiveStage.RenderingContext.Resize(VTubeTiny.ActiveStage.Dimensions);
        }

        /// <summary>
        /// Renders the editor.
        /// </summary>
        public void Render()
        {
            RenderingContext.Begin();
            Raylib.ClearBackground(Color.BLANK);

            rlImGui.Begin();

            var dockId = ImGui.DockSpaceOverViewport();

            DrawMainMenuBar();
            DrawEditorView();
            DrawStageView();

            if (!_didLayoutEditorDocks)
                LayoutDockWindows(dockId);

            rlImGui.End();

            RenderingContext.End();
        }

        /// <summary>
        /// Draws the main menu bar.
        /// </summary>
        private void DrawMainMenuBar()
        {
            if (ImGui.BeginMainMenuBar())
            {
                if (ImGui.BeginMenu("File"))
                {
                    ImGui.MenuItem("Load stage");
                    ImGui.MenuItem("Save stage");
                    ImGui.EndMenu();
                }

                if (ImGui.BeginMenu("View"))
                {
                    if (ImGui.MenuItem("Reset UI"))
                        LayoutDockWindows(ImGui.DockSpaceOverViewport());

                    ImGui.EndMenu();
                }

                if (ImGui.BeginMenu("About"))
                {
                    ImGui.MenuItem("About VTubeTiny");
                    ImGui.EndMenu();
                }
                ImGui.EndMainMenuBar();
            }
        }

        /// <summary>
        /// Draws the stage view.
        /// </summary>
        private void DrawStageView()
        {
            var frameBuffer = VTubeTiny.ActiveStage.RenderingContext.GetFramebuffer().Value;
            var size = new Vector2Int(frameBuffer.width, frameBuffer.height);

            ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2Int(0, 0));

            ImGui.SetNextWindowSizeConstraints(size, size);
            if (ImGui.Begin("Stage View", ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoScrollbar))
            {
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

                ImGui.End();
            }

            ImGui.PopStyleVar();
        }

        /// <summary>
        /// Draws the editor view.
        /// </summary>
        private void DrawEditorView()
        {
            if (ImGui.Begin("VTubeTiny Editor"))
            {
                VTubeTiny.ActiveStage.RenderEditorGUI();

                ImGui.End();
            }
        }

        /// <summary>
        /// Lays out the docked windows.
        /// </summary>
        private void LayoutDockWindows(uint dockId)
        {
            var editorDockId = ImGuiDockBuilder.SplitNode(dockId, ImGuiDir.Left, 0.8f, out uint _, out uint _);
            ImGuiDockBuilder.DockWindow("VTubeTiny Editor", dockId);
            ImGuiDockBuilder.DockWindow("Stage View", editorDockId);
            ImGuiDockBuilder.Finish(dockId);

            _didLayoutEditorDocks = true;
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
        /// 
        /// NOTE: Drag and dropping images onto texture buttons takes precedence over this behavior.
        /// </summary>
        private void HandleDragAndDropImages()
        {
            if (!Raylib.IsFileDropped() || !TryGetPositionWithinStage(Raylib.GetMousePosition(), out Vector2Int mousePos))
                return;

            var path = Raylib.GetDroppedFiles()[0];
            Raylib.ClearDroppedFiles();

            var actor = VTubeTiny.ActiveStage.CreateActor();
            var renderer = actor.AddComponent<TextureRendererComponent>();

            var texture = new Texture(path);
            renderer.Texture = texture;

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
            if (!TryGetPositionWithinStage(absolutePos, out Vector2Int relativePos))
                return;

            if (_heldActor == null)
            {
                if (!TryFindNewActorForDragging(relativePos))
                    return;
            }

            if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT))
            {
                _heldActor.RenderBoundingBox();

                var delta = relativePos - _lastMousePosition;
                _heldActor.Transform.LocalPosition = _heldActor.Transform.LocalPosition + delta;

                _lastMousePosition = relativePos;
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
            Raylib.ClearWindowState(ConfigFlags.FLAG_WINDOW_RESIZABLE);
        }
    }
}
