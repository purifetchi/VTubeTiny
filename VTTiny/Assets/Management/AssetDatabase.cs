using System;
using System.Collections.Generic;
using System.Linq;
using VTTiny.Base;
using VTTiny.Data;
using VTTiny.Extensions;

namespace VTTiny.Assets.Management
{
    /// <summary>
    /// The class responsible for loading and managing loaded assets.
    /// </summary>
    public partial class AssetDatabase
    {
        /// <summary>
        /// The amount of assets.
        /// </summary>
        public int AssetCount => _assets.Count;

        /// <summary>
        /// The config.
        /// </summary>
        private AssetDatabaseConfig _config;

        /// <summary>
        /// The asset database.
        /// </summary>
        private readonly Dictionary<int, Asset> _assets;

        /// <summary>
        /// The lazily instantiated id allocator.
        /// 
        /// We lazily instantiate it, since there's no reason for us to use it, if we don't need to create new assets.
        /// </summary>
        private readonly Lazy<IdAllocator> _idAllocator;

        /// <summary>
        /// Creates a new asset database.
        /// </summary>
        public AssetDatabase()
        {
            _assets = new();
            _idAllocator = new(() => new IdAllocator(_config?.LastId ?? -1));
        }

        /// <summary>
        /// Creates a new asset of a type.
        /// </summary>
        /// <typeparam name="T">The type of the asset.</typeparam>
        /// <returns>The asset.</returns>
        public T CreateAsset<T>() where T : Asset, new()
        {
            var id = _idAllocator.Value.AllocateId();
            return CreateAsset<T>(id);
        }

        /// <summary>
        /// Creates a new asset of a type with a specified id.
        /// </summary>
        /// <typeparam name="T">The type of the asset.</typeparam>
        /// <param name="id">The id of the asset.</param>
        /// <returns>The asset.</returns>
        private T CreateAsset<T>(int id) 
            where T : Asset, new()
        {
            var asset = new T
            {
                Id = id,
                Name = $"{typeof(T).Name} ({id})",
                Database = this
            };
            
            _assets[id] = asset;
            return asset;
        }

        /// <summary>
        /// Creates an asset from its type class and its id.
        /// </summary>
        /// <param name="assetType">The type of the asset.</param>
        /// <param name="id">The id of the asset.</param>
        /// <returns>The asset.</returns>
        private Asset CreateAssetFromType(Type assetType, int id)
        {
            var asset = assetType.Construct<Asset>();

            asset.Id = id;
            asset.Database = this;
            _assets[id] = asset;

            return asset;
        }

        /// <summary>
        /// Removes an asset.
        /// </summary>
        /// <param name="asset">The asset to remove.</param>
        public bool RemoveAsset(Asset asset)
        {
            if (asset == null)
                return false;

            if (!_assets.ContainsKey(asset.Id))
                return false;

            _idAllocator.Value.FreeId(asset.Id);
            asset.Destroy();

            return _assets.Remove(asset.Id);
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
        public IEnumerable<Asset> GetAssets()
        {
            return _assets.Values;
        }

        /// <summary>
        /// Gets all assets of a specific type.
        /// </summary>
        /// <typeparam name="T">The type of the assets.</typeparam>
        /// <returns>All of the assets of a specific type.</returns>
        public IEnumerable<T> GetAllAssetsOfType<T>() where T : Asset
        {
            return _assets.Where(pair => pair.Value is T)
                          .OrderBy(pair => pair.Key)
                          .Select(pair => pair.Value as T);
        }

        /// <summary>
        /// Destroys this asset database.
        /// </summary>
        public void Destroy()
        {
            foreach (var asset in GetAssets())
                asset.Destroy();

            _assets.Clear();
        }
    }
}
