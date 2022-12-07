using VTTiny.Scenery;

namespace VTTiny.Editor.UI
{
    /// <summary>
    /// Defines a window that's aware of the current stage and responsive to the stage changes.
    /// </summary>
    internal interface IStageAwareWindow
    {
        /// <summary>
        /// Called when the current active stage changes.
        /// </summary>
        /// <param name="stage">The new stage.</param>
        void OnStageChange(Stage stage);
    }
}
