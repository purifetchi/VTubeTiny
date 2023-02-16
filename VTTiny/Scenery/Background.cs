using Raylib_cs;
using VTTiny.Assets;

namespace VTTiny.Scenery
{
    /// <summary>
    /// Settings for a stage's background.
    /// </summary>
    public sealed partial class Background
    {
        /// <summary>
        /// The background color to draw, when no texture is specified.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// The background's texture.
        /// </summary>
        public Texture Texture { get; set; }

        /// <summary>
        /// Should a texture be drawn instead of the color.
        /// </summary>
        public bool DrawTexture { get; set; }

        /// <summary>
        /// The mode for the background texture.
        /// </summary>
        public BackgroundTextureMode TextureMode { get; set; }

        /// <summary>
        /// The tiling scale.
        /// </summary>
        public float TilingScale { get; set; } = 1f;

        /// <summary>
        /// Constructs the default background.
        /// </summary>
        /// <returns>The default background.</returns>
        public static Background Default()
        {
            return new Background
            {
                Color = new(0, 255, 0, 255),
            };
        }

        /// <summary>
        /// Renders the background.
        /// </summary>
        public void Render(Vector2Int dimensions)
        {
            if (!DrawTexture)
                return;

            if (Texture == null)
                return;

            var src = new Rectangle(0, 0, Texture.Width, Texture.Height);
            var dest = new Rectangle(0, 0, dimensions.X, dimensions.Y);

            switch (TextureMode)
            {
                case BackgroundTextureMode.Stretch:
                    Raylib.DrawTexturePro(Texture.BackingTexture, src, dest, Vector2Int.Zero, 0f, Color.WHITE);
                    break;

                case BackgroundTextureMode.Tile:
                    Raylib.DrawTextureTiled(Texture.BackingTexture, src, dest, Vector2Int.Zero, 0f, TilingScale, Color.WHITE);
                    break;
            }
        }
    }
}
