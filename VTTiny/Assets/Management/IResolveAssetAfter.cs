namespace VTTiny.Assets.Management;

/// <summary>
/// Forces the asset database to load this asset after assets of a different kind have been loaded.
/// So, if an asset requires textures to be already present, we will stall loading this, until
/// the textures have been loaded.
/// </summary>
/// <typeparam name="TAsset">The asset type.</typeparam>
internal interface IResolveAssetAfter<TAsset>
    where TAsset : Asset
{
}
