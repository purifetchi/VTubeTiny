using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IList<Texture> Speaking { get; set; }

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
        /// The last mouth texture.
        /// </summary>
        private Texture _lastViseme = null;
        
        /// <summary>
        /// Time inbetween visemes.
        /// </summary>
        private double _timeInbetweenVisemes = 100f;
        
        /// <summary>
        /// Last viseme time
        /// </summary>
        private TimeSpan _lastVisemeTime = TimeSpan.Zero;

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
            Speaking ??= new List<Texture>();

            // If we have the last viseme and we're speaking, return it.
            if (_lastViseme != null && 
                _currentState == State.Speaking)
            {
                // That is, unless the time is greater than the viseme in between time
                if ((DateTime.Now.TimeOfDay - _lastVisemeTime) > TimeSpan.FromMilliseconds(_timeInbetweenVisemes))
                    _lastViseme = null;
                else
                    return _lastViseme;
            }
            
            if (IsSpeaking)
                _lastVisemeTime = DateTime.Now.TimeOfDay;

            // Get a random viseme, from the speaking list, only the not null ones
            // TODO: This should have a fast path for when we *don't* have any visemes, as this will
            //       always allocate a new list, which is kinda crap.
            var visemelist = Speaking.Where(x => x != null && x.Id != default)
                .ToList();

            Texture viseme = null;
            if (visemelist.Count > 0)
                viseme = visemelist[Random.Shared.Next(visemelist.Count)];

            _lastViseme = viseme;

            //Return the texture for the current state
            return _currentState switch
            {
                State.Idle => Idle,
                State.IdleBlink when IdleBlink != null => IdleBlink,
                State.Speaking when Speaking != null && Speaking.Any() && visemelist.Any() => viseme,
                State.SpeakingBlink when SpeakingBlink != null => SpeakingBlink,
                _ => Idle
            };
        }
    }
}
