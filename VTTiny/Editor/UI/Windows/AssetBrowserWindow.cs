using System;
using System.IO;
using ImGuiNET;
using Raylib_cs;
using VTTiny.Assets;
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

            LoadBasedOnExtension(path);
        }

        /// <summary>
        /// Loads an asset based on the extension.
        /// </summary>
        /// <param name="path">The path to the asset.</param>
        private void LoadBasedOnExtension(string path)
        {
            var extension = Path.GetExtension(path);

            switch (extension.ToLower())
            {
                case ".png":
                case ".bmp":
                    var texture = Stage.AssetDatabase.CreateAsset<Texture>();
                    texture.Name = Path.GetFileName(path);
                    texture.LoadTextureFromFile(path);
                    break;

                case ".gif":
                    var gifTexture = Stage.AssetDatabase.CreateAsset<GifTexture>();
                    gifTexture.Name = Path.GetFileName(path);
                    gifTexture.LoadTextureFromFile(path);
                    break;

                default:
                    Console.WriteLine($"Unknown asset at path {path}");
                    return;
            }
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
                Stage.AssetDatabase.RenderEditorGUI();
            }

            HandleDragAndDropAssets();
        }

        public void OnStageChange(Stage stage)
        {
            Stage = stage;
        }
    }
}
