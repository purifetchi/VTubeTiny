using System;
using System.IO;

namespace VTTiny.Assets.Management
{
    /// <summary>
    /// Helper functions for working with assets.
    /// </summary>
    public static class AssetHelper
    {
        /// <summary>
        /// Loads an asset based on the extension.
        /// </summary>
        /// <param name="path">The path to the asset.</param>
        /// <param name="assetDatabase">The asset database to load into.</param>
        public static Asset LoadBasedOnExtension(string path, AssetDatabase assetDatabase)
        {
            var extension = Path.GetExtension(path);
            var name = Path.GetFileName(path);

            switch (extension.ToLower())
            {
                case ".png":
                case ".bmp":
                    var texture = assetDatabase.CreateAsset<Texture>();
                    texture.LoadTextureFromFile(path);
                    texture.Name = name;
                    return texture;

                case ".gif":
                    var gifTexture = assetDatabase.CreateAsset<GifTexture>();
                    gifTexture.LoadTextureFromFile(path);
                    gifTexture.Name = name;
                    return gifTexture;

                case ".wav":
                case ".mp3":
                case ".ogg":
                    var sound = assetDatabase.CreateAsset<Sound>();
                    sound.LoadSoundFromFile(path);
                    sound.Name = name;
                    return sound;

                default:
                    return null;
            }
        }
    }
}
