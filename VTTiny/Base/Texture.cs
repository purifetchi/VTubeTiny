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
        /// Constructs a texture loading it from a path.
        /// </summary>
        /// <param name="path">The path to the texture.</param>
        public Texture(string path)
        {
            LoadTexture(path);
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

        public void Dispose()
        {
            Raylib.UnloadTexture(BackingTexture);
        }
    }
}
