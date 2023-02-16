using VTTiny.Assets;
using VTTiny.Assets.Management;
using VTTiny.Data;

namespace VTTiny.Scenery
{
    public sealed partial class Background
    {
        /// <summary>
        /// Constructs a new background from the background config.
        /// </summary>
        /// <param name="config">The background config.</param>
        /// <param name="database">The database to resolve the background texture asset from.</param>
        /// <returns>The background.</returns>
        internal static Background FromConfig(BackgroundConfig config, AssetDatabase database)
        {
            if (config == null)
                return Default();

            return new Background
            {
                Color = config.Color,
                Texture = config.Texture?.Resolve(database),
                DrawTexture = config.DrawTexture,
                TextureMode = config.TextureMode,
                TilingScale = config.TilingScale,
            };
        }

        /// <summary>
        /// Packages this background into a config.
        /// </summary>
        /// <returns>The background config.</returns>
        internal BackgroundConfig PackageIntoConfig()
        {
            return new BackgroundConfig
            {
                Color = Color,
                Texture = Texture?.ToAssetReference<Texture>(),
                DrawTexture = DrawTexture,
                TextureMode = TextureMode,
                TilingScale = TilingScale,
            };
        }
    }
}
