using System;
using ImGuiNET;
using VTTiny.Assets.Management;
using VTTiny.Editor;
using VTTiny.Serialization;

namespace VTTiny.Assets
{
    /// <summary>
    /// An abstract asset used by VTubeTiny.
    /// </summary>
    public abstract class Asset : TypedSerializedObject,
        IEditorGUIDrawable,
        IHasRightClickContext
    {
        /// <summary>
        /// The ID of this asset.
        /// </summary>
        public int Id { get; internal set; }

        /// <summary>
        /// The name of this asset.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The asset database this asset is associated with.
        /// </summary>
        public AssetDatabase Database { get; set; }

        /// <summary>
        /// Constructs a reference to this asset.
        /// </summary>
        /// <typeparam name="T">The type of the asset.</typeparam>
        /// <returns>The reference.</returns>
        public AssetReference<T> ToAssetReference<T>() where T : Asset
        {
            if (this is not T typedAsset)
                throw new NotSupportedException($"Cannot construct an asset reference of type {typeof(T).FullName} (since asset is of type {GetType().FullName}).");

            return new AssetReference<T>(typedAsset);
        }

        /// <inheritdoc/>
        public void RenderEditorGUI()
        {
            ImGui.Text("Asset Preview");
            RenderAssetPreview();

            ImGui.Text("Asset Settings");
            InternalRenderEditorGUI();
        }

        /// <inheritdoc/>
        public void RenderContextMenu()
        {
            if (ImGui.MenuItem("Remove Asset"))
            {
                Database.RemoveAsset(this);
                ImGui.EndPopup();
            }
        }

        /// <summary>
        /// Renders the asset-specific editor gui.
        /// </summary>
        protected virtual void InternalRenderEditorGUI() { }

        /// <summary>
        /// Renders the preview for this asset.
        /// </summary>
        public virtual void RenderAssetPreview() { }

        /// <summary>
        /// Destroys this asset and frees all things associated with it.
        /// </summary>
        public virtual void Destroy() { }

        protected override string GetNameForSerialization()
        {
            return Name;
        }
    }
}
