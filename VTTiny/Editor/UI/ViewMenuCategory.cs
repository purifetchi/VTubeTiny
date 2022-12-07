using ImGuiNET;

namespace VTTiny.Editor.UI
{
    /// <summary>
    /// The view category for a menu.
    /// </summary>
    internal class ViewMenuCategory : MenuCategory
    {
        /// <summary>
        /// Creates a new view menu category.
        /// </summary>
        /// <param name="menuBar">The menu bar we should be attached to.</param>
        public ViewMenuCategory(MenuBar menuBar)
            : base("View", menuBar)
        {

        }
        
        protected override void InitializeBaseActions()
        {
            AddAction("Reset UI", editor =>
            {
                editor.LayoutDockWindows(ImGui.DockSpaceOverViewport());
            });
        }
    }
}
