using System.Collections.Generic;

namespace VTTiny.Data
{
    /// <summary>
    /// Actor configuration data.
    /// </summary>
    public class ActorConfig
    {
        public string Name { get; set; } = "StageActor";
        public string ParentActorName { get; set; } = "";
        public Vector2Int Position { get; set; } = new Vector2Int(0, 0);

        public IList<ComponentConfig> Components { get; set; }
    }
}
