using System;
using Raylib_cs;

namespace VTTiny
{
    public class Texture : IDisposable
    {
        /// <summary>
        /// The actual Raylib texture.
        /// </summary>
        public Texture2D BackingTexture { get; private set; }

        /// <summary>
        /// The path to the texture on disk.
        /// </summary>
        public string Path { get; private set; }

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
        public uint Id => BackingTexture.id;

        private bool _disposedValue;

        /// <summary>
        /// Constructs a texture loading it from a path.
        /// </summary>
        /// <param name="path">The path to the texture.</param>
        public Texture(string path)
        {
            LoadTexture(path);
        }

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
        private void LoadTexture(string path)
        {
            BackingTexture = Raylib.LoadTexture(path);
            Path = path;
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
    }
}
