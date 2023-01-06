namespace VTTiny.Components.Data
{
    internal class AudioResponsiveMovementConfig
    {
        public string Microphone { get; set; }

        public int Threshold { get; set; } = 5;

        public float Multiplier { get; set; } = 1.9f;

        public float JumpHeight { get; set; } = 10f;

        public float JumpSpeedMultiplier { get; set; } = 10;

        public bool LimitJumps { get; set; } = false;

        public int MaxJumps { get; set; } = 1;
    }
}
