using VTTiny.Assets.Management;
using VTTiny.Serialization;

namespace VTTiny.Assets
{
    /// <summary>
    /// An abstract asset used by VTubeTiny.
    /// </summary>
    public abstract class Asset : TypedSerializedObject
    {
        /// <summary>
        /// The ID of this asset.
        /// </summary>
        public int Id { get; internal set; }

        /// <summary>
        /// The name of this asset.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Constructs a reference to this asset.
        /// </summary>
        /// <typeparam name="T">The type of the asset.</typeparam>
        /// <returns>The reference.</returns>
        public AssetReference<T> ToAssetReference<T>() where T : Asset
        {
            var reference = new AssetReference<T>
            {
                Id = Id
            };

            return reference;
        }

        /// <summary>
        /// Renders the editor gui for this asset.
        /// </summary>
        public virtual void RenderEditorGUI() { }

        /// <summary>
        /// Destroys this asset and frees all things associated with it.
        /// </summary>
        public virtual void Destroy() { }
    }
}
