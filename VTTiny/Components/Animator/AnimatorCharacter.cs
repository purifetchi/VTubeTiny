using VTTiny.Assets;

namespace VTTiny.Components.Animator
{
    /// <summary>
    /// A single character config for an animator.
    /// </summary>
    public partial class AnimatorCharacter
    {
        /// <summary>
        /// The state of the character
        /// </summary>
        private enum State
        {
            Idle,
            IdleBlink,
            Speaking,
            SpeakingBlink
        }

        /// <summary>
        /// The frequency of the blinking
        /// </summary>
        public float BlinkEvery { get; set; } = 1.5f;

        /// <summary>
        /// The length of each blink.
        /// </summary>
        public float BlinkLength { get; set; } = 0.1f;

        /// <summary>
        /// The texture for when the character is idling.
        /// </summary>
        public Texture Idle { get; set; }

        /// <summary>
        /// The texture for when the character is idling and blinking.
        /// </summary>
        public Texture IdleBlink { get; set; }

        /// <summary>
        /// The texture for when the character is speaking.
        /// </summary>
        public Texture Speaking { get; set; }

        /// <summary>
        /// The texture for when the character is speaking and blinking.
        /// </summary>
        public Texture SpeakingBlink { get; set; }

        /// <summary>
        /// Whether the character is speaking right now.
        /// </summary>
        public bool IsSpeaking { get; set; } = false;

        /// <summary>
        /// Whether the character is currently blinking.
        /// </summary>
        private State _currentState = State.Idle;

        /// <summary>
        /// The last blink action time.
        /// </summary>
        private double _lastBlinkAction = double.MinValue;

        /// <summary>
        /// Updates the character.
        /// </summary>
        /// <param name="time">The current time.</param>
        public void Update(double time)
        {
            var isBlinking = (_currentState == State.IdleBlink) ||
                             (_currentState == State.SpeakingBlink);

            var delta = time - _lastBlinkAction;
            if (isBlinking)
            {
                if (delta > BlinkLength)
                {
                    isBlinking = false;
                    _lastBlinkAction = time;
                }
            }
            else
            {
                if (delta > BlinkEvery)
                {
                    isBlinking = true;
                    _lastBlinkAction = time;
                }
            }

            if (isBlinking)
                _currentState = IsSpeaking ? State.SpeakingBlink : State.IdleBlink;

            else
                _currentState = IsSpeaking ? State.Speaking : State.Idle;
        }

        /// <summary>
        /// Gets the current texture for the state.
        /// </summary>
        /// <returns>The texture.</returns>
        public Texture GetCurrentStateTexture()
        {
            return _currentState switch
            {
                State.Idle => Idle,
                State.IdleBlink when IdleBlink != null => IdleBlink,
                State.Speaking when Speaking != null => Speaking,
                State.SpeakingBlink when SpeakingBlink != null => SpeakingBlink,
                _ => Idle
            };
        }
    }
}
