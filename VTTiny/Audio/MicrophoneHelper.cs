using System;
using System.Collections.Generic;
using System.Linq;
using NAudio.CoreAudioApi;

namespace VTTiny.Audio
{
    /// <summary>
    /// Helper class for querying microphones.
    /// </summary>
    internal static class MicrophoneHelper
    {
        /// <summary>
        /// The enumerator for all private devices.
        /// </summary>
        private static MMDeviceEnumerator DeviceEnumerator { get; set; }

        /// <summary>
        /// A list containing all of the enumerated microphone devices.
        /// </summary>
        private static List<MMDevice> _microphones;

        /// <summary>
        /// The lazy initialized list of microphone names.
        /// 
        /// Used inside of the editor, since querying the friendly name took a ton of time and lagged the editor.
        /// </summary>
        private static Lazy<string[]> _microphoneNames;

        static MicrophoneHelper() 
        {
            EnumerateDevices();
        }

        /// <summary>
        /// Refreshes the device enumerator.
        /// </summary>
        public static void EnumerateDevices() 
        { 
            DeviceEnumerator = new();
            _microphones = new(DeviceEnumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active));
            _microphoneNames = new(() => _microphones.Select(mic => mic.FriendlyName).ToArray());
        }

        /// <summary>
        /// Gets the default microphone's name.
        /// </summary>
        /// <returns>The default microphone's name.</returns>
        public static MMDevice GetDefaultMicrophone()
        {
            return DeviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Communications);
        }

        /// <summary>
        /// Returns a collection of all of the microphone devices.
        /// </summary>
        /// <returns>The microphone device list.</returns>
        public static IReadOnlyList<MMDevice> GetMicrophones()
        {
            return _microphones.AsReadOnly();
        }

        /// <summary>
        /// Returns a collection of all the microphone names.
        /// </summary>
        /// <returns>A collection of all the microphone names.</returns>
        public static IReadOnlyList<string> GetMicrophoneNames()
        {
            return _microphoneNames.Value;
        }

        /// <summary>
        /// Gets the cached microphone name of a device, avoiding the system lookup.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <returns>The cached name.</returns>
        public static string GetMicrophoneNameFast(MMDevice device)
        {
            var index = _microphones.FindIndex(mic => mic == device);
            if (index < 0)
                return null;

            return _microphoneNames.Value[index];
        }

        /// <summary>
        /// Gets the microphone by its name.
        /// </summary>
        /// <param name="name">The name of the microphone.</param>
        /// <returns>The microphone or null if it wasn't found.</returns>
        public static MMDevice GetMicrophoneByName(string name)
        {
            return _microphones.FirstOrDefault(device => device.FriendlyName.Contains(name));
        }
    }
}
