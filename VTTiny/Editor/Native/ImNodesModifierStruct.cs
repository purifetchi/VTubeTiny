using System.Runtime.InteropServices;

namespace VTTiny.Editor.Native;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct ImNodesModifierStruct
{
    public bool* Modifier;
}
