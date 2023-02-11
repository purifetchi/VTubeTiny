using VTTiny.Base;

namespace VTTiny.Audio
{
    /// <summary>
    /// The generic listenable device interface. Must be inherited by anything that wants to be listenable to.
    /// </summary>
    public interface IListenableDevice : INamedObject
    {
        /// <summary>
        /// Starts listening to this device.
        /// </summary>
        void Listen();
        
        /// <summary>
        /// Stops listening to this device.
        /// </summary>
        void Stop();

        /// <summary>
        /// The current audio level of this device.
        /// </summary>
        float Level { get; }
    }
}
