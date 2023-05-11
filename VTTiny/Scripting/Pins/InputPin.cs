namespace VTTiny.Scripting.Pins;

public abstract class InputPin : Pin
{
    /// <summary>
    /// The id of the output pin it's connected to.
    /// </summary>
    public int OutputId { get; set; }
}
