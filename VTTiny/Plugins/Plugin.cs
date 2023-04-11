using System.IO;
using System.Linq;
using System.Reflection;

namespace VTTiny.Plugins
{
    /// <summary>
    /// A loaded plugin for VTubeTiny.
    /// </summary>
    internal sealed class Plugin
    {
        /// <summary>
        /// The assembly of this plugin.
        /// </summary>
        public Assembly PluginAssembly { get; init; }

        /// <summary>
        /// The name of this plugin.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// The description of this plugin.
        /// </summary>
        public string Description { get; init; }

        /// <summary>
        /// The author of this plugin.
        /// </summary>
        public string Author { get; init; }

        /// <summary>
        /// The version of this plugin.
        /// </summary>
        public string Version { get; init; }

        /// <summary>
        /// Constructs a new plugin, given its assembly.
        /// </summary>
        /// <param name="pluginAssembly">The plugin assembly.</param>
        private Plugin(Assembly pluginAssembly)
        {
            PluginAssembly = pluginAssembly;
        }

        /// <summary>
        /// Tries to load a new plugin given its path.
        /// </summary>
        /// <param name="path">The path to the plugin.</param>
        /// <returns>Either the loaded plugin, or null if it failed to load.</returns>
        public static Plugin TryLoadFromDirectory(string directory)
        {
            var dllPath = Directory.GetFiles(directory)
                .FirstOrDefault(file => Path.GetExtension(file) == ".dll");

            if (dllPath == null)
                return null;

            var assembly = Assembly.LoadFrom(dllPath);
            return new Plugin(assembly);
        }
        public static Plugin TryLoadFromFile(string file)
        {
            var assembly = Assembly.LoadFrom(file);
            return new Plugin(assembly);
        }
    }
}
