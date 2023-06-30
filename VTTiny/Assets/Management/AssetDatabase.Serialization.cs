using System;
using System.Collections.Generic;
using System.Linq;
using VTTiny.Data;

namespace VTTiny.Assets.Management;

public partial class AssetDatabase
{
    /// <summary>
    /// Load assets from a config.
    /// </summary>
    /// <param name="config">The config to load from.</param>
    public void LoadConfig(Config config)
    {
        _config = config.AssetDatabase;
        if (_config == null || _config.Assets == null)
            return;

        foreach (var item in _config.Assets)
        {
            // Resolve this assets type.
            var assetConfig = item.Value;
            if (!assetConfig.TryResolveType<Asset>(out Type assetType))
            {
                Console.WriteLine($"Couldn't resolve asset type for {assetConfig.Namespace}.{assetConfig.Type}");
                continue;
            }

            var asset = CreateAssetFromType(assetType, item.Key);
            asset.Name = assetConfig.Name;

            asset.InheritParametersFromConfig(assetConfig.Parameters);
        }
    }

    /// <summary>
    /// Packages this asset database into a config.
    /// </summary>
    /// <returns>The asset database config.</returns>
    public AssetDatabaseConfig PackageIntoConfig()
    {
        var config = new AssetDatabaseConfig
        {
            LastId = _assets.Count > 0 ? _assets.Keys.Max() : -1,
            Assets = new Dictionary<int, TypedObjectConfig>()
        };

        // Clone the assets list.
        var assets = GetAssets()
            .ToList();
        
        assets.Sort((l, r) =>
        {
            if (l.GetType() == r.GetType())
                return 0;

            var rightResolvesAfter = r.ResolvesAfter(l);
            var leftResolvesAfter = l.ResolvesAfter(r);

            if (rightResolvesAfter)
                return -1;
            else if (leftResolvesAfter)
                return 1;
            else
                return 0;
        });       

        foreach (var asset in assets)
            config.Assets.Add(asset.Id, asset.PackageIntoConfig());

        return config;
    }
}
