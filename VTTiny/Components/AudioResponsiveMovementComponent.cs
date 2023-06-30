using System;
using System.Text.Json;
using ImGuiNET;
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
        
        /// <summary>
        /// If Object can wiggle
        /// </summary>
        public bool CanWiggle { get; set; } = false;
        
        /// <summary>
        /// Wiggle Amount
        /// </summary>
        public float WiggleSpeed { get; set; } = 5f;
        
        /// <summary>
        /// Wiggle Speed Multiplier
        /// </summary>
        public float WiggleSpeedMultiplier { get; set; } = 3;

        /// <summary>
        /// Should the amount of jumps be limited?
        /// </summary>
        public bool LimitJumps { get; set; } = false;

        /// <summary>
        /// The maximum amount of jumps.
        /// </summary>
        public int MaxJumps { get; set; } = 1;

        /// <summary>
        /// The device we're listening to.
        /// </summary>
        private IListenableDevice _device;

        protected bool _talking;
        protected int _lastLevel = 0;
        protected int _jumpCount = 0;

        protected ISpeakingAwareComponent[] _components;
        protected Vector2Int _basePos;
        protected StageTimer _jumpTimer;
        protected bool _jump;

        /// <summary>
        /// Sets the listenable device for this component.
        /// </summary>
        /// <param name="device">The device.</param>
        public void SetListenableDevice(IListenableDevice device)
        {
            if (device == _device)
                return;

            _device?.Stop();

            if (device == null)
                return;

            _device = device;
            _device.Listen();
        }

        /// <summary>
        /// Gets the current master peak level of the microphone.
        /// </summary>
        /// <returns>The peak level.</returns>
        private int Level()
        {
            if (_device == null)
                return 0;

            return (int)Math.Round(_device.Level * 100 * Multiplier);
        }

        public override void Start()
        {
            _components = GetComponents<ISpeakingAwareComponent>();
            _jumpTimer = new StageTimer(Parent.OwnerStage);

            SetListenableDevice(ListenableDeviceHelper.GetFirstDefaultDevice());
        }

        public override void Update()
        {
            if (_device == null)
                return;

            var level = Level();
            _talking = _lastLevel >= Threshold && level >= Threshold;

            _lastLevel = level;

            if (_talking)
            {
                if (!_jump)
                {
                    _jumpTimer.SetNow();

                    _basePos = Parent.Transform.LocalPosition;

                    _jump = !LimitJumps || (LimitJumps && _jumpCount < MaxJumps);
                    _jumpCount++;
                }
            }
            else
            {
                _jumpCount = 0;
            }

            foreach (var component in _components)
                component.IsSpeaking = _talking;

            if (_jump)
            {
                var clone = _basePos;
                clone.Y -= (int)(JumpHeight * (Math.Sin(_jumpTimer.TimeElapsed * JumpSpeedMultiplier)));
                
                //TODO: Different algorithm for wiggling
                if (CanWiggle)
                    clone.X += (int)(WiggleSpeed * (Math.Sin(_jumpTimer.TimeElapsed * WiggleSpeedMultiplier)));

                Parent.Transform.LocalPosition = clone;

                if (_jumpTimer.TimeElapsed >= Math.PI / JumpSpeedMultiplier)
                {
                    Parent.Transform.LocalPosition = _basePos;
                    _jump = false;
                }
            }
        }

        public override void Destroy()
        {
            _device?.Stop();
        }

        public override void InheritParametersFromConfig(JsonElement? parameters)
        {
            var config = JsonObjectToConfig<AudioResponsiveMovementConfig>(parameters);

            if (string.IsNullOrEmpty(config.Microphone))
                SetListenableDevice(ListenableDeviceHelper.GetFirstDefaultDevice());  
            else
                SetListenableDevice(ListenableDeviceHelper.GetListenableDeviceByName(config.Microphone));

            Threshold = config.Threshold;
            Multiplier = config.Multiplier;
            JumpHeight = config.JumpHeight;
            JumpSpeedMultiplier = config.JumpSpeedMultiplier;
            LimitJumps = config.LimitJumps;
            MaxJumps = config.MaxJumps;
            CanWiggle = config.CanWiggle;
        }

        public override void RenderEditorGUI()
        {
            Threshold = EditorGUI.DragInt("Volume threshold", Threshold);
            Multiplier = EditorGUI.DragFloat("Multiplier", Multiplier);
            JumpHeight = EditorGUI.DragFloat("Jump height", JumpHeight);
            JumpSpeedMultiplier = EditorGUI.DragFloat("Jump speed multiplier", JumpSpeedMultiplier);
            
            CanWiggle = EditorGUI.Checkbox("Can Wiggle?", CanWiggle);

            if (CanWiggle)
            {
                WiggleSpeed = EditorGUI.DragFloat("Wiggle Speed", WiggleSpeed);
                WiggleSpeedMultiplier = EditorGUI.DragFloat("Wiggle Speed Multiplier", WiggleSpeedMultiplier);
            }

            LimitJumps = EditorGUI.Checkbox("Limit jumps?", LimitJumps);
            if (LimitJumps)
                MaxJumps = EditorGUI.DragInt("Max jumps", MaxJumps);

            ImGui.Separator();
            if (EditorGUI.ListenableDeviceDropdown("Device", _device, out IListenableDevice newDevice))
                SetListenableDevice(newDevice);

            EditorGUI.Text("Device level");
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
                Microphone = _device?.Name,

                CanWiggle = CanWiggle,

                LimitJumps = LimitJumps,
                MaxJumps = MaxJumps
            };
        }
    }
}
