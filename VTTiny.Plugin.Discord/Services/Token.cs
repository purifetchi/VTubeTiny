using System.Text.Json;
using VTTiny.Plugin.Discord.Config;

namespace VTTiny.Plugin.Discord.Services;

/// <summary>
/// A token for the discord bot.
/// </summary>
public class BotToken
{
    /// <summary>
    /// The string Token data.
    /// </summary>
    public string? Token { get; set; }

    /// <summary>
    /// Gets the token from the token file.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidDataException"></exception>
    public string GetToken()
    {
        BotPaths.CreateFoldersAndFiles();

        try
        {
            var tk = JsonSerializer.Deserialize<BotToken>(File.ReadAllText(BotPaths.TokenFile));
            Token = tk?.Token;
            if (string.IsNullOrEmpty(Token))
                throw new InvalidDataException($"The token file does not exist. {BotPaths.TokenFile}");

            return Token;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    /// <summary>
    /// Sets the token into the token data.
    /// </summary>
    /// <param name="tokenstring">The token string.</param>
    public void SetToken(string tokenstring)
    {
        Token = tokenstring;

        var tokenJson = JsonSerializer.Serialize(this, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText(BotPaths.TokenFile, tokenJson);
    }
}