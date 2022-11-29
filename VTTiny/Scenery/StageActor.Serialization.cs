using System.Collections.Generic;
using VTTiny.Data;

namespace VTTiny.Scenery
{
    public partial class StageActor
    {
        /// <summary>
        /// Packages this stage actor into its config file for exporting.
        /// </summary>
        /// <returns>The ActorConfig containing information about this actor.</returns>
        internal ActorConfig PackageActorIntoConfig()
        {
            var config = new ActorConfig
            {
                Name = Name,
                ParentActorName = ParentActor?.Name ?? string.Empty,
                Position = Transform.LocalPosition
            };

            var componentList = new List<ComponentConfig>();
            foreach (var component in _components)
                componentList.Add(component.PackageComponentIntoConfig());

            config.Components = componentList;

            return config;
        }
    }
}
