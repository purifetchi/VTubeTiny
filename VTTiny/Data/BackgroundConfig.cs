using Raylib_cs;
using VTTiny.Assets;
using VTTiny.Assets.Management;
using VTTiny.Scenery;

namespace VTTiny.Data
{
    /// <summary>
    /// Configuration data for the background.
    /// </summary>
    public class BackgroundConfig
    {
        public Color Color { get; set; } = new(0, 255, 0, 255);
        public AssetReference<Texture>? Texture { get; set; }
        public bool DrawTexture { get; set; } = false;
        public BackgroundTextureMode TextureMode { get; set; } = BackgroundTextureMode.Stretch;
        public float TilingScale { get; set; } = 1f;
    }
}
