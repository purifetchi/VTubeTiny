using System;
using System.Collections.Generic;
using System.Linq;
using VTTiny.Base;
using VTTiny.Extensions;
using VTTiny.Scenery;
using VTTiny.Scripting.Nodes;
using VTTiny.Scripting.Pins;

namespace VTTiny.Scripting;

/// <summary>
/// A visual scripting graph for VTubeTiny.
/// </summary>
public class StageGraph
{
    /// <summary>
    /// Provides a read only view into all of the nodes.
    /// </summary>
    public IReadOnlyList<Node> Nodes => _nodes;

    /// <summary>
    /// Provides a read only view into the links.
    /// </summary>
    public IReadOnlyList<Link> Links => _links;

    /// <summary>
    /// The id allocator for nodes and pins.
    /// </summary>
    public IdAllocator Allocator { get; init; }

    /// <summary>
    /// The stage we're attached to.
    /// </summary>
    public Stage Stage { get; init; }

    /// <summary>
    /// The nodes of this stage graph.
    /// </summary>
    private readonly List<Node> _nodes;

    /// <summary>
    /// The links within this graph.
    /// </summary>
    private readonly List<Link> _links;

    /// <summary>
    /// Constructs a new stage graph.
    /// </summary>
    public StageGraph(Stage stage)
    {
        _nodes = new();
        _links = new();

        Allocator = new();
        Stage = stage;
    }

    /// <summary>
    /// Creates a new node.
    /// </summary>
    /// <typeparam name="TNode">The type of the node.</typeparam>
    /// <returns>The created node.</returns>
    public TNode AddNode<TNode>()
        where TNode: Node, new()
    {
        var node = new TNode
        {
            Graph = this,
            Id = Allocator.AllocateId()
        };

        node.InitializePins();
        _nodes.Add(node);

        return node;
    }

    /// <summary>
    /// Creates a node given its type.
    /// </summary>
    /// <param name="nodeType">The type of the node.</param>
    /// <returns></returns>
    public Node AddNode(Type nodeType)
    {
        var node = nodeType.Construct<Node>();
        node.Graph = this;
        node.Id = Allocator.AllocateId();
        node.InitializePins();
        _nodes.Add(node);

        return node;
    }

    /// <summary>
    /// Gets a node by its id.
    /// </summary>
    /// <typeparam name="TNode">The node type.</typeparam>
    /// <param name="id">The id.</param>
    /// <returns>A node, or nothing.</returns>
    public TNode GetNodeById<TNode>(int id)
        where TNode : Node
    {
        return Nodes.FirstOrDefault(node => node is TNode && node.Id == id) as TNode;
    }

    /// <summary>
    /// Creates a link between two nodes and their pins.
    /// </summary>
    /// <param name="beginningPin">The first pin.</param>
    /// <param name="beginningNode">The first node.</param>
    /// <param name="endingPin">The second pin.</param>
    /// <param name="endingNode">The second node.</param>
    public void CreateLink(int beginningPin, int beginningNode, int endingPin, int endingNode)
    {
        var node = GetNodeById<Node>(beginningNode);
        if (node is null)
            return;

        var pin = node.Outputs
            .FirstOrDefault(pin => pin.Id == beginningPin);
        if (pin is null)
            return;

        var endNode = GetNodeById<Node>(endingNode);
        if (endNode is null)
            return;

        var endPin = endNode.Inputs
            .FirstOrDefault(pin => pin.Id == endingPin);
        if (endPin is null)
            return;

        if (!pin.TryConnectWith(endPin))
            return;

        _links.Add(new Link
        {
            StartAttribute = beginningPin,
            StartNode = beginningNode,

            EndAttribute = endingPin,
            EndNode = endingNode
        });
    }

    /// <summary>
    /// Removes a link between two nodes.
    /// </summary>
    /// <param name="id">The link</param>
    public void SevereLink(int id)
    {
        if (id < _links.Count ||
            id >= _links.Count)
        {
            return;
        }

        var link = _links[id];
        SevereLink(link);
    }

    /// <summary>
    /// Removes a link between two nodes.
    /// </summary>
    /// <param name="link">The link</param>
    private void SevereLink(Link link)
    {
        var node = GetNodeById<Node>(link.StartNode);
        if (node is null)
        {
            _links.Remove(link);
            return;
        }

        var pin = node.Outputs.FirstOrDefault(pin => pin.Id == link.StartAttribute);
        if (pin is null)
        {
            _links.Remove(link);
            return;
        }

        pin.TryRemoveLink(link.EndAttribute);
        _links.Remove(link);
    }

    /// <summary>
    /// Removes a node and all the connections it had given its id.
    /// </summary>
    /// <param name="id">The id of the node.</param>
    public void RemoveNode(int id)
    {
        var node = GetNodeById<Node>(id);
        if (node is null)
            return;

        RemoveNode(node);
    }

    /// <summary>
    /// Removes a node.
    /// </summary>
    /// <param name="node">The node to remove.</param>
    public void RemoveNode(Node node)
    {
        var links = Links
            .Where(link => link.StartNode == node.Id || link.EndNode == node.Id)
            .ToList();

        foreach (var link in links)
            SevereLink(link);

        Allocator.FreeId(node.Id);

        var ids = node.Inputs
            .Select(input => input as Pin)
            .Concat(node.Outputs)
            .Select(pin => pin.Id);

        foreach (var id in ids)
            Allocator.FreeId(id);

        _nodes.Remove(node);
    }

    /// <summary>
    /// Updates the stage graph.
    /// </summary>
    public void Update()
    {
        foreach (var node in _nodes)
        {
            if (node is ISourceNode)
                node.Update();
        }
    }
}
