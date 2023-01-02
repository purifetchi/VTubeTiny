using System;
using System.Collections.Generic;
using ImGuiNET;
using Raylib_cs;
using rlImGui_cs;
using VTTiny.Editor.Native;
using VTTiny.Editor.UI;
using VTTiny.Rendering;

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
        internal VTubeTiny VTubeTiny { get; init; }

        /// <summary>
        /// The rendering context of the window.
        /// </summary>
        private IRenderingContext RenderingContext { get; init; }

        /// <summary>
        /// The main menu bar.
        /// </summary>
        private readonly MenuBar _menuBar;

        /// <summary>
        /// The windows list.
        /// </summary>
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
            _windows = new();
            _menuBar = new(this);
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
        internal T GetWindow<T>() where T : EditorWindow
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
        internal void ForEachWindowOfType<T>(Action<T> callback)
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

            AddWindow(new AssetBrowserWindow(VTubeTiny.ActiveStage));
            AddWindow(new StageViewWindow(VTubeTiny.ActiveStage));
            AddWindow(new StagePropertiesWindow(VTubeTiny.ActiveStage));

            InitializeMainMenuBar();

            _wasEditorListModified = false;
        }

        /// <summary>
        /// Initializes the main menu bar.
        /// </summary>
        private void InitializeMainMenuBar()
        {
            _menuBar.AddCategory(new FileMenuCategory(_menuBar));
            _menuBar.AddCategory(new ViewMenuCategory(_menuBar));
            _menuBar.AddCategory(new AboutMenuCategory(_menuBar));
        }

        /// <summary>
        /// Renders the editor.
        /// </summary>
        public void Render()
        {
            RenderingContext.Begin(Color.BLANK);
            rlImGui.Begin();

            var dockId = ImGui.DockSpaceOverViewport();

            _menuBar.Render();
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
        /// Lays out the docked windows.
        /// </summary>
        public void LayoutDockWindows(uint dockId)
        {
            ImGuiDockBuilder.SplitNode(dockId, ImGuiDir.Left, 0.8f, out uint editorDockId, out uint settingsDockId);
            ImGuiDockBuilder.SplitNode(settingsDockId, ImGuiDir.Down, 0.4f, out uint assetBrowserDockId, out uint propsDockId);

            GetWindow<StagePropertiesWindow>().Dock(propsDockId);
            GetWindow<StageViewWindow>().Dock(editorDockId);
            GetWindow<AssetBrowserWindow>().Dock(assetBrowserDockId);

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
