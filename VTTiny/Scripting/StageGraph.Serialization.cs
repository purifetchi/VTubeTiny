using System.Linq;
using VTTiny.Scripting.Nodes;
using VTTiny.Scripting.Serialization;

namespace VTTiny.Scripting;

public partial class StageGraph
{
    /// <summary>
    /// Packages this stage graph into a config.
    /// </summary>
    /// <returns>The resulting config.</returns>
    internal StageGraphConfig PackageIntoConfig()
    {
        return new StageGraphConfig
        {
            Nodes = Nodes.Select(node => new NodeConfig 
            { 
                Id = node.Id, 
                Config = node.PackageIntoConfig(), 
                Pins = new PinConfig 
                { 
                    InputPinIds = node.Inputs.Select(input => input.Id).ToList(),
                    OutputPinIds = node.Outputs.Select(output => output.Id).ToList()
                }
            }).ToList(),
            Links = _links.ToList()
        };
    }

    /// <summary>
    /// Loads the stage graph data from config.
    /// </summary>
    /// <param name="config">The config.</param>
    internal void LoadFromConfig(StageGraphConfig config)
    {
        foreach (var nodeConfig in config.Nodes)
        {
            if (!nodeConfig.Config.TryResolveType<Node>(out var resolved))
                continue;

            AddNode(resolved, nodeConfig.Id, nodeConfig.Pins)
                .InheritParametersFromConfig(nodeConfig.Config.Parameters);
        }

        foreach (var link in config.Links)
            CreateLink(link.StartAttribute, link.StartNode, link.EndAttribute, link.EndNode);
    }
}
