using System;
using System.Collections.Generic;
using ImGuiNET;
using Raylib_cs;
using rlImGui_cs;
using VTTiny.Editor.Native;
using VTTiny.Editor.Native.Win32;
using VTTiny.Editor.UI;
using VTTiny.Rendering;
using VTTiny.Scenery;

namespace VTTiny.Editor
{
    /// <summary>
    /// The VTubeTiny editor.
    /// </summary>
    internal class VTubeTinyEditor
    {
        /// <summary>
        /// The current instance of VTubeTiny this editor is working with.
        /// </summary>
        private VTubeTiny VTubeTiny { get; set; }

        /// <summary>
        /// The rendering context of the window.
        /// </summary>
        private IRenderingContext RenderingContext { get; set; }

        private readonly List<EditorWindow> _windows;
        private bool _didLayoutEditorDocks = false;
        private bool _wasEditorListModified = false;

        /// <summary>
        /// Instantiates a new VTubeTiny editor instance from a given VTubeTiny instance.
        /// </summary>
        /// <param name="instance">The VTubeTiny instance.</param>
        public VTubeTinyEditor(VTubeTiny instance)
        {
            VTubeTiny = instance;
            RenderingContext = new GenericRaylibRenderingContext();
            _windows = new List<EditorWindow>();
        }

        /// <summary>
        /// Adds a new window to the window list.
        /// </summary>
        /// <param name="window">The window.</param>
        internal void AddWindow(EditorWindow window)
        {
            _windows.Add(window);

            _wasEditorListModified = true;
        }

        /// <summary>
        /// Gets a window by its type.
        /// </summary>
        /// <typeparam name="T">The type of the window (must derive from `VTTiny.Editor.UI.EditorWindow`.</typeparam>
        /// <returns>Either the window or null.</returns>
        internal T GetWindow<T>() where T: EditorWindow
        {
            foreach (var window in _windows)
            {
                if (window is T typedWindow)
                    return typedWindow;
            }

            return null;
        }

        /// <summary>
        /// Removes a window from the window list.
        /// </summary>
        /// <param name="window">The window to remove.</param>
        /// <returns>Whether the removing was successful.</returns>
        internal bool RemoveWindow(EditorWindow window)
        {
            if (!_windows.Contains(window))
                return false;

            _wasEditorListModified = true;
            return _windows.Remove(window);
        }

        /// <summary>
        /// Executes a callback for each window that derives from a type.
        /// </summary>
        /// <typeparam name="T">The type the window should derive from.</typeparam>
        /// <param name="callback">The callback.</param>
        private void ForEachWindowOfType<T>(Action<T> callback)
        {
            foreach (var window in _windows)
            {
                if (window is T type)
                    callback(type);
            }
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

            AddWindow(new StageViewWindow(VTubeTiny.ActiveStage));
            AddWindow(new StagePropertiesWindow(VTubeTiny.ActiveStage));

            _wasEditorListModified = false;
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

            foreach (var window in _windows)
            {
                window.Render();
                
                if (_wasEditorListModified)
                {
                    _wasEditorListModified = false;
                    break;
                }
            }

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
                    if (ImGui.MenuItem("New stage"))
                    {
                        var stage = Stage.Blank();
                        VTubeTiny.SetActiveStage(stage);

                        ForEachWindowOfType<IStageAwareWindow>(window => window.OnStageChange(VTubeTiny.ActiveStage));
                    }

                    if (ImGui.MenuItem("Load stage"))
                    {
                        var path = FileDialog.OpenFile();
                        if (string.IsNullOrEmpty(path))
                            return;

                        VTubeTiny.LoadConfigFromFile(path);
                        VTubeTiny.ReloadStage();

                        ForEachWindowOfType<IStageAwareWindow>(window => window.OnStageChange(VTubeTiny.ActiveStage));
                    }

                    if (ImGui.MenuItem("Save stage"))
                    {
                        var path = FileDialog.SaveFile();
                        if (string.IsNullOrEmpty(path))
                            return;

                        VTubeTiny.ActiveStage.ExportStageToFile(path);
                    }

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
                    if (ImGui.MenuItem("About VTubeTiny"))
                    {
                        if (GetWindow<AboutWindow>() == null)
                            AddWindow(new AboutWindow(this));
                    }

                    ImGui.EndMenu();
                }

                ImGui.EndMainMenuBar();
            }
        }

        /// <summary>
        /// Lays out the docked windows.
        /// </summary>
        private void LayoutDockWindows(uint dockId)
        {
            var editorDockId = ImGuiDockBuilder.SplitNode(dockId, ImGuiDir.Left, 0.8f, out uint _, out uint _);
            GetWindow<StagePropertiesWindow>().Dock(dockId);
            GetWindow<StageViewWindow>().Dock(editorDockId);
            ImGuiDockBuilder.Finish(dockId);

            _didLayoutEditorDocks = true;
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
