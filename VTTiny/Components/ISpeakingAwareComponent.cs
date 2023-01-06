namespace VTTiny.Components
{
    /// <summary>
    /// The interface for all components that are aware of when the user is speaking.
    /// 
    /// The speaking is broadcasted by the AudioResponsiveMovementComponent.
    /// </summary>
    public interface ISpeakingAwareComponent
    {
        /// <summary>
        /// Whether the user is speaking.
        /// </summary>
        bool IsSpeaking { get; set; }
    }
}
