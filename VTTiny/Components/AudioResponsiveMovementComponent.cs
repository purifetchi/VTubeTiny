using System;
using System.Text.Json;
using ImGuiNET;
using NAudio.CoreAudioApi;
using VTTiny.Audio;
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
        /// Sets the microphone for this component.
        /// </summary>
        /// <param name="microphone">The microphone.</param>
        public void SetMicrophone(MMDevice microphone)
        {
            DestroyWasapiContexts();
            _microphone = microphone;

            if (_microphone == null)
                return;

            _capture = new WasapiCapture(_microphone);
            _capture.StartRecording();
        }

        /// <summary>
        /// Gets the current master peak level of the microphone.
        /// </summary>
        /// <returns>The peak level.</returns>
        private int Level()
        {
            if (_microphone == null)
                return 0;

            return (int)Math.Round(_microphone.AudioMeterInformation.MasterPeakValue * 100 * Multiplier);
        }

        /// <summary>
        /// Destroys all WASAPI related contexts (WasapiCapture, Microphone).
        /// </summary>
        private void DestroyWasapiContexts()
        {
            _capture?.StopRecording();
            _capture?.Dispose();

            _capture = null;
        }

        public override void Start()
        {
            _character = GetComponent<SimpleCharacterAnimatorComponent>();
            _jumpTimer = new StageTimer(Parent.OwnerStage);

            SetMicrophone(MicrophoneHelper.GetDefaultMicrophone());
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
                SetMicrophone(MicrophoneHelper.GetDefaultMicrophone());
            else
                SetMicrophone(MicrophoneHelper.GetMicrophoneByName(config.Microphone));

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

            ImGui.Separator();
            if (EditorGUI.MicrophoneDropdown("Microphone", _microphone, out MMDevice newMic))
                SetMicrophone(newMic);

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
