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
        public Vector2Int Position { get; set; } = new(0, 0);
        public float Rotation { get; set; } = 0;
        public bool AllowRendering { get; set; } = true;

        public IList<TypedObjectConfig> Components { get; set; }
    }
}
