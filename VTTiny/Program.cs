using CommandLine;
using VTTiny;

Parser.Default
      .ParseArguments<VTubeTinyCommandOptions>(args)
      .WithParsed(parsed => new VTubeTiny(parsed).Run());
