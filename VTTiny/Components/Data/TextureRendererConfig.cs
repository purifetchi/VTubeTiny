using Raylib_cs;
using VTTiny.Assets;
using VTTiny.Assets.Management;

namespace VTTiny.Components.Data
{
    internal class TextureRendererConfig
    {
        public AssetReference<Texture>? Image { get; set; }
        public Color Tint { get; set; } = Color.WHITE;
        public float Rotation { get; set; } = 0f;
        public float Scale { get; set; } = 1f;
    }
}
