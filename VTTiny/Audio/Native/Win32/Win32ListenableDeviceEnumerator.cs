#if ARCH_WINDOWS

using System.Collections.Generic;
using NAudio.CoreAudioApi;

namespace VTTiny.Audio.Native.Win32
{
    /// <summary>
    /// Listenable device enumerator for the Win32 architecture.
    /// </summary>
    internal class Win32ListenableDeviceEnumerator : IListenableDeviceEnumerator
    {
        /// <summary>
        /// The enumerator for all private devices.
        /// </summary>
        private MMDeviceEnumerator DeviceEnumerator { get; set; }

        /// <summary>
        /// The default device.
        /// </summary>
        private IListenableDevice DefaultDevice { get; set; }

        /// <summary>
        /// Construct a new enumerator.
        /// </summary>
        public Win32ListenableDeviceEnumerator()
        {
            DeviceEnumerator = new();
        }

        /// <summary>
        /// Tries to get the default device.
        /// </summary>
        private void TryFindDefaultDevice()
        {
            if (DefaultDevice != null)
                return;

            var maybeDefaultDevice = DeviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Communications);
            if (maybeDefaultDevice == null)
                return;

            DefaultDevice = new ListenableMMDevice(maybeDefaultDevice);
        }

        /// <inheritdoc/>
        public IEnumerable<IListenableDevice> EnumerateAllListenableDevices()
        {
            // Try and find the default device, yield if able to.
            TryFindDefaultDevice();
            if (DefaultDevice != null)
                yield return DefaultDevice;

            foreach (var device in DeviceEnumerator.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active))
            {
                // We don't wanna yield the default device twice.
                if (device == DefaultDevice)
                    continue;

                yield return new ListenableMMDevice(device);
            }
        }

        /// <inheritdoc/>
        public IListenableDevice GetDefaultDevice()
        {
            TryFindDefaultDevice();
            return DefaultDevice;
        }
    }

}

#endif
