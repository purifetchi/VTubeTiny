using System.Timers;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.VoiceNext;
using DSharpPlus.VoiceNext.EventArgs;
using VTTiny.Plugin.Discord.Services;

namespace VTTiny.Plugin.Discord.Commands;


[SlashCommandGroup("vc", "VC connectivity")]
public class VoiceChannelCommands : ApplicationCommandModule
{
    /// <summary>
    /// The timer.
    /// </summary>
    private readonly System.Timers.Timer _timer = new(500);

    /// <summary>
    /// Command to make the bot join the vc and start listening for users speaking
    /// </summary>
    /// <param name="ctx">Discord Command Context</param>
    [SlashCommand("join", "Makes the bot join the channel the client is in.")]
    public async Task StartCommand(InteractionContext ctx)
    {
        // This should never happen
        if (ctx == null) 
            throw new ArgumentNullException(nameof(ctx));

        await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
        
        try
        {
            var channel = ctx!.Member?.VoiceState?.Channel;
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
            _timer.Elapsed += TimerOnElapsed;
            _timer.Start();

            foreach (var user in NameProvider.Instance.Users)
                Console.WriteLine(user);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        await ctx.EditResponseAsync(new DiscordWebhookBuilder()
            .WithContent("Joined the voice channel."));

    }
    /// <summary>
    /// A method to remove users from the list of users speaking when they stopped speaking for Milliseconds
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TimerOnElapsed(object? sender, ElapsedEventArgs e)
    {
        var now = DateTime.Now;
        var inactiveUsers = NameProvider.Instance.UsersSpeaking
            .Where(entry => now - entry.Value > TimeSpan.FromMilliseconds(100))
            .Select(entry => entry.Key)
            .ToList();
        
        foreach (string username in inactiveUsers)
            NameProvider.Instance.UsersSpeaking.Remove(username);
        
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
        if (args.User is not null)
        {
            NameProvider.Instance.UsersSpeaking[args.User.Username] = DateTime.Now;
            NameProvider.Instance.UsersSpeaking[args.User.Id.ToString()] = DateTime.Now;
        }

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
        foreach (var user in NameProvider.Instance.Users)
            Console.WriteLine(user);

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

        _timer.Stop();
        _timer.Elapsed -= TimerOnElapsed;
        _timer.Dispose();

        vnc.Dispose();
        
        await ctx.EditResponseAsync(new DiscordWebhookBuilder()
            .WithContent("Goodbye."));
    }
}