using System;
using System.Threading;
using System.Threading.Tasks;
using VTTiny.Editor;
using VTTiny.Scripting.Pins;

namespace VTTiny.Scripting.Nodes;

/// <summary>
/// A node that waits a set amount of time and then resumes operation.
/// </summary>
public class WaitNode : Node
{
    /// <summary>
    /// The amount of time to wait.
    /// </summary>
    public float Time { get; set; } = 1f;

    /// <summary>
    /// Should this node cancel the already pending operation, if one exists?
    /// </summary>
    public bool CancelIfPending { get; set; }

    /// <summary>
    /// The cancellation token.
    /// </summary>
    private CancellationTokenSource _source;

    /// <summary>
    /// Creates a new wait node.
    /// </summary>
    public WaitNode()
        : base("Wait", NodeStyles.PassthroughNode)
    {
        _source = new();
    }

    /// <summary>
    /// Waits and then fires the output.
    /// </summary>
    private async void Wait()
    {
        try
        {
            await Task.Delay((int)(Time * 1000f), _source.Token);

            (Outputs[0] as ActionOutputPin).Fire();
        }
        catch (OperationCanceledException)
        {
            // This is fine.
        }
    }

    /// <inheritdoc/>
    public override void RenderEditorGUI()
    {
        Time = EditorGUI.DragFloat("Time", Time, 0.1f);
        CancelIfPending = EditorGUI.Checkbox("Cancel if pending", CancelIfPending);
    }

    /// <inheritdoc/>
    public override void Update()
    {
        if (CancelIfPending)
        {
            _source?.Cancel();
            _source = new();
        }

        Wait();
    }

    /// <inheritdoc/>
    internal override void InitializePins()
    {
        AddInput<ActionInputPin>();
        AddOutput<ActionOutputPin>();
    }
}
