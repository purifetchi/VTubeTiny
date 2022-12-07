namespace VTTiny.Editor.UI
{
    /// <summary>
    /// The about category for a menu.
    /// </summary>
    internal class AboutMenuCategory : MenuCategory
    {
        /// <summary>
        /// Creates a new about menu category.
        /// </summary>
        /// <param name="menuBar">The menu bar we should be attached to.</param>
        public AboutMenuCategory(MenuBar menuBar)
            : base("About", menuBar)
        {

        }

        protected override void InitializeBaseActions()
        {
            AddAction("About VTubeTiny", editor =>
            {
                if (editor.GetWindow<AboutWindow>() != null)
                    return;

                editor.AddWindow(new AboutWindow(editor));
            });
        }
    }
}
