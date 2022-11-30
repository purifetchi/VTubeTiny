using Raylib_cs;

namespace VTTiny.Components.Data
{
    internal class TextureRendererConfig
    {
        public string Image { get; set; }
        public Color Tint { get; set; } = Color.WHITE;
        public float Rotation { get; set; } = 0f;
        public float Scale { get; set; } = 1f;
    }
}
