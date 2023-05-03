using ImGuiNET;
using VTTiny.Editor.Native;

namespace VTTiny.Editor.UI
{
    /// <summary>
    /// This is an abstraction over ImGui windows.
    /// </summary>
    internal abstract class EditorWindow
    {
        /// <summary>
        /// The name of this window.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The flags this window has attached to them.
        /// </summary>
        public ImGuiWindowFlags Flags { get; set; }

        /// <summary>
        /// The editor.
        /// </summary>
        public VTubeTinyEditor Editor { get; internal set; }

        /// <summary>
        /// Constructs a new window with the given name.
        /// </summary>
        /// <param name="name">The name of the window.</param>
        /// <param name="flags">The flags to attach to this window.</param>
        public EditorWindow(string name, ImGuiWindowFlags flags = ImGuiWindowFlags.None)
        {
            Name = name;
            Flags = flags;
        }

        /// <summary>
        /// Renders this window.
        /// </summary>
        public void Render()
        {
            PreDrawUI();

            if (ImGui.Begin(Name, Flags))
            {
                DrawUI();
                ImGui.End();
            }

            PostDrawUI();
        }

        /// <summary>
        /// Docks this window to a dock space.
        /// </summary>
        /// <param name="dockId">The id of the dock space.</param>
        public void Dock(uint dockId)
        {
            ImGuiDockBuilder.DockWindow(Name, dockId);
        }

        /// <summary>
        /// Draws the UI of this window.
        /// </summary>
        protected abstract void DrawUI();

        /// <summary>
        /// Invoked before we start drawing this window.
        /// </summary>
        protected virtual void PreDrawUI() { }

        /// <summary>
        /// Invoked after we're done drawing this window.
        /// </summary>
        protected virtual void PostDrawUI() { }
    }
}
