#if ARCH_WINDOWS

using System;
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
        public float Level
        {
            get
            {
                // I am VERY not proud of this code, but WASAPI is giving me headaches with weird-ass
                // race conditions while switching the capturing state.
                // I assume this is because NAudio's capture state is volatile...
                try
                {
                    // Force start the recording if the capture is stopped and we're supposed to be listening.
                    // This will trigger when Listen() will do absolutely nothing due to the thing mentioned above.
                    if (_deviceDataFlow != DataFlow.Render)
                    {
                        if (_wasapiCapture.CaptureState == CaptureState.Stopped && _isSupposedToBeListening)
                            _wasapiCapture.StartRecording();
                    }

                    return _backingDevice.AudioMeterInformation.MasterPeakValue;
                }
                catch
                {
                    // If we actually get an error while trying to spin up a recording, just return 0, lol.
                    return 0f;
                }
            }
        }

        /// <summary>
        /// Whether this MMDevice is supposed to be listened to.
        /// </summary>
        private bool _isSupposedToBeListening = false;

        /// <summary>
        /// The device that backs this listenable device.
        /// </summary>
        private readonly MMDevice _backingDevice;

        /// <summary>
        /// The Windows Audio Session capture of this device.
        /// </summary>
        private WasapiCapture _wasapiCapture;

        /// <summary>
        /// The device data flow, cached for performance.
        /// </summary>
        private readonly DataFlow _deviceDataFlow;

        /// <summary>
        /// Construct a new ListenableMMDevice from an MMDevice.
        /// </summary>
        /// <param name="backingDevice">The MMDevice.</param>
        public ListenableMMDevice(MMDevice backingDevice)
        {
            _backingDevice = backingDevice;
            _deviceDataFlow = backingDevice.DataFlow;

            Name = _backingDevice.FriendlyName;
        }

        /// <inheritdoc/>
        public void Listen()
        {
            if (_deviceDataFlow == DataFlow.Render)
                return;

            if (_wasapiCapture != null && _wasapiCapture.CaptureState != CaptureState.Stopped)
                return;

            _wasapiCapture ??= new WasapiCapture(_backingDevice);
            _wasapiCapture.StartRecording();

            _isSupposedToBeListening = true;
        }

        /// <inheritdoc/>
        public void Stop()
        {
            if (_deviceDataFlow == DataFlow.Render)
                return;

            if (_wasapiCapture == null)
                return;

            _wasapiCapture.StopRecording();

            _isSupposedToBeListening = false;
        }
    }
}

#endif
