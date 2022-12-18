namespace VTTiny.Assets.Management
{
    public partial class AssetDatabase
    {
        /// <summary>
        /// Renders the asset database GUI.
        /// </summary>
        internal void RenderEditorGUI()
        {
            foreach (var asset in GetAssets())
            {
                if (asset.RenderEditorGUI(this))
                    break;
            }
        }
    }
}
