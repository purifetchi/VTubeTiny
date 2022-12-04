using CommandLine;
using VTTiny;

public class Program
{
    public static void Main(string[] args)
    {
        var parsed = Parser.Default.ParseArguments<VTubeTinyCommandOptions>(args).Value;
        new VTubeTiny(parsed).Run();
    }
}