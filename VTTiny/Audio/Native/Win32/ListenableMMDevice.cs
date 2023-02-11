#if ARCH_WINDOWS

using NAudio.CoreAudioApi;

namespace VTTiny.Audio.Native.Win32
{
    /// <summary>
    /// A listenable CoreAudio MMDevice.
    /// </summary>
    internal class ListenableMMDevice : IListenableDevice
    {
        /// <inheritdoc/>
        public string Name { get; set; } = "[Unknown]";
        
        /// <inheritdoc/>
        public float Level => _backingDevice.AudioMeterInformation.MasterPeakValue;

        /// <summary>
        /// The device that backs this listenable device.
        /// </summary>
        private readonly MMDevice _backingDevice;

        /// <summary>
        /// The Windows Audio Session capture of this device.
        /// </summary>
        private WasapiCapture _wasapiCapture;

        /// <summary>
        /// Construct a new ListenableMMDevice from an MMDevice.
        /// </summary>
        /// <param name="backingDevice">The MMDevice.</param>
        public ListenableMMDevice(MMDevice backingDevice)
        {
            _backingDevice = backingDevice;

            Name = _backingDevice.FriendlyName;
        }

        /// <inheritdoc/>
        public void Listen()
        {
            if (_backingDevice.DataFlow == DataFlow.Render)
                return;

            if (_wasapiCapture != null)
                return;

            _wasapiCapture = new WasapiCapture(_backingDevice);
            _wasapiCapture.StartRecording();
        }

        /// <inheritdoc/>
        public void Stop()
        {
            if (_backingDevice.DataFlow == DataFlow.Render)
                return;

            if (_wasapiCapture == null)
                return;

            _wasapiCapture.StopRecording();
        }
    }
}

#endif
