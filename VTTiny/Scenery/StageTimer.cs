namespace VTTiny.Scenery
{
    /// <summary>
    /// A timer that counts how much time has passed in a stage ever since a set point in time.
    /// </summary>
    public class StageTimer
    {
        private Stage _stage;
        private double _timePoint;

        /// <summary>
        /// Get the elapsed time since that time point.
        /// </summary>
        public double TimeElapsed => _stage.Time - _timePoint;

        /// <summary>
        /// Creates a new stage timer. This will show you the amount of time that has passed from
        /// a specific point of time.
        /// </summary>
        /// <param name="stage">The stage that this timer will be associated with.</param>
        public StageTimer(Stage stage)
        {
            _stage = stage;
            _timePoint = _stage.Time;
        }

        /// <summary>
        /// Set the point of time from when to start counting.
        /// </summary>
        /// <param name="time"></param>
        public void Set(double time)
        {
            _timePoint = time;
        }

        /// <summary>
        /// Set the current scene time as the time point.
        /// </summary>
        public void SetNow()
        {
            Set(_stage.Time);
        }
    }
}
