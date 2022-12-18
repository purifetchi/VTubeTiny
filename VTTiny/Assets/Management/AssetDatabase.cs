using System;
using System.Collections.Generic;
using VTTiny.Assets.Management;

namespace VTTiny.Assets.Management
{
    /// <summary>
    /// The class responsible for loading and managing loaded assets.
    /// </summary>
    public partial class AssetDatabase
    {
        /// <summary>
        /// The asset database.
        /// </summary>
        private readonly Dictionary<int, Asset> _assets;

        /// <summary>
        /// The lazily instantiated id allocator.
        /// 
        /// We lazily instantiate it, since there's no reason for us to use it, if we don't need to create new assets.
        /// </summary>
        private readonly Lazy<AssetIdAllocator> _idAllocator;

        /// <summary>
        /// Creates a new asset database.
        /// </summary>
        public AssetDatabase()
        {
            _assets = new();
            _idAllocator = new();
        }

        /// <summary>
        /// Get an asset by its id and type.
        /// </summary>
        /// <typeparam name="T">The type of the asset.</typeparam>
        /// <param name="id">The asset's id.</param>
        /// <returns>Either null if we couldn't find an asset with said id and type, or the asset.</returns>
        public T GetAsset<T>(int id) where T : Asset
        {
            if (!_assets.TryGetValue(id, out var asset))
                return default;

            if (asset is not T typedAsset)
                return default;

            return typedAsset;
        }

        /// <summary>
        /// Gets all of the assets.
        /// </summary>
        /// <returns>The assets.</returns>
        public IEnumerable<KeyValuePair<int, Asset>> GetAssets()
        {
            return _assets;
        }
    }
}
