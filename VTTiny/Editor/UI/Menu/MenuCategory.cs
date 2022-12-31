using System;
using System.Collections.Generic;
using ImGuiNET;

namespace VTTiny.Editor.UI
{
    /// <summary>
    /// A singular category for a menu.
    /// </summary>
    internal class MenuCategory
    {
        /// <summary>
        /// A single action for the menu category.
        /// </summary>
        private class MenuAction
        {
            /// <summary>
            /// The name of the action.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// The callback for the action.
            /// </summary>
            public Action<VTubeTinyEditor> Callback { get; set; }

            /// <summary>
            /// Should the action render a separator above itself?
            /// </summary>
            public bool SeparatorAbove { get; set; } = false;
        }

        /// <summary>
        /// The name of this category.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The parent menu.
        /// </summary>
        public MenuBar ParentMenu { get; set; }

        /// <summary>
        /// The list of actions within this menu.
        /// </summary>
        private readonly List<MenuAction> _actions;

        /// <summary>
        /// Creates a new menu category.
        /// </summary>
        /// <param name="name">The name of the category.</param>
        /// <param name="menuBar">The menu bar we should be attached to.</param>
        public MenuCategory(string name, MenuBar menuBar)
        {
            Name = name;
            ParentMenu = menuBar;
            _actions = new();

            InitializeBaseActions();
        }

        /// <summary>
        /// Adds a new action to this menu category.
        /// </summary>
        /// <param name="name">The name of this action.</param>
        /// <param name="callback">The callback.</param>
        public void AddAction(string name, Action<VTubeTinyEditor> callback, bool withSeparator = false)
        {
            _actions.Add(new MenuAction
            {
                Name = name,
                Callback = callback,
                SeparatorAbove = withSeparator
            });
        }

        /// <summary>
        /// Renders this menu category.
        /// </summary>
        public void Render()
        {
            if (!ImGui.BeginMenu(Name))
                return;

            foreach (var action in _actions)
            {
                if (action.SeparatorAbove)
                    ImGui.Separator();

                if (ImGui.MenuItem(action.Name))
                    action.Callback?.Invoke(ParentMenu.Editor);
            }

            ImGui.EndMenu();
        }

        /// <summary>
        /// Initializes all the base actions for this menu category.
        /// </summary>
        protected virtual void InitializeBaseActions() { }
    }
}
