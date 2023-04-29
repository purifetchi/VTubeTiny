using VTTiny.Plugin.Discord.Services;

namespace VTTiny.Plugin.Discord.Config;

/// <summary>
/// Various configuration paths for the bot.
/// </summary>
public static class BotPaths
{
    /// <summary>
    /// The AppData directory.
    /// </summary>
    public static string AppData { get; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

    /// <summary>
    /// The VTubeTiny bot subdirectory.
    /// </summary>
    public static string DiscordBotDirectory { get; } = Path.Combine(AppData, "VTtinyDiscordBot");

    /// <summary>
    /// The token file.
    /// </summary>
    public static string TokenFile { get; } = Path.Combine(DiscordBotDirectory, "token.json");

    /// <summary>
    /// Create all the neccessary folders and files required for the bot to operate.
    /// </summary>
    public static void CreateFoldersAndFiles()
    {
        if (!Directory.Exists(DiscordBotDirectory))
            Directory.CreateDirectory(DiscordBotDirectory);

        if (File.Exists(TokenFile)) return;

        BotToken token = new();
        token.SetToken("Token");
    }
}