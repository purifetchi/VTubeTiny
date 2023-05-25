using System;
using System.Collections.Generic;
using VTTiny.Components;
using VTTiny.Data;

namespace VTTiny.Scenery
{
    public partial class StageActor
    {
        /// <summary>
        /// Build all the components from a component config.
        /// </summary>
        /// <param name="componentConfigs"></param>
        internal void BuildComponentsFromConfig(IList<TypedObjectConfig> componentConfigs)
        {
            if (componentConfigs == null)
                return;

            foreach (var componentConfig in componentConfigs)
            {
                if (!componentConfig.TryResolveType<Component>(out Type type))
                {
                    Console.WriteLine($"Couldn't instantiate component {componentConfig.Namespace}.{componentConfig.Type} in actor {Name}.");
                    continue;
                }

                // We should not instantiate the transform from the parameters, there's only one transform per actor.
                if (type == typeof(TransformComponent))
                    continue;

                var component = ConstructComponentFromType(type);
                component.InheritParametersFromConfig(componentConfig.Parameters);
            }
        }

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
                Position = Transform.LocalPosition,
                Rotation = Transform.LocalRotation,
                AllowRendering = AllowRendering
            };

            var componentList = new List<TypedObjectConfig>();
            foreach (var component in _components)
                componentList.Add(component.PackageIntoConfig());

            config.Components = componentList;

            return config;
        }
    }
}
