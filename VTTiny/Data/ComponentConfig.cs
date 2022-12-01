using Newtonsoft.Json.Linq;

namespace VTTiny.Data
{
    /// <summary>
    /// Component configuration data.
    /// </summary>
    public class ComponentConfig
    {
        public string Namespace { get; set; } = "VTTiny.Components";
        public string Type { get; set; }
        public JObject Parameters { get; set; }
    }
}
