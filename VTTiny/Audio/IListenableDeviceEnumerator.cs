using System.Collections.Generic;

namespace VTTiny.Audio
{
    /// <summary>
    /// An interface for everything that wants to return an enumerable collection of IListenableDevices.
    /// </summary>
    public interface IListenableDeviceEnumerator
    {
        /// <summary>
        /// List all the listenable devices of this enumerator.
        /// </summary>
        /// <returns>An IEnumerable of all listenable devices this enumerator has.</returns>
        IEnumerable<IListenableDevice> EnumerateAllListenableDevices();

        /// <summary>
        /// Get the default listenable device.
        /// </summary>
        /// <returns>The default listenable device.</returns>
        IListenableDevice GetDefaultDevice();
    }
}
