using System.Text.Json;
using VTTiny.Components.Animator;
using VTTiny.Components.Data;

namespace VTTiny.Components
{
    public class SimpleCharacterAnimatorComponent : Component
    {
        /// <summary>
        /// Is the character speaking?
        /// </summary>
        public bool IsSpeaking { get; set; } = false;

        /// <summary>
        /// The current animator package.
        /// </summary>
        public AnimatorCharacter Character { get; set; }

        private TextureRendererComponent _renderer;

        public override void Start()
        {
            Character = new AnimatorCharacter();
            _renderer = GetComponent<TextureRendererComponent>();
        }

        public override void Update()
        {
            Character.IsSpeaking = IsSpeaking;
            Character.Update(Parent.OwnerStage.Time);

            _renderer?.SetTexture(Character.GetCurrentStateTexture());
        }

        public override void Destroy()
        {
            _renderer?.SetTexture(null);
        }

        internal override void InheritParametersFromConfig(JsonElement? parameters)
        {
            var config = JsonObjectToConfig<SimpleCharacterAnimatorConfig>(parameters);
            Character = config.ConstructCharacterPackage(Parent.OwnerStage.AssetDatabase);
        }

        internal override void RenderEditorGUI()
        {
            Character.DrawEditorGUI(Parent.OwnerStage.AssetDatabase);
        }

        protected override object PackageParametersIntoConfig()
        {
            return SimpleCharacterAnimatorConfig.FromCharacter(Character);
        }
    }
}
