using CommandLine;

namespace VTTiny
{
    /// <summary>
    /// The command line options for VTubeTiny.
    /// </summary>
    public class VTubeTinyCommandOptions
    {
        [Option('v', "Verbose", Required = false, HelpText = "Show Raylib logging.")]
        public bool Verbose { get; set; }

        [Option('f', "Config File", Required = false, HelpText = "The config file to load.")]
        public string ConfigFile { get; set; }

        [Option('s', "Stage Viewer Mode", Required = false, HelpText = "Launch into the stage viewer mode. (VTubeTiny will launch into the editor mode otherwise.)")]
        public bool StageViewerMode { get; set; }
    }
}
