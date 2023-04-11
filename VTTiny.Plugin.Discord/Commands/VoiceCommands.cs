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


    [SlashCommand("join", "Makes the bot join the vc")]
    public async Task StartCommand(InteractionContext ctx)
    {
        await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
        
        try
        {
            DiscordChannel channel = ctx.Member.VoiceState?.Channel;
            if (channel == null)
            {
                ctx.EditResponseAsync(new DiscordWebhookBuilder()
                    .WithContent("You're not in a voice channel!"));
                return;
            }

            var connection = await channel.ConnectAsync();
            var vnext = ctx.Client.GetVoiceNext();
            var vnc = vnext.GetConnection(ctx.Guild);
            vnc.UserLeft += VncOnUserLeft;
            vnc.UserJoined += VncOnUserJoined;
            vnc.UserSpeaking += VncOnUserSpeaking;
            vnc.VoiceReceived += VncOnVoiceReceived;
            NameProvider.Instance._users = channel.Users.Select(x => x.Username).ToList();
            timer.Elapsed += TimerOnElapsed;
            timer.Start();

            foreach (var user in NameProvider.Instance._users)
            {
                Console.WriteLine(user);
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        await ctx.EditResponseAsync(new DiscordWebhookBuilder()
            .WithContent("Hello there"));

    }

    private void TimerOnElapsed(object? sender, ElapsedEventArgs e)
    {
        DateTime now = DateTime.Now;
        List<string> inactiveUsers = new List<string>();
        foreach (KeyValuePair<string, DateTime> entry in NameProvider.Instance._usersSpeaking)
        {
            if (now - entry.Value > TimeSpan.FromMilliseconds(100))
            {
                inactiveUsers.Add(entry.Key);
            }
        }
        foreach (string username in inactiveUsers)
        {
            NameProvider.Instance._usersSpeaking.Remove(username);
        }

        if (NameProvider.Instance._usersSpeaking.Count != 0)
        {
            Console.WriteLine($"Users speaking: {String.Join(", ", NameProvider.Instance._usersSpeaking.Keys)}");
            
        }
    }

    private async Task VncOnVoiceReceived(VoiceNextConnection sender, VoiceReceiveEventArgs args)
    {
        try
        {
            //add user to list if they are speaking
            NameProvider.Instance._usersSpeaking[args.User.Username] = DateTime.Now;
            NameProvider.Instance._usersSpeaking[args.User.Id.ToString()] = DateTime.Now;
        }
        catch (NullReferenceException ex) //Invalid error
        {
        }
    }

    private async Task VncOnUserSpeaking(VoiceNextConnection sender, UserSpeakingEventArgs args)
    {
    }

    private async Task VncOnUserJoined(VoiceNextConnection sender, VoiceUserJoinEventArgs args)
    {
        Console.WriteLine(args.User.Username);
        NameProvider.Instance._users.Add(args.User.Username);
        foreach (var user in NameProvider.Instance._users)
        {
            Console.WriteLine(user);
        }
    }

    private async Task VncOnUserLeft(VoiceNextConnection sender, VoiceUserLeaveEventArgs args)
    {
        NameProvider.Instance._users.Remove(args.User.Username);
        foreach (var user in _nameProvider._users)
        {
            Console.WriteLine(user);
        }
    }

    [SlashCommand("leave", "Makes the bot leave the vc")]
    public async Task LeaveCommand(InteractionContext ctx)
    {
        await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
        
        var vnext = ctx.Client.GetVoiceNext();

        var vnc = vnext.GetConnection(ctx.Guild);
        vnc.UserJoined -= VncOnUserJoined;
        vnc.UserLeft -= VncOnUserLeft;
        vnc.UserSpeaking -= VncOnUserSpeaking;
        vnc.VoiceReceived -= VncOnVoiceReceived;
        timer.Stop();
        timer.Elapsed -= TimerOnElapsed;
        timer.Dispose();
        vnc.Dispose();
        
        await ctx.EditResponseAsync(new DiscordWebhookBuilder()
            .WithContent("Ok i'll leave"));
    }
}