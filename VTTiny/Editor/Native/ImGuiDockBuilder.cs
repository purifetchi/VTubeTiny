using ImGuiNET;
using System.Runtime.InteropServices;

namespace VTTiny.Editor.Native
{
    /// <summary>
    /// This class exposes some of the (yet unexposed in ImGui.NET) dock building functionality of ImGui for convenience.
    /// 
    /// The best course of action would be to remove it as soon as Imgui.NET actually starts generating bindings for the imgui_internal.h header.
    /// </summary>
    internal static class ImGuiDockBuilder
    {
        private const string IMGUI_LIBRARY_NAME = "cimgui";

        [DllImport(IMGUI_LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern void igDockBuilderDockWindow([MarshalAs(UnmanagedType.LPStr)] string title, uint id);

        /// <summary>
        /// Dock a window within another window.
        /// </summary>
        /// <param name="title">The title of the window to dock.</param>
        /// <param name="id">The id of the parent dock.</param>
        public static void DockWindow(string title, uint id)
        {
            igDockBuilderDockWindow(title, id);
        }

        [DllImport(IMGUI_LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern uint igDockBuilderSplitNode(uint nodeId, ImGuiDir dir, float ratio, out uint outIdAtDir, out uint outIdAtOpposite);

        /// <summary>
        /// Split a dock node.
        /// </summary>
        /// <param name="nodeId">The node id to split.</param>
        /// <param name="dir">The direction in which to split.</param>
        /// <param name="ratio">The ratio of the split.</param>
        /// <param name="idAtDir">The id of the node at the direction.</param>
        /// <param name="idAtOpposite">The id of the node on the opposite side.</param>
        /// <returns>The id of the newly split node.</returns>
        public static uint SplitNode(uint nodeId, ImGuiDir dir, float ratio, out uint idAtDir, out uint idAtOpposite)
        {
            return igDockBuilderSplitNode(nodeId, dir, ratio, out idAtDir, out idAtOpposite);
        }

        [DllImport(IMGUI_LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern void igDockBuilderFinish(uint nodeId);

        /// <summary>
        /// Finish the dock builder for a node.
        /// </summary>
        /// <param name="nodeId">The node.</param>
        public static void Finish(uint nodeId)
        {
            igDockBuilderFinish(nodeId);
        }
    }
}
