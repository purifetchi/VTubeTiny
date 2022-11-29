using System.Collections.Generic;
using VTTiny.Data;

namespace VTTiny.Scenery
{
    public partial class Stage
    {
        /// <summary>
        /// Packages this stage into a config class, that can then be exported.
        /// </summary>
        /// <returns>The Config class containing information about the stage.</returns>
        internal Config PackageStageIntoConfig()
        {
            var config = new Config
            {
                ClearColor = ClearColor,
                Dimensions = Dimensions
            };

            var actorList = new List<ActorConfig>();
            foreach (var actor in _actors)
                actorList.Add(actor.PackageActorIntoConfig());

            config.Actors = actorList;

            return config;
        }
    }
}
