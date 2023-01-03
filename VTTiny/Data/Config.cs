using System.Collections.Generic;
using Raylib_cs;

namespace VTTiny.Data
{
    /// <summary>
    /// Stage configuration data.
    /// </summary>
    public class Config
    {
        public Vector2Int Dimensions { get; set; } = new(800, 480);
        public Color ClearColor { get; set; } = new(0, 255, 0, 255);
        public int FPSLimit { get; set; } = 60;
        public bool BroadcastViaSpout { get; set; } = false;

        public IList<ActorConfig> Actors { get; set; }
        public AssetDatabaseConfig AssetDatabase { get; set; }
    }
}
