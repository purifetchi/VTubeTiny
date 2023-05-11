using System.Collections.Generic;
using ImGuiNET;
using VTTiny.Editor;
using VTTiny.Scripting.Pins;
using VTTiny.Serialization;

namespace VTTiny.Scripting.Nodes;

/// <summary>
/// A stage graph node.
/// </summary>
public abstract partial class Node : TypedSerializedObject,
    IEditorGUIDrawable,
    IHasRightClickContext
{
    /// <summary>
    /// The name of the node.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// The input pins.
    /// </summary>
    public List<InputPin> Inputs { get; init; }

    /// <summary>
    /// The output pins.
    /// </summary>
    public List<OutputPin> Outputs { get; init; }

    /// <summary>
    /// The node id.
    /// </summary>
    public int Id { get; internal set; }

    /// <summary>
    /// The stage graph we're a part of.
    /// </summary>
    public StageGraph Graph { get; internal set; }

    /// <summary>
    /// The color of this node.
    /// </summary>
    public ImColor NodeColor { get; init; }

    /// <summary>
    /// Creates a new node.
    /// </summary>
    /// <param name="name">The name of the node.</param>
    /// <param name="color">The color of this node.</param>
    public Node(string name, ImColor color)
    {
        Name = name;
        NodeColor = color;

        Inputs = new();
        Outputs = new();
    }

    /// <summary>
    /// Adds an input pin.
    /// </summary>
    /// <typeparam name="TPin">The type of the pin.</typeparam>
    /// <returns>The pin.</returns>
    protected TPin AddInput<TPin>()
        where TPin : InputPin, new()
    {
        var pin = new TPin
        {
            Node = this,
            Id = Graph.Allocator.AllocateId()
        };

        Inputs.Add(pin);

        return pin;
    }

    /// <summary>
    /// Adds an output pin.
    /// </summary>
    /// <typeparam name="TPin">The type of the pin.</typeparam>
    /// <returns>The pin.</returns>
    protected TPin AddOutput<TPin>()
        where TPin : OutputPin, new()
    {
        var pin = new TPin
        {
            Node = this,
            Id = Graph.Allocator.AllocateId()
        };

        Outputs.Add(pin);

        return pin;
    }

    /// <summary>
    /// Initializes the pins for this node.
    /// </summary>
    internal abstract void InitializePins();

    /// <summary>
    /// Runs the node's logic.
    /// </summary>
    public abstract void Update();
}
