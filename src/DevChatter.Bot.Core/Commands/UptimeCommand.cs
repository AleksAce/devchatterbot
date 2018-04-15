using DevChatter.Bot.Core.Commands.Trackers;
using DevChatter.Bot.Core.Data;
using DevChatter.Bot.Core.Data.Model;
using DevChatter.Bot.Core.Events.Args;
using DevChatter.Bot.Core.Systems.Chat;
using DevChatter.Bot.Core.Systems.Streaming;
using System;

namespace DevChatter.Bot.Core.Commands
{
    public class UptimeCommand : BaseCommand
    {
        private readonly IStreamingPlatform _streamingPlatform;

        public UptimeCommand(IRepository repository, IStreamingPlatform streamingPlatform)
            : base(repository, UserRole.Everyone)
        {
            _streamingPlatform = streamingPlatform;
            HelpText = "Just type \"!uptime\" and it will tell you how long we've been streaming.";
        }

        public override CommandUsage Process(IChatClient chatClient, CommandReceivedEventArgs eventArgs)
        {
            TimeSpan? timeSpan = _streamingPlatform.GetUptimeAsync().Result;
            if (timeSpan.HasValue)
            {
                chatClient.SendMessage($"The stream has been going for {timeSpan:hh\\:mm\\:ss}");
            }
            else
            {
                chatClient.SendMessage("Something's Wrong. Are we live right now?");
            }

            return CommandUsage(eventArgs);
        }
    }
}
