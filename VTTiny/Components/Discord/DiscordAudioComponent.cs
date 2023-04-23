using System;
using System.Text.Json;
using System.Threading.Tasks;
using VTTiny.Editor;
using VTTiny.Plugin.Discord;
using VTTiny.Plugin.Discord.Services;
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
        {
            component.IsSpeaking = _talking;
        }

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

    public override void Destroy()
    {
        
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
        var config = JsonObjectToConfig<DiscordAudioConfig>(parameters);
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
        
        return new DiscordAudioConfig
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

public class DiscordAudioConfig
{
    public string Name { get; set; } = string.Empty;

    public float Multiplier { get; set; } = 1.9f;

    public float JumpHeight { get; set; } = 10f;

    public float JumpSpeedMultiplier { get; set; } = 10;

    public bool LimitJumps { get; set; } = false;

    public int MaxJumps { get; set; } = 1;
        
    public float WiggleSpeed { get; set; } = 1f;
    public bool CanWiggle { get; set; } = false;
}

//Singleton
/// <summary>
/// Q: Why is this a singleton?
/// A: Because we want to have a single instance of the Discord bot running in the background
/// Q: Why should we have a single instance of the Discord bot running in the background?
/// A: Otherwise discord will have multiple bots running and we will have to deal with multiple connections
/// -  Making discord kinda mad, commands would fire ONCE to a single connection and not to all of them 
/// </summary>
public class DiscordAudioSingleton : IDisposable
{
    public DiscordBot _bot;
    private static DiscordAudioSingleton _instance;
    private static readonly object _instanceLock = new object();
    public static DiscordAudioSingleton Instance
    {
        get
        {
            lock (_instanceLock)
            {
                if (_instance == null)
                {
                    _instance = new DiscordAudioSingleton();
                    _instance.Start(); //only start the bot when we need it
                }
            }
            return _instance;
        }
    }
    public bool IsUserSpeaking(string userName)
    {
        if (_bot == null || !_bot.IsRunning)
            return false;
        return NameProvider.Instance.IsUserSpeaking(userName);
    }
    private void Start()
    {
        
        //Will dispose of this singleton when discord bot closes
        Task.Run(() =>
        {
            _bot = new DiscordBot();
            _bot.RunAsync().Wait();
        });
    }
    

    public void Dispose()
    {
        //Dispose of this singleton
        _bot.StopAsync().Wait();
        _bot = null;
        _instance = null;
    }
}


