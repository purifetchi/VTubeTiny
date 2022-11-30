using System;
using System.Collections.Generic;
using VTTiny.Data;

namespace VTTiny.Scenery
{
    public partial class StageActor
    {
        /// <summary>
        /// Build all the components from a component config.
        /// </summary>
        /// <param name="componentConfigs"></param>
        internal void BuildComponentsFromConfig(IList<ComponentConfig> componentConfigs)
        {
            if (componentConfigs == null)
                return;

            foreach (var componentConfig in componentConfigs)
            {
                // Get the type of the constructor, which .NET expects as {namespace}.{type}
                var type = Type.GetType($"{componentConfig.Namespace}.{componentConfig.Type}");
                if (type == null)
                {
                    Console.WriteLine($"Couldn't instantiate component {componentConfig.Namespace}.{componentConfig.Type} in actor {Name}.");
                    continue;
                }

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
