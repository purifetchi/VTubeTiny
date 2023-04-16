using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace VTTiny.Plugins
{
    /// <summary>
    /// A manager class tasked with loading and storing all of the plugins.
    /// </summary>
    internal  static  class PluginManager
    {
        /// <summary>
        /// The path of the plugins subdirectory.
        /// </summary>
        private static string PLUGINS_FOLDER_PATH = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, System.AppDomain.CurrentDomain.RelativeSearchPath ?? "", "Plugins");

        /// <summary>
        /// The currently loaded plugins.
        /// </summary>
        private static List<Plugin> _plugins;

        /// <summary>
        /// Initializes the manager and loads all of the plugins.
        /// </summary>
        public static void LoadAllPlugins()
        {
            Console.WriteLine(PLUGINS_FOLDER_PATH);
            _plugins = new();
            if (!Directory.Exists(PLUGINS_FOLDER_PATH))
                Directory.CreateDirectory(PLUGINS_FOLDER_PATH);
            //First boot after download will not have the plugins folder, and crash
            
            foreach (var file in Directory.GetFiles(PLUGINS_FOLDER_PATH)) // Apparently this was all directories, not files.
            {
                Console.WriteLine(file);
                var plugin = Plugin.TryLoadFromFile(file);

                if (plugin == null)
                {
                    Console.WriteLine($"Failed to load plugin {file}.");
                    continue;
                }

                Console.WriteLine($"Loaded plugin {plugin.Name}!");
                _plugins.Add(plugin);
            }

            foreach (var dir in Directory.GetDirectories(PLUGINS_FOLDER_PATH))
            {
                var plugin = Plugin.TryLoadFromDirectory(dir);
                if (plugin == null)
                {
                    Console.WriteLine($"Failed to load plugin {dir}.");
                    continue;
                }
                Console.WriteLine($"Loaded plugin {plugin.Name}!");
                _plugins.Add(plugin);
            }
        }

        /// <summary>
        /// Gets all the currently loaded in assemblies.
        /// </summary>
        /// <returns>An enumerable of all of the loaded assemblies (including the main VTubeTiny assembly).</returns>
        public static IEnumerable<Assembly> GetAllLoadedAssemblies()
        {
            // We always want to return the VTubeTiny assembly first, to have priority when loading.
            yield return Assembly.GetExecutingAssembly();

            foreach (var plugin in _plugins)
                yield return plugin.PluginAssembly;
        }

        /// <summary>
        /// Gets all the types from the loaded assemblies.
        /// </summary>
        /// <returns>The types.</returns>
        public static IEnumerable<Type> GetTypesInLoadedAssemblies()
        {
            foreach (var assembly in GetAllLoadedAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                    yield return type;
            }
        }

        /// <summary>
        /// Finds and returns the type given its name in all the loaded assemblies.
        /// </summary>
        /// <param name="typeName">The type name.</param>
        /// <returns>The type if it was found, or null.</returns>
        public static Type FindTypeInLoadedAssemblies(string typeName)
        {
            foreach (var assembly in GetAllLoadedAssemblies())
            {
                var type = assembly.GetType(typeName);

                if (type != null)
                    return type;
            }

            return null;
        }
    }
}
