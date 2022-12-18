namespace VTTiny.Assets
{
    /// <summary>
    /// An abstract asset used by VTubeTiny.
    /// </summary>
    public abstract class Asset
    {
        /// <summary>
        /// The ID of this asset.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// The name of this asset.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Creates a new asset.
        /// </summary>
        /// <param name="id">The asset id.</param>
        /// <param name="name">The asset's name.</param>
        public Asset(int id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// Renders the editor gui for this asset.
        /// </summary>
        public virtual void RenderEditorGUI() { }
    }
}
