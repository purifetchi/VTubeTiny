using System.Timers;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.SlashCommands;
using DSharpPlus.VoiceNext;
using DSharpPlus.VoiceNext.EventArgs;
using VTTiny.Plugin.Discord.Services;

namespace VTTiny.Plugin.Discord.Commands;


[SlashCommandGroup("vc", "VC connectivity")]
public class VoiceChannelCommands : ApplicationCommandModule
{
    /// <summary>
    /// Users in the VC
    /// </summary>
    System.Timers.Timer timer = new System.Timers.Timer(500);
    private readonly NameProvider _nameProvider;
    public VoiceChannelCommands()
    {
        _nameProvider = NameProvider.Instance;
    }

    /// <summary>
    /// Command to make the bot join the vc and start listening for users speaking
    /// </summary>
    /// <param name="ctx">Discord Command Context</param>
    [SlashCommand("join", "Makes the bot join the vc")]
    public async Task StartCommand(InteractionContext ctx)
    {
        if (ctx == null) throw new ArgumentNullException(nameof(ctx)); //This could never happen
        await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
        
        try
        {
            var channel = ctx?.Member?.VoiceState?.Channel;
            if (channel == null)
            {
                await ctx.EditResponseAsync(new DiscordWebhookBuilder()
                    .WithContent("You're not in a voice channel!"));
                return;
            }
            
            //This is needed to make the bot join the vc, we do not use it later
            var _ = await channel.ConnectAsync(); 
            var vnext = ctx!.Client.GetVoiceNext();
            var vnc = vnext.GetConnection(ctx.Guild);
            vnc.UserLeft += VncOnUserLeft;
            vnc.UserJoined += VncOnUserJoined;
            vnc.VoiceReceived += VncOnVoiceReceived;
            NameProvider.Instance.Users = channel.Users.Select(x => x.Username).ToList();
            timer.Elapsed += TimerOnElapsed;
            timer.Start();

            foreach (var user in NameProvider.Instance.Users)
            {
                Console.WriteLine(user);
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        await ctx.EditResponseAsync(new DiscordWebhookBuilder()
            .WithContent("I've joined the voice channel!"));

    }
    /// <summary>
    /// A method to remove users from the list of users speaking when they stopped speaking for Milliseconds
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TimerOnElapsed(object? sender, ElapsedEventArgs e)
    {
        var now = DateTime.Now;
        var inactiveUsers = 
            (from entry in NameProvider.Instance.UsersSpeaking 
                where now - entry.Value > TimeSpan.FromMilliseconds(100) 
                select entry.Key)
            .ToList();
        
        foreach (string username in inactiveUsers)
        {
            NameProvider.Instance.UsersSpeaking.Remove(username);
        }
        
        #if DEBUG
        if (NameProvider.Instance.UsersSpeaking.Count != 0)
        {
            Console.WriteLine($"Users speaking: {String.Join(", ", NameProvider.Instance.UsersSpeaking.Keys)}");
        }
        #endif
    }
    /// <summary>
    /// Adds users to the list of users speaking when they start speaking
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    private Task VncOnVoiceReceived(VoiceNextConnection sender, VoiceReceiveEventArgs args)
    {
        try
        {
            //add user to list if they are speaking
            NameProvider.Instance.UsersSpeaking[args.User.Username] = DateTime.Now;
            NameProvider.Instance.UsersSpeaking[args.User.Id.ToString()] = DateTime.Now;
        }
        catch (NullReferenceException ex){} //Invalid error

        return Task.CompletedTask;
    }
    /// <summary>
    /// On user join to correct list of users
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    private Task VncOnUserJoined(VoiceNextConnection sender, VoiceUserJoinEventArgs args)
    {
        Console.WriteLine(args.User.Username);
        NameProvider.Instance.Users.Add(args.User.Username);
        foreach (var user in NameProvider.Instance.Users)
        {
            Console.WriteLine(user);
        }

        return Task.CompletedTask;
    }
    /// <summary>
    /// Removes users from the list of users speaking when they leave the vc
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    private Task VncOnUserLeft(VoiceNextConnection sender, VoiceUserLeaveEventArgs args)
    {
        NameProvider.Instance.Users.Remove(args.User.Username);
        foreach (var user in _nameProvider.Users)
        {
            Console.WriteLine(user);
        }

        return Task.CompletedTask;
    }
    /// <summary>
    /// Discord command to make the bot leave the vc
    /// </summary>
    /// <param name="ctx">Discord Command Context</param>
    [SlashCommand("leave", "Makes the bot leave the vc")]
    public async Task LeaveCommand(InteractionContext ctx)
    {
        await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
        var vnext = ctx.Client.GetVoiceNext();
        var vnc = vnext.GetConnection(ctx.Guild);
        vnc.UserJoined -= VncOnUserJoined;
        vnc.UserLeft -= VncOnUserLeft;
        vnc.VoiceReceived -= VncOnVoiceReceived;
        timer.Stop();
        timer.Elapsed -= TimerOnElapsed;
        timer.Dispose();
        vnc.Dispose();
        
        await ctx.EditResponseAsync(new DiscordWebhookBuilder()
            .WithContent("I've left the voice channel!"));
    }
}