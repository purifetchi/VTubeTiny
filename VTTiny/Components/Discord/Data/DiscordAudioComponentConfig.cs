using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTTiny.Components.Discord.Data;

public class DiscordAudioComponentConfig
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