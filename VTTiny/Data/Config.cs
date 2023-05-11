using System.Collections.Generic;
using VTTiny.Scripting.Serialization;

namespace VTTiny.Data
{
    /// <summary>
    /// Stage configuration data.
    /// </summary>
    public class Config
    {
        public Vector2Int Dimensions { get; set; } = new(800, 480);
        public BackgroundConfig Background { get; set; }
        public int FPSLimit { get; set; } = 60;
        public bool BroadcastViaSpout { get; set; } = false;

        public IList<ActorConfig> Actors { get; set; }
        public AssetDatabaseConfig AssetDatabase { get; set; }
        public StageGraphConfig StageGraph { get; set; }
    }
}
