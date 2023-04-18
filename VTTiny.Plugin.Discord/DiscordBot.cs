using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.VoiceNext;
using Microsoft.Extensions.DependencyInjection;
using VTTiny.Plugin.Discord.Commands;
using VTTiny.Plugin.Discord.Services;

public class DiscordBot
{
    public DiscordShardedClient _client;
    private readonly string _token;
    private readonly IServiceCollection _services;
    public readonly IServiceScopeFactory _scopeFactory;
    public IServiceProvider _serviceProvider;
    public bool IsRunning { get; set; }

    /// <summary>
    /// Creates a new DiscordBot instance
    /// </summary>
    /// <param name="token">Token Via commandline otherwise it'll take it from disk</param>
    public DiscordBot(string? token = null)
    {
        string Token = token ?? new Token().getToken(); 
        IsRunning = false;
        _token = token ?? Token;
        _services = new ServiceCollection();
        _services.AddVTubeTinyDiscordServices();
        _scopeFactory = _services.BuildServiceProvider().GetService<IServiceScopeFactory>();
        _serviceProvider = _services.BuildServiceProvider();
    }

    public async Task RunAsync()
    {
        Console.WriteLine("Configuring DiscordBot");
        var config = new DiscordConfiguration
        {
            Token = _token,
            TokenType = TokenType.Bot,
            AutoReconnect = true,
            MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug,
            Intents = DiscordIntents.AllUnprivileged
        };
        Console.WriteLine("Configured DiscordBot");

        _client = new DiscordShardedClient(config);
        Console.WriteLine("Scoping commands");
        try
        {
            var slash = await _client.UseSlashCommandsAsync(new SlashCommandsConfiguration
            {
                //Services in here
                Services = _scopeFactory.CreateScope().ServiceProvider,
            });
            Console.WriteLine("Registering commands");
            RegisterSlashCommands(slash);
            Console.WriteLine("Registered commands");
            await _client.UseVoiceNextAsync(new VoiceNextConfiguration()
            {
                EnableIncoming = true,
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Console.WriteLine("Failed to register commands");
        }

        Console.WriteLine("Registering events");
        RegisterEvents();
        Console.WriteLine("Starting client");
        await _client.StartAsync();
        Console.WriteLine("Started client");
        

        await Task.Delay(-1);
    }

    private void RegisterEvents()
    {
        _client.Ready += async (sender, args) =>
        {
            Console.WriteLine("Client is ready to process events.");
            await sender.UpdateStatusAsync(new DiscordActivity("Vtubers grow", ActivityType.Watching));
            Console.WriteLine($"Registration complete for {sender.CurrentUser.Username}");
            IsRunning = true;
        };
        _client.GuildAvailable += async (sender, args) => { Console.WriteLine($"Guild available: {args.Guild.Name}"); };

    }

    private static void RegisterSlashCommands(IReadOnlyDictionary<int, SlashCommandsExtension> slash)
    {
        slash.RegisterCommands<VoiceChannelCommands>();

    }
    public async Task StopAsync()
    {
        await _client.StopAsync();
    }
}