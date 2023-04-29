namespace VTTiny.Components.Data
{
    internal class CharacterMoverConfig
    {
        public float JumpHeight { get; set; } = 10f;

        public float JumpSpeedMultiplier { get; set; } = 10;

        public bool LimitJumps { get; set; } = false;

        public int MaxJumps { get; set; } = 1;
    }
}
