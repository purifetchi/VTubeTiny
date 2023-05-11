namespace VTTiny.Scripting.Pins;

public class ActionInputPin : InputPin
{
    /// <summary>
    /// Runs the node associated with this action input pin.
    /// </summary>
    public void Run()
    {
        Node.Update();
    }
}
