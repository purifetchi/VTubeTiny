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

            var path = Raylib.GetDroppedFiles()[0];
            Raylib.ClearDroppedFiles();

            AssetHelper.LoadBasedOnExtension(path, Stage.AssetDatabase);
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
                foreach (var asset in Stage.AssetDatabase.GetAssets())
                {
                    asset.RenderAssetPreview();

                    if (ImGui.IsItemClicked())
                        SelectAsset(asset);

                    Editor.DoContextMenuFor(asset);

                    ImGui.SameLine();
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
