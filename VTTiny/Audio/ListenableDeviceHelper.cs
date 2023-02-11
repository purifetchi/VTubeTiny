using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using VTTiny.Extensions;

namespace VTTiny.Audio
{
    /// <summary>
    /// Helper class for getting listenable devices.
    /// </summary>
    public static class ListenableDeviceHelper
    {
        /// <summary>
        /// The enumerator list.
        /// </summary>
        private static List<IListenableDeviceEnumerator> _enumerators;

        /// <summary>
        /// The list of listenable devices.
        /// </summary>
        private static List<IListenableDevice> _listenableDevices;

        static ListenableDeviceHelper()
        {
            FetchAllEnumerators();
        }

        /// <summary>
        /// Fetches and instantiates all the enumerators from the assembly.
        /// </summary>
        private static void FetchAllEnumerators()
        {
            _enumerators = new();

            // Get all the listenable enumerator types via reflection.
            // This is kindaaaa bad but it should work for now.
            var enumeratorTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && t.IsAssignableTo(typeof(IListenableDeviceEnumerator)));

            foreach (var enumeratorType in enumeratorTypes)
                _enumerators.Add(enumeratorType.Construct<IListenableDeviceEnumerator>());

            Console.WriteLine($"enumerator count: {_enumerators.Count}");
        }

        /// <summary>
        /// Enumerates all the devices.
        /// </summary>
        private static void EnumerateAllDevices()
        {
            _listenableDevices = new();
         
            foreach (var enumerator in _enumerators)
                _listenableDevices.AddRange(enumerator.EnumerateAllListenableDevices());
        }

        /// <summary>
        /// Gets the default device from the first loaded enumerator.
        /// </summary>
        /// <returns>The default device.</returns>
        public static IListenableDevice GetFirstDefaultDevice()
        {
            return _enumerators.First().GetDefaultDevice();
        }

        /// <summary>
        /// Returns a list of all listenable devices.
        /// </summary>
        /// <returns>A read-only list of listenable devices.</returns>
        public static IReadOnlyList<IListenableDevice> GetAllListenableDevices()
        {
            if (_listenableDevices == null)
                EnumerateAllDevices();

            return _listenableDevices;
        }

        /// <summary>
        /// Gets a listenable device by its name.
        /// </summary>
        /// <param name="name">The name of the listenable device.</param>
        /// <returns>The listenable device or null if it wasn't found.</returns>
        public static IListenableDevice GetListenableDeviceByName(string name)
        {
            if (_listenableDevices == null)
                EnumerateAllDevices();

            return _listenableDevices.FirstOrDefault(device => device.Name.Contains(name));
        }
    }
}
