using System;
using Raylib_cs;

namespace VTTiny.Assets;

/// <summary>
/// A sound file.
/// </summary>
internal class Sound : Asset,
    IDisposable
{
    /// <summary>
    /// The sound.
    /// </summary>
    private Raylib_cs.Sound _sound;

    private bool _disposedValue;

    /// <summary>
    /// Loads a sound file from path.
    /// </summary>
    /// <param name="path">The path.</param>
    public void LoadSoundFromFile(string path)
    {
        _sound = Raylib.LoadSound(path);
    }

    /// <summary>
    /// Plays this sound file.
    /// </summary>
    public void PlayOnce()
    {
        Raylib.PlaySoundMulti(_sound);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            Raylib.UnloadSound(_sound);
            _disposedValue = true;
        }
    }

    ~Sound()
    {
         Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
