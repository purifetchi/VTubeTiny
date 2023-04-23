namespace VTTiny.Plugin.Discord.Services;

public class NameProvider
{
    //Sadly you can't solve this with dependency injection because the DiscordAudioComponent
    //does not create new scopes
    //So we have to use a (classic) singleton and not a DP singleton 
    public List<string> NamesToWatch { get; set; } = new List<string>();
    /// <summary>
    /// Users in the VC
    /// </summary>
    public List<string> Users = new();
    public Dictionary<string, DateTime> UsersSpeaking = new();
    
    public void AddUser(string name)
    {
        if (!Users.Contains(name))
        {
            Users.Add(name);
        }
    }
    
    public void RemoveUser(string name)
    {
        if (Users.Contains(name))
        {
            Users.Remove(name);
        }
    }
    
    /// <summary>
    /// Is the user in the VC? So it can be drawn
    /// </summary>
    /// <param name="name">Who to Search</param>
    /// <returns>If the user is active in VC</returns>
    public bool IsUserInVc(string name)
    {
        return Users.Contains(name);
    }
    
    /// <summary>
    /// Is the User Speaking?
    /// </summary>
    /// <param name="name">Who to search</param>
    /// <returns>if the User speaking?</returns>
    public bool IsUserSpeaking(string name)
    {
        return UsersSpeaking.ContainsKey(name);
    }
    
    private static NameProvider _instance;
    public static NameProvider Instance => _instance ??= new NameProvider();
    private NameProvider() { }

}