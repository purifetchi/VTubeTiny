using System.Collections.Generic;

namespace VTTiny.Data
{
    /// <summary>
    /// Asset database configuration data.
    /// </summary>
    public class AssetDatabaseConfig
    {
        public int LastId { get; set; }
        public IDictionary<int, AssetConfig> Assets { get; set; }
    }
}
