using Raylib_cs;

namespace VTTiny.Components.Data
{
    internal class SpeakingColorChangerConfig
    {
        public Color DefaultTint { get; set; } = Color.WHITE;

        public Color SpeakingTint { get; set; } = Color.WHITE;

        public float LerpTime { get; set; } = 0.1f;
    }
}
