using System;
using System.Threading.Tasks;
using VTTiny.Plugin.Discord;
using VTTiny.Plugin.Discord.Services;

namespace VTTiny.Components.Discord.BotIntegration;

/// <summary>
/// Q: Why is this a singleton?
/// A: Because we want to have a single instance of the Discord bot running in the background
/// Q: Why should we have a single instance of the Discord bot running in the background?
/// A: Otherwise discord will have multiple bots running and we will have to deal with multiple connections
/// -  Making discord kinda mad, commands would fire ONCE to a single connection and not to all of them 
/// </summary>
public class DiscordAudioSingleton : IDisposable
{
    /// <summary>
    /// The bot.
    /// </summary>
    private DiscordBot _bot;

    /// <summary>
    /// The instance of the discord audio singleton.
    /// </summary>
    public static DiscordAudioSingleton Instance
    {
        get
        {
            lock (_instanceLock)
            {
                if (_instance == null)
                {
                    _instance = new DiscordAudioSingleton();
                    _instance.Start(); //only start the bot when we need it
                }
            }

            return _instance;
        }
    }

    private static DiscordAudioSingleton _instance;

    /// <summary>
    /// The synchronization lock for the instance object.
    /// </summary>
    private static readonly object _instanceLock = new();

    /// <summary>
    /// Is this user speaking?
    /// </summary>
    /// <param name="userName">The user's name.</param>
    /// <returns></returns>
    public bool IsUserSpeaking(string userName)
    {
        if (_bot == null || !_bot.IsRunning)
            return false;
        return NameProvider.Instance.IsUserSpeaking(userName);
    }

    /// <summary>
    /// Starts the bot.
    /// </summary>
    private void Start()
    {
        //Will dispose of this singleton when discord bot closes
        Task.Run(() =>
        {
            _bot = new DiscordBot();

            _bot.RunAsync()
                .Wait();
        });
    }

    /// <summary>
    /// Disposes of the bot.
    /// </summary>
    public void Dispose()
    {
        //Dispose of this singleton
        _bot.StopAsync()
            .Wait();
        _bot = null;
        _instance = null;
    }
}