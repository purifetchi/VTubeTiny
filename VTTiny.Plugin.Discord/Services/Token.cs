using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text.Json;

namespace VTTiny.Plugin.Discord.Services;

public class Token
{
    public string? token
    {
        get; set;
    }

    public string getToken()
    {
        BotPaths.CreateFoldersAndFiles();
        Console.WriteLine("Getting Token");
        try
        {
            Console.WriteLine("Deserializing Token");
            //Read the token from the json file
            
            var tk = JsonSerializer.Deserialize<Token>(File.ReadAllText(BotPaths.token));
            Console.WriteLine("Deserialized Token");
            token = tk.token;
            Console.WriteLine("Got Token from Json");
            //if this is windows
            if (string.IsNullOrEmpty(token))
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    //Makes a popup window that says "You are an Idiot, you forgot the Token! {BotPaths.token}"
                    string msg =
                        """powershell -WindowStyle hidden -Command "& {[System.Reflection.Assembly]::LoadWithPartialName('System.Windows.Forms'); [System.Windows.Forms.MessageBox]::Show('You forgot to put a token in, The folder is in %appdata%/VTtinyDiscordBot/Token.json','WARNING')} """;
                    var process = System.Diagnostics.Process.Start("cmd.exe", msg);
                    process.WaitForExit();
                    Environment.Exit(0);
                }
            }
            if (string.IsNullOrEmpty(token))
                throw new InvalidDataException($"you forgot the Token! {BotPaths.token}");
            return token;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
    public void setToken(string token)
    {
        this.token = token;
        var tokenJson = JsonSerializer.Serialize(this, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(BotPaths.token, tokenJson);
        
    }
    
}
public static class BotPaths
{
    public static string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    public static string VTtinyDiscordBot = Path.Combine(appdata, "VTtinyDiscordBot");
    public static string token = Path.Combine(VTtinyDiscordBot, "token.json");
    
    public static void CreateFoldersAndFiles()
    {
        if (!Directory.Exists(VTtinyDiscordBot))
        {
            Directory.CreateDirectory(VTtinyDiscordBot);
        }
        if (!File.Exists(token))
        {
            Token ntoken = new();
            ntoken.setToken("Token");
        }
        
    }
}

internal class Json
{

    /// <summary>
    ///     Reads all text from file
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public static string ReadJson(string filename)
    {
        var path = Path.Combine(filename);
        try
        {
            if (File.Exists(path))
            {
                var file = File.ReadAllText(path);
                return file;
            }
            else
            {
                throw new FileNotFoundException(filename);
            }
        }
        catch (FileNotFoundException ex)
        {
            throw;
        }
    }

    public static T? deserializer<T>(string name)
    {
        var json = ReadJson(name);
        if (json is not (null or ""))
            return JsonSerializer.Deserialize<T>(json);
        return default;
    }
}