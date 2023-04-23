using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text.Json;

namespace VTTiny.Plugin.Discord.Services;

public class Token
{
    public string? token { get; set; }
    public string GetToken()
    {
        BotPaths.CreateFoldersAndFiles();
        try
        {
            var tk = JsonSerializer.Deserialize<Token>(File.ReadAllText(BotPaths.Token));
            token = tk?.token;
            if (string.IsNullOrEmpty(token))
                throw new InvalidDataException($"you forgot the Token! {BotPaths.Token}");
            return token;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
    public void SetToken(string tokenstring)
    {
        this.token = tokenstring;
        var tokenJson = JsonSerializer.Serialize(this, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(BotPaths.Token, tokenJson);
        
    }
    
}
public static class BotPaths
{
    public static string Appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    public static string VTtinyDiscordBot = Path.Combine(Appdata, "VTtinyDiscordBot");
    public static string Token = Path.Combine(VTtinyDiscordBot, "token.json");
    
    public static void CreateFoldersAndFiles()
    {
        if (!Directory.Exists(VTtinyDiscordBot))
        {
            Directory.CreateDirectory(VTtinyDiscordBot);
        }

        if (File.Exists(Token)) return;
        Token token = new();
        token.SetToken("Token");
    }
}