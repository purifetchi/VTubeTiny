using ImGuiNET;
using System.Collections.Generic;

namespace VTTiny.Editor.UI
{
    /// <summary>
    /// This is an abstraction over the ImGui main menu bar.
    /// </summary>
    internal class MenuBar
    {
        /// <summary>
        /// The editor this main menu bar is attached to.
        /// </summary>
        public VTubeTinyEditor Editor { get; private set; }

        /// <summary>
        /// A list of all categories.
        /// </summary>
        private readonly List<MenuCategory> _categories;

        /// <summary>
        /// Constructs a new menu bar.
        /// </summary>
        public MenuBar(VTubeTinyEditor editor)
        {
            Editor = editor;
            _categories = new();
        }

        /// <summary>
        /// Adds a category to this menu bar.
        /// </summary>
        /// <param name="category">The category.</param>
        public void AddCategory(MenuCategory category)
        {
            _categories.Add(category);
        }

        /// <summary>
        /// Renders the menu.
        /// </summary>
        public void Render()
        {
            if (!ImGui.BeginMainMenuBar())
                return;

            foreach (var category in _categories)
                category.Render();

            ImGui.EndMainMenuBar();
        }
    }
}
