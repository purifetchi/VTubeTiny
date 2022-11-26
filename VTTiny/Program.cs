using System.IO;
using CommandLine;
using Newtonsoft.Json;
using VTTiny;
using VTTiny.Data;

public class Program
{
    public static void Main(string[] args)
    {
        var parsed = Parser.Default.ParseArguments<VTubeTinyCommandOptions>(args).Value;

        Config config = null;
        if (!string.IsNullOrEmpty(parsed.ConfigFile))
        {
            var configData = File.ReadAllText(parsed.ConfigFile);
            config = JsonConvert.DeserializeObject<Config>(configData);
        }

        new VTubeTiny(config, parsed.Verbose, parsed.EditorMode).Run();
    }
}