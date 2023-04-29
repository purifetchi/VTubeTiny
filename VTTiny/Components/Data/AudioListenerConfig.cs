namespace VTTiny.Components.Data
{
    internal class AudioListenerConfig
    {
        public string Microphone { get; set; }

        public int Threshold { get; set; } = 5;

        public float Multiplier { get; set; } = 1.9f;
    }
}
