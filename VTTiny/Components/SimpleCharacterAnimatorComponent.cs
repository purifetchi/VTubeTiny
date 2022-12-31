using System.Text.Json;
using VTTiny.Assets;
using VTTiny.Components.Data;
using VTTiny.Editor;
using VTTiny.Scenery;

namespace VTTiny.Components
{
    public class SimpleCharacterAnimatorComponent : Component
    {
        /// <summary>
        /// The state of the character
        /// </summary>
        public enum State
        {
            Idle,
            Blinking,
            Speaking
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
        /// Is the character speaking?
        /// </summary>
        public bool IsSpeaking { get; set; } = false;

        private Texture _idle;
        private Texture _blinking;
        private Texture _speaking;

        private StageTimer _blinkTimer;
        private StageTimer _blinkStartTimer;

        private bool _isBlinking;

        private TextureRendererComponent _renderer;

        /// <summary>
        /// Set a texture for a specific state
        /// </summary>
        /// <param name="texture">Handle to the texture.</param>
        /// <param name="state">The state.</param>
        public void SetTextureForState(Texture texture, State state)
        {
            switch (state)
            {
                case State.Idle:
                    _idle = texture;
                    break;
                case State.Blinking:
                    _blinking = texture;
                    break;
                case State.Speaking:
                    _speaking = texture;
                    break;
            }
        }

        public override void Start()
        {
            _blinkTimer = new StageTimer(Parent.OwnerStage);
            _blinkStartTimer = new StageTimer(Parent.OwnerStage);

            _renderer = GetComponent<TextureRendererComponent>();
        }

        public override void Update()
        {
            if (_blinking != null)
            {
                if (_blinkTimer.TimeElapsed >= BlinkEvery &&
                    !_isBlinking)
                {
                    _isBlinking = true;
                    _blinkStartTimer.SetNow();
                }

                if (_isBlinking &&
                    _blinkStartTimer.TimeElapsed >= BlinkLength)
                {
                    _isBlinking = false;
                    _blinkTimer.SetNow();
                }
            }
            else
            {
                // We never blink if the character has no blinking texture selected.
                _isBlinking = false;
            }

            if (IsSpeaking && !_isBlinking)
                _renderer.SetTexture(_speaking);

            else
                _renderer.SetTexture(_isBlinking ? _blinking : _idle);
        }

        public override void Destroy()
        {
            _renderer?.SetTexture(null);
        }

        internal override void InheritParametersFromConfig(JsonElement? parameters)
        {
            var config = JsonObjectToConfig<SimpleCharacterAnimatorConfig>(parameters);
            config.LoadStates(this);

            BlinkEvery = config.BlinkEvery;
            BlinkLength = config.BlinkLength;
        }

        internal override void RenderEditorGUI()
        {
            BlinkEvery = EditorGUI.DragFloat("Blink every", BlinkEvery);
            BlinkLength = EditorGUI.DragFloat("Blink length", BlinkLength);

            if (EditorGUI.AssetDropdown("Idle", Parent.OwnerStage.AssetDatabase, _idle, out Texture newIdle))
                _idle = newIdle;

            if (EditorGUI.AssetDropdown("Blinking", Parent.OwnerStage.AssetDatabase, _blinking, out Texture newBlinking))
                _blinking = newBlinking;

            if (EditorGUI.AssetDropdown("Speaking", Parent.OwnerStage.AssetDatabase, _speaking, out Texture newSpeaking))
                _speaking = newSpeaking;
        }

        protected override object PackageParametersIntoConfig()
        {
            return new SimpleCharacterAnimatorConfig
            {
                BlinkEvery = BlinkEvery,
                BlinkLength = BlinkLength,

                Idle = _idle?.ToAssetReference<Texture>(),
                Blinking = _blinking?.ToAssetReference<Texture>(),
                Speaking = _speaking?.ToAssetReference<Texture>()
            };
        }
    }
}
