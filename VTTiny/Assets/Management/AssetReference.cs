namespace VTTiny.Assets.Management
{
    /// <summary>
    /// Lightweight wrapper that holds a reference to an asset.
    /// </summary>
    /// <typeparam name="T">The type of the asset.</typeparam>
    public readonly struct AssetReference<T> where T : Asset
    {
        /// <summary>
        /// Id of the referenced asset.
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Constructs a new reference to an asset.
        /// </summary>
        /// <param name="asset">The asset to reference.</param>
        public AssetReference(T asset)
        {
            Id = asset.Id;
        }

        /// <summary>
        /// Resolves the asset from the id.
        /// </summary>
        /// <param name="database">The database to resolve from.</param>
        /// <returns>The asset or null.</returns>
        public T Resolve(AssetDatabase database)
        {
            return database.GetAsset<T>(Id);
        }
    }
}
