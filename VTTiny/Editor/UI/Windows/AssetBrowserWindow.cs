using ImGuiNET;
using VTTiny.Scenery;

namespace VTTiny.Editor.UI
{
    /// <summary>
    /// The asset browser.
    /// </summary>
    internal class AssetBrowserWindow : EditorWindow, IStageAwareWindow
    {
        /// <summary>
        /// The stage this window is attached to.
        /// </summary>
        public Stage Stage { get; set; }

        /// <summary>
        /// Creates a new stage properties window with a given stage.
        /// </summary>
        /// <param name="stage">The stage to set.</param>
        public AssetBrowserWindow(Stage stage)
            : base("Asset Browser")
        {
            Stage = stage;
        }

        protected override void DrawUI()
        {
            ImGui.Text("Assets will show here.");
        }

        public void OnStageChange(Stage stage)
        {
            Stage = stage;
        }
    }
}
