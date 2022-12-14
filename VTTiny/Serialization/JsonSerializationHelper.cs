using System.IO;
using System.Text.Json;
using VTTiny.Data;
using VTTiny.Scenery;

namespace VTTiny.Serialization
{
    /// <summary>
    /// Contains various utilities to assist with (de)serialization of VTubeTiny data.
    /// </summary>
    internal static class JsonSerializationHelper
    {
        /// <summary>
        /// The default options for the serializer.
        /// </summary>
        private static JsonSerializerOptions SerializerOptions { get; } = new JsonSerializerOptions
        {
            IncludeFields = true,
            PropertyNameCaseInsensitive = true,
            AllowTrailingCommas = true,
        };

        /// <summary>
        /// Exports a stage into a json file.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <param name="stage">The stage to export.</param>
        public static void ExportStageToFile(string path, Stage stage)
        {
            var config = stage.PackageStageIntoConfig();
            var serialized = JsonSerializer.SerializeToElement(config, SerializerOptions).ToString();
            File.WriteAllText(path, serialized);
        }

        /// <summary>
        /// Loads a config from a json file.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <returns>The resulting config.</returns>
        public static Config LoadConfigFromFile(string path)
        {
            var data = File.ReadAllText(path);
            return JsonSerializer.Deserialize<Config>(data, SerializerOptions);
        }

        /// <summary>
        /// Converts a JsonElement to a type.
        /// </summary>
        /// <typeparam name="T">The result type.</typeparam>
        /// <param name="element">The JsonElement.</param>
        /// <returns>The resulting value of a type.</returns>
        public static T JsonElementToType<T>(JsonElement element)
        {
            return JsonSerializer.Deserialize<T>(element, SerializerOptions);
        }

        /// <summary>
        /// Converts an object to a JsonElement.
        /// </summary>
        /// <param name="obj">The object to convert.</param>
        /// <returns>The resulting JsonElement.</returns>
        public static JsonElement? ObjectToJsonElement(object obj)
        {
            return JsonSerializer.SerializeToElement(obj, SerializerOptions);
        }
    }
}
