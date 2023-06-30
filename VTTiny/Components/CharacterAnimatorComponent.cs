using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using ImGuiNET;
using Raylib_cs;
using VTTiny.Base;
using VTTiny.Components.Animator;
using VTTiny.Components.Data;
using VTTiny.Editor;

namespace VTTiny.Components;

[DependsOnComponent(typeof(TextureRendererComponent))]
public class CharacterAnimatorComponent : Component, 
    ISpeakingAwareComponent
{
    /// <inheritdoc/>
    public bool IsSpeaking { get; set; }

    /// <summary>
    /// All of the characters this animator has.
    /// </summary>
    private List<AnimatorState> _states;

    /// <summary>
    /// The currently operated on character.
    /// </summary>
    private CharacterAnimator _animator;

    /// <summary>
    /// The current renderer.
    /// </summary>
    private TextureRendererComponent _renderer;

    /// <summary>
    /// Tries to find the default state and sets it.
    /// </summary>
    public void FindAndSetDefaultState()
    {
        _animator.State = _states.First(state => state.IsDefaultState);
    }

    /// <inheritdoc/>
    public override void Start()
    {
        _animator = new();
        _states = new();

        _renderer = GetComponent<TextureRendererComponent>();
    }

    /// <inheritdoc/>
    public override void Update()
    {
        foreach (var state in _states)
        {
            if (!Raylib.IsKeyPressed(state.Key))
                continue;

            _animator.State = state;
            break;
        }

        _animator.IsSpeaking = IsSpeaking;
        _animator.Update(Parent.OwnerStage.Time);

        _renderer?.SetTexture(_animator.GetCurrentStateTexture());
    }

    /// <inheritdoc/>
    public override void InheritParametersFromConfig(JsonElement? parameters)
    {
        var config = JsonObjectToConfig<CharacterAnimatorConfig>(parameters);

        foreach (var stateConfig in config.States)
        {
            var state = new AnimatorState(Parent.OwnerStage.AssetDatabase);
            stateConfig.Into(state);

            _states.Add(state);
        }

        FindAndSetDefaultState();
    }

    /// <inheritdoc/>
    protected override object PackageParametersIntoConfig()
    {
        var stateConfigs = _states.Select(state => state.PackageIntoConfig()).ToList();

        return new CharacterAnimatorConfig
        {
            States = stateConfigs
        };
    }

    /// <inheritdoc/>
    public override void RenderEditorGUI()
    {
        EditorGUI.Text("States");

        foreach (var character in _states)
            character.DrawEditorGUI(Parent.OwnerStage);

        if (!ImGui.Button("Add Character State"))
            return;

        var state = new AnimatorState(Parent.OwnerStage.AssetDatabase);
        _states.Add(state);

        if (_states.Count != 1)
            return;

        state.IsDefaultState = true;
        FindAndSetDefaultState();
    }
}