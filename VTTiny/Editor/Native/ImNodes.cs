using System.Runtime.InteropServices;
using ImGuiNET;

namespace VTTiny.Editor.Native;

/// <summary>
/// The shape of the node pin.
/// </summary>
public enum ImNodesPinShape
{
    Circle = 0,
    CircleFilled = 1,
    Triangle = 2,
    TriangleFilled = 3,
    Quad = 4,
    QuadFilled = 5,
}

/// <summary>
/// The color style variables.
/// </summary>
public enum ImNodesCol
{
    NodeBackground = 0,
    NodeBackgroundHovered = 1,
    NodeBackgroundSelected = 2,
    NodeOutline = 3,
    TitleBar = 4,
    TitleBarHovered = 5,
    TitleBarSelected = 6,
    Link = 7,
    LinkHovered = 8,
    LinkSelected = 9,
    Pin = 10,
    PinHovered = 11,
    BoxSelector = 12,
    BoxSelectorOutline = 13,
    GridBackground = 14,
    GridLine = 15,
    MiniMapBackground = 16,
    MiniMapBackgroundHovered = 17,
    MiniMapOutline = 18,
    MiniMapOutlineHovered = 19,
    MiniMapNodeBackground = 20,
    MiniMapNodeBackgroundHovered = 21,
    MiniMapNodeBackgroundSelected = 22,
    MiniMapNodeOutline = 23,
    MiniMapLink = 24,
    MiniMapLinkSelected = 25,
    MiniMapCanvas = 26,
    MiniMapCanvasOutline = 27,
    COUNT = 28,
}

/// <summary>
/// The style variables.
/// </summary>
public enum ImNodesStyleVar
{
    GridSpacing = 0,
    NodeCornerRounding = 1,
    NodePadding = 2,
    NodeBorderThickness = 3,
    LinkThickness = 4,
    LinkLineSegmentsPerLength = 5,
    LinkHoverDistance = 6,
    PinCircleRadius = 7,
    PinQuadSideLength = 8,
    PinTriangleSideLength = 9,
    PinLineThickness = 10,
    PinHoverRadius = 11,
    PinOffset = 12,
    MiniMapPadding = 13,
    MiniMapOffset = 14
}

/// <summary>
/// A single link created from within ImNodes
/// </summary>
public struct NodeLink
{
    /// <summary>
    /// The starting node id.
    /// </summary>
    public int StartNode { get; set; }
    
    /// <summary>
    /// The ending node id.
    /// </summary>
    public int EndNode { get; set; }
    
    /// <summary>
    /// The starting attribute.
    /// </summary>
    public int StartAttribute { get; set; }

    /// <summary>
    /// The ending attribute.
    /// </summary>
    public int EndAttribute { get; set; }
}

/// <summary>
/// Bindings to the ImNodes library.
/// </summary>
public static class ImNodes
{
    private const string IMNODES_LIBRARY_NAME = "cimnodes";

    /// <summary>
    /// The context.
    /// </summary>
    private static nint Context { get; set; }

    [DllImport(IMNODES_LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
    private static extern nint imnodes_CreateContext();

    [DllImport(IMNODES_LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
    private static extern void imnodes_SetImGuiContext(nint ctx);

    /// <summary>
    /// Creates a new ImNodes context and sets the appropriate imgui context.
    /// </summary>
    public static void CreateContext()
    {
        Context = imnodes_CreateContext();
        imnodes_SetImGuiContext(ImGui.GetCurrentContext());
        
        unsafe
        {
            // Hook the ImGui ctrl held with the link detach modifier.
            // We're doing it this way, since this is the way ImNodes tells you to wire it all up.
            GetIO().NativePtr->LinkDetachWithModifierClick.Modifier = (bool*)&ImGui.GetIO().NativePtr->KeyCtrl;
        }
    }

    [DllImport(IMNODES_LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
    private static extern void imnodes_DestroyContext(nint ctx);

    /// <summary>
    /// Destroy the ImNodes context.
    /// </summary>
    public static void DestroyContext()
    {
        imnodes_DestroyContext(Context);
    }

    [DllImport(IMNODES_LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
    private static extern nint imnodes_GetIO();

    /// <summary>
    /// Get the IO of ImNodes.
    /// </summary>
    /// <returns>The ImNodes IO.</returns>
    public static ImNodesIO GetIO()
    {
        return new ImNodesIO(imnodes_GetIO());
    }

    [DllImport(IMNODES_LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
    private static extern void imnodes_BeginNode(int id);

    /// <summary>
    /// Begins a node with the given id.
    /// </summary>
    /// <param name="id">The id of the node.</param>
    public static void BeginNode(int id)
    {
        imnodes_BeginNode(id);
    }

    [DllImport(IMNODES_LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
    private static extern void imnodes_BeginNodeEditor();

    /// <summary>
    /// Begins the node editor.
    /// </summary>
    public static void BeginNodeEditor()
    {
        imnodes_BeginNodeEditor();
    }

    [DllImport(IMNODES_LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
    private static extern void imnodes_EndNode();

    /// <summary>
    /// Ends a node.
    /// </summary>
    public static void EndNode()
    {
        imnodes_EndNode();
    }

    [DllImport(IMNODES_LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
    private static extern void imnodes_EndNodeEditor();

    /// <summary>
    /// Ends the node editor.
    /// </summary>
    public static void EndNodeEditor()
    {
        imnodes_EndNodeEditor();
    }

    [DllImport(IMNODES_LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
    private static extern void imnodes_BeginNodeTitleBar();

    /// <summary>
    /// Begin the title bar of the node.
    /// </summary>
    public static void BeginNodeTitleBar()
    {
        imnodes_BeginNodeTitleBar();
    }

    [DllImport(IMNODES_LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
    private static extern void imnodes_EndNodeTitleBar();

    /// <summary>
    /// Begin the title bar of the node.
    /// </summary>
    public static void EndNodeTitleBar()
    {
        imnodes_EndNodeTitleBar();
    }

    [DllImport(IMNODES_LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
    public static extern void imnodes_BeginInputAttribute(int id, ImNodesPinShape shape);

    /// <summary>
    /// Begins an input attribute.
    /// </summary>
    public static void BeginInputAttribute(int id, ImNodesPinShape shape)
    {
        imnodes_BeginInputAttribute(id, shape);
    }

    [DllImport(IMNODES_LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
    public static extern void imnodes_EndInputAttribute();

    /// <summary>
    /// Ends an input attribute.
    /// </summary>
    public static void EndInputAttribute()
    {
        imnodes_EndInputAttribute();
    }

    [DllImport(IMNODES_LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
    private static extern void imnodes_BeginOutputAttribute(int id, ImNodesPinShape shape);

    /// <summary>
    /// Begins an output attribute.
    /// </summary>
    public static void BeginOutputAttribute(int id, ImNodesPinShape shape)
    {
        imnodes_BeginOutputAttribute(id, shape);
    }

    [DllImport(IMNODES_LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
    private static extern void imnodes_EndOutputAttribute();

    /// <summary>
    /// Ends an output attribute.
    /// </summary>
    public static void EndOutputAttribute()
    {
        imnodes_EndOutputAttribute();
    }

    [DllImport(IMNODES_LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool imnodes_IsLinkCreated_IntPtr(out int started_at_node_id, out int started_at_attribute_id, out int ended_at_node_id, out int ended_at_attribute_id, out byte created_from_snap);
    
    /// <summary>
    /// Checks if a new link has been created.
    /// </summary>
    /// <param name="link">The resulting link.</param>
    /// <returns>Whether a link has been created.</returns>
    public static bool IsLinkCreated(out NodeLink link)
    {
        if (!imnodes_IsLinkCreated_IntPtr(out var startNode, out var startAttrib, out var endNode, out var endAttrib, out _))
        {
            link = default;
            return false;
        }

        link = new NodeLink
        {
            StartNode = startNode,
            StartAttribute = startAttrib,
            EndNode = endNode,
            EndAttribute = endAttrib
        };

        return true;
    }

    [DllImport(IMNODES_LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern byte imnodes_IsLinkDestroyed(out int link_id);

    /// <summary>
    /// Checks if a link has been destroyed.
    /// </summary>
    /// <param name="id">The id of the destroyed link.</param>
    /// <returns>Whether it has been destroyed.</returns>
    public static bool IsLinkDestroyed(out int id)
    {
        // NOTE: We don't use UnmanagedType.Bool here only because it constantly returns true for some reason.
        //       Using U1 and then checking ourselves seems to work, though.
        return imnodes_IsLinkDestroyed(out id) == 1;
    }

    [DllImport(IMNODES_LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
    private static extern void imnodes_Link(int id, int start_attribute_id, int end_attribute_id);

    /// <summary>
    /// Links two attributes together.
    /// </summary>
    /// <param name="id">The id of the link.</param>
    /// <param name="start">The start attribute.</param>
    /// <param name="end">The end attribute.</param>
    public static void Link(int id, int start, int end)
    {
        imnodes_Link(id, start, end);
    }

    [DllImport(IMNODES_LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
    private static extern void imnodes_PushColorStyle(ImNodesCol styleVar, uint color);

    /// <summary>
    /// Push a color style.
    /// </summary>
    /// <param name="styleVar">The specific style var to push.</param>
    /// <param name="color">The color.</param>
    public static void PushColorStyle(ImNodesCol styleVar, ImColor color)
    {
        imnodes_PushColorStyle(styleVar, ImGui.ColorConvertFloat4ToU32(color.Value));
    }

    [DllImport(IMNODES_LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
    private static extern void imnodes_PopColorStyle();

    /// <summary>
    /// Pops the last color style.
    /// </summary>
    public static void PopColorStyle()
    {
        imnodes_PopColorStyle();
    }

    [DllImport(IMNODES_LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
    private static extern void imnodes_MiniMap();

    /// <summary>
    /// Draws the minimap.
    /// </summary>
    public static void MiniMap()
    {
        imnodes_MiniMap();
    }

    [DllImport(IMNODES_LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
    private static extern nint imnodes_SaveCurrentEditorStateToIniString(out nint size);

    /// <summary>
    /// Saves the current editor state to an ini string.
    /// </summary>
    /// <returns>The editor state.</returns>
    public static string SaveCurrentEditorStateToIniString()
    {
        var ptr = imnodes_SaveCurrentEditorStateToIniString(out var size);
        return Marshal.PtrToStringUTF8(ptr, (int)size);
    }

    [DllImport(IMNODES_LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
    private static extern void imnodes_LoadCurrentEditorStateFromIniString(nint data, nint data_size);

    /// <summary>
    /// Loads the current editor state from an ini string.
    /// </summary>
    /// <param name="data">The ini config.</param>
    public static void LoadCurrentEditorStateFromIniString(string data)
    {
        if (string.IsNullOrEmpty(data))
            return;

        var ptr = Marshal.StringToCoTaskMemUTF8(data);
        var size = System.Text.Encoding.UTF8.GetByteCount(data);
        imnodes_LoadCurrentEditorStateFromIniString(ptr, size);

        Marshal.ZeroFreeCoTaskMemUTF8(ptr);
    }
}
