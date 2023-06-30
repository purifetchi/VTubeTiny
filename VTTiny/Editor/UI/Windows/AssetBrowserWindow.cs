using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ImGuiNET;
using Raylib_cs;
using VTTiny.Assets;
using VTTiny.Assets.Management;
using VTTiny.Scenery;

namespace VTTiny.Editor.UI;

/// <summary>
/// The asset browser.
/// </summary>
internal class AssetBrowserWindow : EditorWindow,
    IStageAwareWindow
{
    /// <summary>
    /// The stage this window is attached to.
    /// </summary>
    public Stage Stage { get; set; }

    /// <summary>
    /// Buffer used for storing the drag'n'drop asset identifier.
    /// </summary>
    private readonly nint _dragDropMemoryBufferPointer;

    /// <summary>
    /// The previous GUI object that we've had.
    /// Dirty hack but I have no idea how to circumvent that lol.
    /// </summary>
    private WeakReference<IEditorGUIDrawable> _previousGUIObject;

    /// <summary>
    /// Creates a new stage properties window with a given stage.
    /// </summary>
    /// <param name="stage">The stage to set.</param>
    public AssetBrowserWindow(Stage stage)
        : base("Asset Browser")
    {
        Stage = stage;
        _dragDropMemoryBufferPointer = Marshal.AllocHGlobal(sizeof(int));
    }

    /// <summary>
    /// The destructor. Deallocates the memory buffer for the drag'n'drop payload.
    /// </summary>
    ~AssetBrowserWindow()
    {
        Marshal.FreeHGlobal(_dragDropMemoryBufferPointer);
    }

    /// <summary>
    /// Handle drag and dropping assets.
    /// </summary>
    private void HandleDragAndDropAssets()
    {
        if (!Raylib.IsFileDropped() || !ImGui.IsWindowHovered())
            return;

        var paths = Raylib.GetDroppedFiles();
        Raylib.ClearDroppedFiles();

        foreach (var path in paths)
            AssetHelper.LoadBasedOnExtension(path, Stage.AssetDatabase);
    }

    /// <summary>
    /// Selects the asset and shows its properties in the properties window.
    /// </summary>
    /// <param name="asset">The asset.</param>
    private void SelectAsset(Asset asset)
    {
        var window = Editor.GetWindow<ObjectPropertiesWindow>();

        // HACK: We only do that for the drag'n'drop behavior. Since activating drag'n'drop requires us to
        //       move the mouse, the asset selection will override whatever we had in the object properties
        //       window before. We actually want to be able to drag assets into the previous window, so
        //       we'll store a weak reference to it (in case this object might actually want to become invalid).
        _previousGUIObject = new WeakReference<IEditorGUIDrawable>(window.GuiObject);

        window.GuiObject = asset;
    }

    /// <summary>
    /// Handles the drag'n'drop behavior for assets.
    /// </summary>
    /// <returns>Whether this asset is being drag'n'dropped.</returns>
    private unsafe bool HandleDragDropForAsset(Asset asset)
    {
        if (!ImGui.BeginDragDropSource())
            return false;

        Unsafe.Write((void*)(_dragDropMemoryBufferPointer), asset.Id);

        ImGui.SetDragDropPayload("Asset", _dragDropMemoryBufferPointer, sizeof(int));
        asset.RenderAssetPreview();
        ImGui.EndDragDropSource();

        return true;
    }

    /// <summary>
    /// Restores the previous GUI object in the object properties window after starting to drag'n'drop.
    /// This is a hack, it should probably be fixed in a more sane way.
    /// </summary>
    private void RestorePreviousGUIObject()
    {
        if (_previousGUIObject?.TryGetTarget(out var guiObject) != true)
            return;

        Editor.GetWindow<ObjectPropertiesWindow>()
            .GuiObject = guiObject;

        _previousGUIObject = null;
    }

    /// <inheritdoc/>
    protected override void DrawUI()
    {
        if (Stage.AssetDatabase.AssetCount < 1)
        {
            if (ImGui.IsWindowHovered())
                ImGui.SetTooltip("Drag and drop a file here to load it as an asset!");

            EditorGUI.CenterText("No assets present.");
        }
        else
        {
            var style = ImGui.GetStyle();

            var contentRegion = ImGui.GetContentRegionAvail().X + style.WindowPadding.X;
            var assetPreviewMargins = style.CellPadding.X + (style.FramePadding.X * 2);

            var maxItems = (int)MathF.Max(1, contentRegion / (Asset.ASSET_PREVIEW_SIZE + assetPreviewMargins));

            var currentItems = 0;

            foreach (var asset in Stage.AssetDatabase.GetAssets())
            {
                currentItems++;

                asset.RenderAssetPreview();

                if (!HandleDragDropForAsset(asset))
                {
                    if (ImGui.IsItemHovered())
                        ImGui.SetTooltip(asset.Name);

                    if (ImGui.IsItemClicked())
                        SelectAsset(asset);
                }
                else
                {
                    RestorePreviousGUIObject();
                }

                Editor.DoContextMenuFor(asset);

                if (currentItems % maxItems != 0)
                    ImGui.SameLine();
            }
        }

        HandleDragAndDropAssets();
    }

    /// <inheritdoc/>
    public void OnStageChange(Stage stage)
    {
        Stage = stage;
    }
}
