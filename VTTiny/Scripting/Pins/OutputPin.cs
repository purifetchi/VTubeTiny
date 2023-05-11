using System.Collections.Generic;
using System.Linq;

namespace VTTiny.Scripting.Pins;

/// <summary>
/// The base class for any output pin.
/// </summary>
public abstract class OutputPin : Pin
{
    /// <summary>
    /// The id of the input pin it's connected to.
    /// </summary>
    public List<InputPin> Inputs { get; init; }

    /// <summary>
    /// Creates a new output pin.
    /// </summary>
    public OutputPin()
    {
        Inputs = new();
    }

    /// <summary>
    /// Can this output connect to a given pin?
    /// </summary>
    /// <param name="input">The input pin.</param>
    /// <returns>Whether it can connect.</returns>
    public virtual bool CanConnectWith(InputPin input)
    {
        return false;
    }

    /// <summary>
    /// Tries to connect with an input.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>Whether the connection was successful.</returns>
    public bool TryConnectWith(InputPin input)
    {
        if (!CanConnectWith(input))
            return false;

        Inputs.Add(input);
        return true;
    }

    /// <summary>
    /// Tries to remove a link with a different pin.
    /// </summary>
    /// <param name="otherPin">The other pin</param>
    /// <returns>Whether it was succesfully removed.</returns>
    public bool TryRemoveLink(int otherPin)
    {
        var item = Inputs.Find(pin => pin.Id == otherPin);
        if (item == null)
            return false;

        return Inputs.Remove(item);
    }
}
