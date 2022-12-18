using System.Collections.Generic;

namespace VTTiny.Data
{
    /// <summary>
    /// Asset database configuration data.
    /// </summary>
    public class AssetDatabaseConfig
    {
        public int LastId { get; set; }
        public IDictionary<int, TypedObjectConfig> Assets { get; set; }
    }
}
