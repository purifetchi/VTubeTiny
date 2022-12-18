using VTTiny.Data;

namespace VTTiny.Assets.Management
{
    public partial class AssetDatabase
    {
        /// <summary>
        /// Load assets from a config.
        /// </summary>
        /// <param name="config">The config to load from.</param>
        public void LoadConfig(Config config)
        {

        }

        /// <summary>
        /// Packages this asset database into a config.
        /// </summary>
        /// <returns>The asset database config.</returns>
        public AssetDatabaseConfig PackageIntoConfig()
        {
            return new AssetDatabaseConfig();
        }
    }
}
