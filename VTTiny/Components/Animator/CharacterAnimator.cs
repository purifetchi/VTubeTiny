using System;
using System.Linq;
using VTTiny.Assets;

namespace VTTiny.Components.Animator;

/// <summary>
/// A class responsible for animating character assets.
/// </summary>
public class CharacterAnimator
{
    /// <summary>
    /// The current animator state we're animating.
    /// </summary>
    public AnimatorState State { get; set; }

    /// <summary>
    /// The character of the current state.
    /// </summary>
    private Character Character => State?.Character;

    /// <summary>
    /// Whether the character is currently blinking.
    /// </summary>
    private Character.State _currentState = Character.State.Idle;

    /// <summary>
    /// Whether the character is speaking right now.
    /// </summary>
    public bool IsSpeaking { get; set; } = false;

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
        if (Character == null)
            return;

        var isBlinking = (_currentState == Character.State.IdleBlink) ||
                         (_currentState == Character.State.SpeakingBlink);

        var delta = time - _lastBlinkAction;
        if (isBlinking)
        {
            if (delta > Character.BlinkLength)
            {
                isBlinking = false;
                _lastBlinkAction = time;
            }
        }
        else
        {
            if (delta > Character.BlinkEvery)
            {
                isBlinking = true;
                _lastBlinkAction = time;
            }
        }

        if (isBlinking)
            _currentState = IsSpeaking ? Character.State.SpeakingBlink : Character.State.IdleBlink;
        else
            _currentState = IsSpeaking ? Character.State.Speaking : Character.State.Idle;
    }

    /// <summary>
    /// Gets the current texture for the state.
    /// </summary>
    /// <returns>The texture.</returns>
    public Texture GetCurrentStateTexture()
    {
        if (Character == null)
            return null;

        // If we have the last viseme and we're speaking, return it.
        if (_lastViseme != null &&
            _currentState == Character.State.Speaking)
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
        var visemelist = Character.Speaking?
            .Where(x => x != null && x.Id != default)
            .ToList();

        Texture viseme = null;
        if (visemelist != null && visemelist.Count > 0)
            viseme = visemelist[Random.Shared.Next(visemelist.Count)];

        _lastViseme = viseme;

        // Return the texture for the current state
        return _currentState switch
        {
            Character.State.Idle => Character.Idle,
            Character.State.IdleBlink when Character.IdleBlink != null => Character.IdleBlink,
            Character.State.Speaking when visemelist.Any() => viseme,
            Character.State.SpeakingBlink when Character.SpeakingBlink != null => Character.SpeakingBlink,
            _ => Character.Idle
        };
    }
}