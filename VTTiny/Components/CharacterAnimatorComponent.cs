using System.Collections.Generic;
using System.Text.Json;
using ImGuiNET;
using Raylib_cs;
using VTTiny.Components.Animator;
using VTTiny.Components.Animator.Data;
using VTTiny.Components.Data;
using VTTiny.Editor;

namespace VTTiny.Components
{
    public class CharacterAnimatorComponent : Component, ISpeakingAwareComponent
    {
        public bool IsSpeaking { get; set; }

        /// <summary>
        /// All of the characters this animator has.
        /// </summary>
        private List<AnimatorState> _states;

        /// <summary>
        /// The currently operated on character.
        /// </summary>
        private AnimatorCharacter _currentCharacter;

        /// <summary>
        /// The current renderer.
        /// </summary>
        private TextureRendererComponent _renderer;

        /// <summary>
        /// Tries to find the default state and sets it.
        /// </summary>
        public void FindAndSetDefaultState()
        {
            _currentCharacter = _states.Find(state => state.IsDefaultState)?.Character;
        }

        public override void Start()
        {
            _states = new();
            _renderer = GetComponent<TextureRendererComponent>();
        }

        public override void Update()
        {
            foreach (var state in _states)
            {
                if (Raylib.IsKeyPressed(state.Key))
                {
                    _currentCharacter = state.Character;
                    break;
                }
            }

            if (_currentCharacter != null)
            {
                _currentCharacter.IsSpeaking = IsSpeaking;
                _currentCharacter.Update(Parent.OwnerStage.Time);
            }

            _renderer?.SetTexture(_currentCharacter?.GetCurrentStateTexture());
        }

        internal override void InheritParametersFromConfig(JsonElement? parameters)
        {
            var config = JsonObjectToConfig<CharacterAnimatorConfig>(parameters);

            foreach (var stateConfig in config.States)
                _states.Add(stateConfig.ToAnimatorState(Parent.OwnerStage.AssetDatabase));

            FindAndSetDefaultState();
        }

        protected override object PackageParametersIntoConfig()
        {
            var stateConfigs = new List<AnimatorStateConfig>();

            foreach (var state in _states)
                stateConfigs.Add(state.PackageIntoConfig());

            return new CharacterAnimatorConfig
            {
                States = stateConfigs
            };
        }

        internal override void RenderEditorGUI()
        {
            EditorGUI.Text("States");

            foreach (var character in _states)
                character.DrawEditorGUI(Parent.OwnerStage);

            if (ImGui.Button("Add Character State"))
            {
                var state = new AnimatorState();
                _states.Add(state);

                if (_states.Count == 1)
                {
                    state.IsDefaultState = true;
                    FindAndSetDefaultState();
                }
            }
        }
    }
}
