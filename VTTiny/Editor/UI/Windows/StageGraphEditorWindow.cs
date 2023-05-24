using ImGuiNET;
using VTTiny.Editor.Native;
using VTTiny.Scenery;
using VTTiny.Scripting;
using VTTiny.Scripting.Nodes;

namespace VTTiny.Editor.UI;

/// <summary>
/// The editor for VTubeTiny's stage graph.
/// </summary>
internal class StageGraphEditorWindow : EditorWindow,
    IStageAwareWindow
{
    /// <summary>
    /// The stage graph.
    /// </summary>
    private StageGraph _graph;

    /// <summary>
    /// Creates a new stage graph editor.
    /// </summary>
    public StageGraphEditorWindow() 
        : base("Stage Graph Editor", ImGuiWindowFlags.MenuBar)
    {
        
    }

    /// <summary>
    /// Saves the current editor state to a string that can be loaded on later.
    /// </summary>
    /// <returns>The editor state string.</returns>
    public string SaveEditorStateToString()
    {
        return ImNodes.SaveCurrentEditorStateToIniString();
    }

    /// <summary>
    /// Loads the editor state from an ini string.
    /// </summary>
    /// <param name="config">The ini string.</param>
    public void LoadEditorStateFromString(string config)
    {
        ImNodes.LoadCurrentEditorStateFromIniString(config);
    }

    /// <summary>
    /// Draws a single node.
    /// </summary>
    /// <param name="node">The node.</param>
    private void DrawNode(Node node)
    {
        const float ITEM_WIDTH = 150f;

        ImGui.PushItemWidth(ITEM_WIDTH);
        ImNodes.PushColorStyle(ImNodesCol.TitleBar, node.NodeColor);

        ImNodes.BeginNode(node.Id);

        ImNodes.BeginNodeTitleBar();
        ImGui.TextUnformatted(node.Name);
        ImNodes.EndNodeTitleBar();

        node.RenderEditorGUI();

        foreach (var input in node.Inputs)
        {
            ImNodes.BeginInputAttribute(input.Id, ImNodesPinShape.CircleFilled);
            input.RenderEditorGUI();
            ImNodes.EndInputAttribute();
        }

        foreach (var output in node.Outputs)
        {
            ImNodes.BeginOutputAttribute(output.Id, ImNodesPinShape.CircleFilled);
            output.RenderEditorGUI();
            ImNodes.EndOutputAttribute();
        }

        ImNodes.EndNode();
        
        ImNodes.PopColorStyle();

        Editor.DoContextMenuFor(node);

        ImGui.PopItemWidth();
    }

    /// <inheritdoc/>
    protected override void DrawUI()
    {
        if (_graph == null)
        {
            _graph = Editor?.VTubeTiny?.ActiveStage?.StageGraph;
            return;
        }

        if (ImGui.BeginMenuBar())
        {
            if (ImGui.BeginMenu("Nodes"))
            {
                if (EditorGUI.NodeList(out var nodeType))
                    _graph.AddNode(nodeType);

                ImGui.EndMenu();
            }

            if (ImGui.MenuItem("Close"))
                Editor.RemoveWindow(this);
            
            ImGui.EndMenuBar();
        }

        ImNodes.BeginNodeEditor();

        foreach (var node in _graph.Nodes)
            DrawNode(node);

        for (var i = 0; i < _graph.Links.Count; i++)
        {
            var nodeLink = _graph.Links[i];
            ImNodes.Link(i, nodeLink.StartAttribute, nodeLink.EndAttribute);
        }

        ImNodes.EndNodeEditor();

        if (ImNodes.IsLinkCreated(out var link))
        {
            _graph.CreateLink(
                link.StartAttribute, 
                link.StartNode, 
                link.EndAttribute, 
                link.EndNode);
        }

        if (ImNodes.IsLinkDestroyed(out var linkId))
            _graph.SevereLink(linkId);
    }

    public void OnStageChange(Stage newStage)
    {
        _graph = newStage.StageGraph;
        LoadEditorStateFromString(newStage.VTubeTiny.Config.StageGraph.EditorConfig);
    }
}
