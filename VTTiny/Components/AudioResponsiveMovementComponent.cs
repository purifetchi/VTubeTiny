using System;
using System.Linq;
using NAudio.CoreAudioApi;
using Newtonsoft.Json.Linq;
using VTTiny.Components.Data;
using VTTiny.Scenery;

namespace VTTiny.Components
{
    public class AudioResponsiveMovementComponent : Component
    {
        /// <summary>
        /// The threshold to detect audio.
        /// </summary>
        public int Threshold { get; set; } = 5;

        /// <summary>
        /// The multiplier to increase the power of the audio.
        /// </summary>
        public float Multiplier { get; set; } = 1.9f;

        /// <summary>
        /// The height to jump to.
        /// </summary>
        public float JumpHeight { get; set; } = 10f;

        /// <summary>
        /// The multiplier of the speed of the jump.
        /// </summary>
        public float JumpSpeedMultiplier { get; set; } = 10;

        private WasapiCapture _capture;
        private MMDevice _microphone;
        private bool _talking;
        private int _lastLevel = 0;

        private SimpleCharacterAnimatorComponent _character;
        private Vector2Int _basePos;
        private StageTimer _jumpTimer;
        private bool _jump;

        /// <summary>
        /// Set the microphone by its name.
        /// </summary>
        /// <param name="name">The microphone name.</param>
        public void SetMicrophoneByName(string name)
        {
            var deviceEnumerator = new MMDeviceEnumerator();

            var devices = deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);
            var device = devices?.FirstOrDefault(mic => mic.FriendlyName.Contains(name));
            _microphone = device;

            if (_microphone == null)
            {
                Console.WriteLine($"The microphone {name} does not exist!");
                return;
            }

            _capture = new WasapiCapture(_microphone);
            _capture.StartRecording();
        }

        /// <summary>
        /// Gets the default microphone's name.
        /// </summary>
        /// <returns>The default microphone's name.</returns>
        private string GetDefaultMicrophone()
        {
            var deviceEnumerator = new MMDeviceEnumerator();
            return deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Communications)?.FriendlyName;
        }

        private int Level()
        {
            return (int)Math.Round(_microphone.AudioMeterInformation.MasterPeakValue * 100 * Multiplier);
        }

        public override void Start()
        {
            _character = GetComponent<SimpleCharacterAnimatorComponent>();
            _basePos = Parent.Transform.LocalPosition;

            _jumpTimer = new StageTimer(Parent.OwnerStage);
        }

        public override void Update()
        {
            if (_microphone == null)
                return;

            var level = Level();
            _talking = _lastLevel >= Threshold && level >= Threshold;

            _lastLevel = level;

            if (_talking &&
                !_jump)
            {
                _jumpTimer.SetNow();
                _jump = true;
            }

            if (_jump)
            {
                var clone = _basePos;
                clone.Y -= (int)(JumpHeight * (Math.Sin(_jumpTimer.TimeElapsed * JumpSpeedMultiplier)));

                Parent.Transform.LocalPosition = clone;

                if (_jumpTimer.TimeElapsed >= Math.PI / JumpSpeedMultiplier)
                    _jump = false;
            }

            _character.IsSpeaking = _talking;
        }

        public override void Destroy()
        {
            if (_capture != null)
            {
                _capture.StopRecording();
                _capture.Dispose();
            }

            if (_microphone != null)
            {
                _microphone.Dispose();
            }
        }

        internal override void InheritParametersFromConfig(JObject parameters)
        {
            var config = parameters.ToObject<AudioResponsiveMovementConfig>();
            
            if (string.IsNullOrEmpty(config.Microphone))
                SetMicrophoneByName(GetDefaultMicrophone());

            else
                SetMicrophoneByName(config.Microphone);
        }
    }
}
