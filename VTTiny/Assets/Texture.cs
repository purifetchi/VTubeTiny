using System;
using System.Text.Json;
using Raylib_cs;
using VTTiny.Assets.Data;
using VTTiny.Editor;

namespace VTTiny.Assets
{
    /// <summary>
    /// A wrapper around Raylib's Texture2D class that can auto-manage its lifetime.
    /// </summary>
    public class Texture : Asset, IDisposable
    {
        /// <summary>
        /// The actual Raylib texture.
        /// </summary>
        public virtual Texture2D BackingTexture { get; protected set; }

        /// <summary>
        /// The path to the texture on disk.
        /// </summary>
        public string Path { get; protected set; }

        /// <summary>
        /// The width of the texture.
        /// </summary>
        public int Width => BackingTexture.width;

        /// <summary>
        /// The height of the texture.
        /// </summary>
        public int Height => BackingTexture.height;

        /// <summary>
        /// The internal id of this texture.
        /// </summary>
        public uint TextureId => BackingTexture.id;

        /// <summary>
        /// The current filtering mode for this texture.
        /// </summary>
        public TextureFilter FilteringMode { get; private set; } = TextureFilter.TEXTURE_FILTER_POINT;

        private bool _disposedValue;

        /// <summary>
        /// The destructor, automatically frees the texture.
        /// </summary>
        ~Texture()
        {
            Dispose(disposing: false);
        }

        /// <summary>
        /// Loads a texture from a given path.
        /// </summary>
        /// <param name="path">The path to the texture.</param>
        public virtual void LoadTextureFromFile(string path)
        {
            BackingTexture = Raylib.LoadTexture(path);
            Path = path;
        }

        /// <summary>
        /// Sets the filtering mode for this texture.
        /// </summary>
        /// <param name="filter">The filtering mode for this texture.</param>
        public void SetTextureFilterMode(TextureFilter filter)
        {
            if (FilteringMode == filter)
                return;

            Raylib.SetTextureFilter(BackingTexture, filter);
            FilteringMode = filter;

            // If we're above trilinear filtering, generate mipmaps.
            if ((int)FilteringMode >= (int)TextureFilter.TEXTURE_FILTER_TRILINEAR)
                GenerateMipmaps();
        }

        /// <summary>
        /// Generates mipmaps for this texture.
        /// </summary>
        protected virtual void GenerateMipmaps()
        {
            var texture = BackingTexture;
            Raylib.GenTextureMipmaps(ref texture);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                Raylib.UnloadTexture(BackingTexture);
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public override void Destroy()
        {
            Dispose();
        }

        internal override void InheritParametersFromConfig(JsonElement? parameters)
        {
            var config = JsonObjectToConfig<TextureConfig>(parameters);
            LoadTextureFromFile(config.Path);
            SetTextureFilterMode(config.FilteringMode);
        }

        protected override object PackageParametersIntoConfig()
        {
            return new TextureConfig
            {
                Path = Path,
                FilteringMode = FilteringMode
            };
        }

        public override void RenderAssetPreview()
        {
            EditorGUI.ImageButton(this, 100, 100);
        }

        protected override void InternalRenderEditorGUI()
        {
            var isSmooth = EditorGUI.Checkbox("Smooth Filtering", FilteringMode == TextureFilter.TEXTURE_FILTER_TRILINEAR);
            SetTextureFilterMode(isSmooth ? TextureFilter.TEXTURE_FILTER_TRILINEAR : TextureFilter.TEXTURE_FILTER_POINT);
        }
    }
}
