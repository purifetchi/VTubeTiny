using System.Text.Json;
using Raylib_cs;
using VTTiny.Base;
using VTTiny.Components.Data;
using VTTiny.Editor;
using VTTiny.Extensions;
using VTTiny.Scenery;

namespace VTTiny.Components
{
    /// <summary>
    /// The component that switches the actor's color when they're speaking and not.
    /// </summary>
    [DependsOnComponent(typeof(TextureRendererComponent))]
    public class SpeakingColorChangerComponent : Component, ISpeakingAwareComponent
    {
        public bool IsSpeaking { get; set; } = false;

        /// <summary>
        /// The default tint for when the user is not speaking.
        /// </summary>
        public Color DefaultTint { get; set; } = Color.WHITE;

        /// <summary>
        /// The tint to use for the renderer when the user is speaking.
        /// </summary>
        public Color SpeakingTint { get; set; } = Color.WHITE;

        /// <summary>
        /// The time to interpolate between the tints when we switch states.
        /// </summary>
        public float LerpTime { get; set; } = 0.1f;

        private TextureRendererComponent _renderer;
        private StageTimer _lerpTimer;
        private bool _wasSpeaking = false;

        private Color _begin;
        private Color _goal;

        public override void Start()
        {
            _renderer = GetComponent<TextureRendererComponent>();
            if (_renderer != null)
                _renderer.Tint = DefaultTint;

            _lerpTimer = new(Parent.OwnerStage);
        }

        public override void Update()
        {
            if (_renderer == null)
                return;

            if (_wasSpeaking ^ IsSpeaking)
                _lerpTimer.SetNow();

            _begin = _renderer.Tint;
            _goal = IsSpeaking ? SpeakingTint : DefaultTint;

            var color = _begin.Lerp(_goal, (float)_lerpTimer.TimeElapsed / LerpTime);

            _wasSpeaking = IsSpeaking;
            _renderer.Tint = color;
        }

        internal override void RenderEditorGUI()
        {
            DefaultTint = EditorGUI.ColorEdit("Default tint", DefaultTint);
            SpeakingTint = EditorGUI.ColorEdit("Speaking tint", SpeakingTint);
            LerpTime = EditorGUI.DragFloat("Interpolation time", LerpTime, 0.01f);

            if (LerpTime <= 0)
                LerpTime = float.Epsilon;
        }

        protected override object PackageParametersIntoConfig()
        {
            return new SpeakingColorChangerConfig
            {
                DefaultTint = DefaultTint,
                SpeakingTint = SpeakingTint,
                LerpTime = LerpTime
            };
        }

        internal override void InheritParametersFromConfig(JsonElement? parameters)
        {
            var config = JsonObjectToConfig<SpeakingColorChangerConfig>(parameters);

            DefaultTint = config.DefaultTint;
            SpeakingTint = config.SpeakingTint;
            LerpTime = config.LerpTime;
        }
    }
}
