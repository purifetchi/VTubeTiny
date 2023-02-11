using System;
using ImGuiNET;
using VTTiny.Assets.Management;
using VTTiny.Base;
using VTTiny.Serialization;

namespace VTTiny.Assets
{
    /// <summary>
    /// An abstract asset used by VTubeTiny.
    /// </summary>
    public abstract class Asset : TypedSerializedObject, INamedObject
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

        /// <summary>
        /// Renders the editor gui for this asset.
        /// </summary>
        /// <returns>Whether we've modified the asset database collection (e.g. by removing an item).</returns>
        public bool RenderEditorGUI(AssetDatabase database)
        {
            ImGui.PushID($"{GetHashCode()}");

            if (ImGui.TreeNode(Name ?? Id.ToString()))
            {
                ImGui.Text("Asset Preview");
                RenderAssetPreview();

                ImGui.NewLine();

                ImGui.Text("Asset Settings");
                InternalRenderEditorGUI();

                if (ImGui.Button("Remove asset"))
                {
                    database.RemoveAsset(this);
                    return true;
                }

                ImGui.TreePop();
            }

            return false;
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
