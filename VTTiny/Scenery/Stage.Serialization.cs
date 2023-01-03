using System;
using System.Collections.Generic;
using VTTiny.Data;

namespace VTTiny.Scenery
{
    public partial class Stage
    {
        /// <summary>
        /// Creates actors from the actor config list.
        /// </summary>
        /// <param name="actorConfigs">The actor config list.</param>
        private void CreateActorsFromConfigList(IList<ActorConfig> actorConfigs)
        {
            if (actorConfigs == null)
                return;

            foreach (var config in actorConfigs)
            {
                var actor = CreateActor(config.Name);
                actor.Transform.LocalPosition = config.Position;
                actor.AllowRendering = config.AllowRendering;

                // Try to set the parent of this actor
                var parent = FindActor(config.ParentActorName);
                if (!actor.TryReparent(parent))
                    Console.WriteLine($"Reparenting actor '{actor.Name}' to actor '{parent?.Name}' failed. (Is there a cyclic reparent somewhere?");

                actor.BuildComponentsFromConfig(config.Components);

                Console.WriteLine($"Actor {actor.Name} instantiated.");
            }
        }

        /// <summary>
        /// Packages this stage into a config class, that can then be exported.
        /// </summary>
        /// <returns>The Config class containing information about the stage.</returns>
        internal Config PackageStageIntoConfig()
        {
            var config = new Config
            {
                ClearColor = ClearColor,
                Dimensions = Dimensions,
                FPSLimit = TargetFPS,
                BroadcastViaSpout = BroadcastViaSpout,
            };

            var actorList = new List<ActorConfig>();
            foreach (var actor in _actors)
                actorList.Add(actor.PackageActorIntoConfig());

            config.Actors = actorList;
            config.AssetDatabase = AssetDatabase.PackageIntoConfig();

            return config;
        }
    }
}
