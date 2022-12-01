using System.Collections.Generic;
using Raylib_cs;

namespace VTTiny.Data
{
    /// <summary>
    /// Stage configuration data.
    /// </summary>
    public class Config
    {
        public Vector2Int Dimensions { get; set; } = new Vector2Int(800, 480);
        public Color ClearColor { get; set; } = new Color(0, 255, 0, 255);
        public int FPSLimit { get; set; } = 60;

        public IList<ActorConfig> Actors { get; set; }
    }
}
