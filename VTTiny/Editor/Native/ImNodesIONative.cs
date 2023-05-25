using System.Runtime.InteropServices;

namespace VTTiny.Editor.Native;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct ImNodesIONative
{
    public ImNodesModifierStruct EmulateThreeButtonMouse;
    public ImNodesModifierStruct LinkDetachWithModifierClick;
    public ImNodesModifierStruct MultipleSelectModifier;

    public int AltMouseButton;

    public float AutoPanningSpeed;
}
