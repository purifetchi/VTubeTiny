using System;
using System.Linq;
using System.Text.Json;
using NAudio.CoreAudioApi;
using VTTiny.Components.Data;
using VTTiny.Editor;
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
            DestroyWasapiContexts();

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

        /// <summary>
        /// Gets the current master peak level of the microphone.
        /// </summary>
        /// <returns>The peak level.</returns>
        private int Level()
        {
            return (int)Math.Round(_microphone.AudioMeterInformation.MasterPeakValue * 100 * Multiplier);
        }

        /// <summary>
        /// Destroys all WASAPI related contexts (WasapiCapture, Microphone).
        /// </summary>
        private void DestroyWasapiContexts()
        {
            if (_capture != null)
            {
                _capture.StopRecording();
                _capture.Dispose();

                _capture = null;
            }

            if (_microphone != null)
            {
                _microphone.Dispose();

                _microphone = null;
            }
        }

        public override void Start()
        {
            _character = GetComponent<SimpleCharacterAnimatorComponent>();
            _jumpTimer = new StageTimer(Parent.OwnerStage);

            SetMicrophoneByName(GetDefaultMicrophone());
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

                _basePos = Parent.Transform.LocalPosition;
                _jump = true;
            }

            if (_jump)
            {
                var clone = _basePos;
                clone.Y -= (int)(JumpHeight * (Math.Sin(_jumpTimer.TimeElapsed * JumpSpeedMultiplier)));

                Parent.Transform.LocalPosition = clone;

                if (_jumpTimer.TimeElapsed >= Math.PI / JumpSpeedMultiplier)
                {
                    Parent.Transform.LocalPosition = _basePos;
                    _jump = false;
                }
            }

            if (_character != null)
                _character.IsSpeaking = _talking;
        }

        public override void Destroy()
        {
            DestroyWasapiContexts();
        }

        internal override void InheritParametersFromConfig(JsonElement? parameters)
        {
            var config = JsonObjectToConfig<AudioResponsiveMovementConfig>(parameters);

            if (string.IsNullOrEmpty(config.Microphone))
                SetMicrophoneByName(GetDefaultMicrophone());

            else
                SetMicrophoneByName(config.Microphone);

            Threshold = config.Threshold;
            Multiplier = config.Multiplier;
            JumpHeight = config.JumpHeight;
            JumpSpeedMultiplier = config.JumpSpeedMultiplier;
        }

        internal override void RenderEditorGUI()
        {
            Threshold = EditorGUI.DragInt("Volume threshold", Threshold);
            Multiplier = EditorGUI.DragFloat("Multiplier", Multiplier);
            JumpHeight = EditorGUI.DragFloat("Jump height", JumpHeight);
            JumpSpeedMultiplier = EditorGUI.DragFloat("Jump speed multiplier", JumpSpeedMultiplier);

            EditorGUI.Text("Microphone level");
            EditorGUI.ReactiveProgressBar(Level(), Threshold, 100);
        }

        protected override object PackageParametersIntoConfig()
        {
            return new AudioResponsiveMovementConfig
            {
                Threshold = Threshold,
                Multiplier = Multiplier,
                JumpSpeedMultiplier = JumpSpeedMultiplier,
                JumpHeight = JumpHeight,
                Microphone = _microphone?.FriendlyName
            };
        }
    }
}
