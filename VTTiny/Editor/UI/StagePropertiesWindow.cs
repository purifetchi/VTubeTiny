using VTTiny.Scenery;

namespace VTTiny.Editor.UI
{
    /// <summary>
    /// The stage properties view.
    /// </summary>
    internal class StagePropertiesWindow : EditorWindow, IStageAwareWindow
    {
        /// <summary>
        /// The stage this window is attached to.
        /// </summary>
        public Stage Stage { get; set; }

        /// <summary>
        /// Creates a new stage properties window with a given stage.
        /// </summary>
        /// <param name="stage">The stage to set.</param>
        public StagePropertiesWindow(Stage stage) :
            base("Stage Properties")
        {
            Stage = stage;
        }

        protected override void DrawUI()
        {
            Stage.RenderEditorGUI();
        }

        public void OnStageChange(Stage stage)
        {
            Stage = stage;
        }
    }
}
