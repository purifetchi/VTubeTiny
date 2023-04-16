namespace VTTiny.Plugin.Discord.Services;

public class NameProvider
{
    public List<string> NamesToWatch { get; set; } = new List<string>();
    /// <summary>
    /// Users in the VC
    /// </summary>
    public List<string> _users = new();
    public Dictionary<string, DateTime> _usersSpeaking = new();
    
    public void AddUser(string name)
    {
        if (!_users.Contains(name))
        {
            _users.Add(name);
        }
    }
    public void RemoveUser(string name)
    {
        if (_users.Contains(name))
        {
            _users.Remove(name);
        }
    }
    
    /// <summary>
    /// Is the user in the VC? So it can be drawn
    /// </summary>
    /// <param name="name">Who to Search</param>
    /// <returns>If the user is active in VC</returns>
    public bool IsUserInVc(string name)
    {
        return _users.Contains(name);
    }
    /// <summary>
    /// Is the User Speaking?
    /// </summary>
    /// <param name="name">Who to search</param>
    /// <returns>if the User speaking?</returns>
    public bool IsUserSpeaking(string name)
    {
        return _usersSpeaking.ContainsKey(name);
    }
    //Make this a singleton
    private static NameProvider _instance;
    public static NameProvider Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new NameProvider();
            }
            return _instance;
        }
    }
    private NameProvider()
    {
    }

}