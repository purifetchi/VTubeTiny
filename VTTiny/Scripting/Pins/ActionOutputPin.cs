namespace VTTiny.Scripting.Pins;

/// <summary>
/// An output pin that 
/// </summary>
public class ActionOutputPin : OutputPin
{
    /// <inheritdoc/>
    public override bool CanConnectWith(InputPin input)
    {
        return input is ActionInputPin;
    }

    /// <summary>
    /// Fires this output pin.
    /// </summary>
    public void Fire()
    {
        foreach (var input in Inputs)
            (input as ActionInputPin).Run();
    }
}
