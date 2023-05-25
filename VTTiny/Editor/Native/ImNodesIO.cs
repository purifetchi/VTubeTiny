using System.Runtime.CompilerServices;

namespace VTTiny.Editor.Native;
public unsafe struct ImNodesIO
{
    internal unsafe ImNodesIONative* NativePtr { get; init; }
    
    public ImNodesIO(nint ptr)
    {
        NativePtr = (ImNodesIONative*)ptr;
    }
}
