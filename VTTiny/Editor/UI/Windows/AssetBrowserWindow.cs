using System.Xml.Linq;
using ImGuiNET;
using Raylib_cs;
using VTTiny.Assets;
using VTTiny.Assets.Management;
using VTTiny.Scenery;

namespace VTTiny.Editor.UI
{
    /// <summary>
    /// The asset browser.
    /// </summary>
    internal class AssetBrowserWindow : EditorWindow, IStageAwareWindow
    {
        /// <summary>
        /// The stage this window is attached to.
        /// </summary>
        public Stage Stage { get; set; }

        /// <summary>
        /// Creates a new stage properties window with a given stage.
        /// </summary>
        /// <param name="stage">The stage to set.</param>
        public AssetBrowserWindow(Stage stage)
            : base("Asset Browser")
        {
            Stage = stage;
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
            {
                AssetHelper.LoadBasedOnExtension(path, Stage.AssetDatabase);
            }
        }

        /// <summary>
        /// Selects the asset and shows its properties in the properties window.
        /// </summary>
        /// <param name="asset">The asset.</param>
        private void SelectAsset(Asset asset)
        {
            Editor.GetWindow<ObjectPropertiesWindow>()
                .GuiObject = asset;
        }

        protected override void DrawUI()
        {
            if (Stage.AssetDatabase.AssetCount < 1)
            {
                if (ImGui.IsWindowHovered())
                    ImGui.SetTooltip("Drag and drop a file here to load it as an asset!");

                ImGui.Text("No assets present.");
            }
            else
            {
                var style = ImGui.GetStyle();

                var contentRegion = ImGui.GetContentRegionAvail().X + style.WindowPadding.X;
                var assetPreviewMargins = style.CellPadding.X + (style.FramePadding.X * 2);

                var maxItems = (int)System.MathF.Max(1, contentRegion / (Asset.ASSET_PREVIEW_SIZE + assetPreviewMargins));

                var currentItems = 0;

                foreach (var asset in Stage.AssetDatabase.GetAssets())
                {
                    currentItems++;

                    asset.RenderAssetPreview();

                    if (ImGui.IsItemClicked())
                        SelectAsset(asset);

                    Editor.DoContextMenuFor(asset);

                    if (currentItems % maxItems != 0)
                    {
                        ImGui.SameLine();
                    }
                }
            }

            HandleDragAndDropAssets();
        }

        public void OnStageChange(Stage stage)
        {
            Stage = stage;
        }
    }
}
