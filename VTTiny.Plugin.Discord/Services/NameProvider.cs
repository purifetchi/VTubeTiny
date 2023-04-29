namespace VTTiny.Plugin.Discord.Services;

/// <summary>
/// A provider for the currently speaking users.
/// </summary>
public class NameProvider
{
    //Sadly you can't solve this with dependency injection because the DiscordAudioComponent
    //does not create new scopes
    //So we have to use a (classic) singleton and not a DP singleton 
    public List<string> NamesToWatch { get; set; } = new();

    /// <summary>
    /// Users in the VC
    /// </summary>
    public List<string> Users { get; internal set; } = new();

    /// <summary>
    /// The currently speaking users.
    /// </summary>
    public Dictionary<string, DateTime> UsersSpeaking { get; } = new();

    /// <summary>
    /// The singleton instance of NameProvider.
    /// </summary>
    public static NameProvider Instance => _instance ??= new NameProvider();

    private static NameProvider? _instance;

    private NameProvider() 
    {
    }

    /// <summary>
    /// Add a user.
    /// </summary>
    /// <param name="name">The name of the user.</param>
    public void AddUser(string name)
    {
        if (!Users.Contains(name))
        {
            Users.Add(name);
        }
    }
    
    /// <summary>
    /// Remove a user.
    /// </summary>
    /// <param name="name">The name of the user.</param>
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
}