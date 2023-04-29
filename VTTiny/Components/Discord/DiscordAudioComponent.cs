using System;
using System.Text.Json;
using VTTiny.Components.Discord.BotIntegration;
using VTTiny.Components.Discord.Data;
using VTTiny.Editor;
using VTTiny.Scenery;

namespace VTTiny.Components.Discord;

/// <summary>
/// This is the component that will be used to handle the Discord bot with audio
/// This bot will connect with a process running in the background
/// </summary>
public class DiscordAudioComponent : AudioResponsiveMovementComponent
{
    /// <summary>
    /// Public name of the user
    /// </summary>
    public string Name { get; set; } = string.Empty;

    public override void Start()
    {
        _components = GetComponents<ISpeakingAwareComponent>();
        _jumpTimer = new StageTimer(Parent.OwnerStage);
    }

    public override void Update()
    {
        _talking = DiscordAudioSingleton.Instance.IsUserSpeaking(Name);

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

        if (!_jump) return;
        var clone = _basePos;
        clone.Y -= (int)(JumpHeight * (Math.Sin(_jumpTimer.TimeElapsed * JumpSpeedMultiplier)));
                
        if (CanWiggle)
            clone.X += (int)(WiggleSpeed * (Math.Sin(_jumpTimer.TimeElapsed * WiggleSpeedMultiplier)));

        Parent.Transform.LocalPosition = clone;

        if (!(_jumpTimer.TimeElapsed >= Math.PI / JumpSpeedMultiplier)) return;
        Parent.Transform.LocalPosition = _basePos;
        _jump = false;
    }

    public override void RenderEditorGUI()
    {
        Name = EditorGUI.InputText("Name/Discord ID", Name);
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
    }

    public override void InheritParametersFromConfig(JsonElement? parameters)
    {
        var config = JsonObjectToConfig<DiscordAudioComponentConfig>(parameters);
        Name = config.Name;
        Multiplier = config.Multiplier;
        JumpHeight = config.JumpHeight;
        JumpSpeedMultiplier = config.JumpSpeedMultiplier;
        LimitJumps = config.LimitJumps;
        MaxJumps = config.MaxJumps;
        WiggleSpeed = config.WiggleSpeed;
        CanWiggle = config.CanWiggle;
        
        base.InheritParametersFromConfig(parameters);
    }

    protected override object PackageParametersIntoConfig()
    {
        return new DiscordAudioComponentConfig
        {
            Name = Name,
            Multiplier = Multiplier,
            JumpSpeedMultiplier = JumpSpeedMultiplier,
            JumpHeight = JumpHeight,
            LimitJumps = LimitJumps,
            MaxJumps = MaxJumps,
            WiggleSpeed = WiggleSpeed,
            CanWiggle = CanWiggle
        };
    }
}