using System.Text.Json;

namespace VTTiny.Data
{
    /// <summary>
    /// Typed serialized object configuration data.
    /// </summary>
    public class TypedObjectConfig
    {
        public string Namespace { get; set; } = "VTTiny.Components";
        public string Type { get; set; }
        public JsonElement? Parameters { get; set; }
    }
}
