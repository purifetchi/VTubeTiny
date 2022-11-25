using CommandLine;

namespace VTTiny
{
    public class VTubeTinyCommandOptions
    {
        [Option('v', "Verbose", Required = false, HelpText = "Show Raylib logging.")]
        public bool Verbose { get; set; }

        [Option('f', "Config File", Required = false, HelpText = "The config file to load.")]
        public string ConfigFile { get; set; }
    }
}
