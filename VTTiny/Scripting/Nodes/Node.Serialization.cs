namespace VTTiny.Scripting.Nodes;

public abstract partial class Node
{
    /// <inheritdoc/>
    protected override string GetNameForSerialization()
    {
        return Name;
    }
}
