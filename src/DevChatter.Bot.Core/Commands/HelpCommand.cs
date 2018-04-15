using System.Collections.Generic;
using System.Linq;
using DevChatter.Bot.Core.Commands.Trackers;
using DevChatter.Bot.Core.Data;
using DevChatter.Bot.Core.Data.Model;
using DevChatter.Bot.Core.Events.Args;
using DevChatter.Bot.Core.Systems.Chat;

namespace DevChatter.Bot.Core.Commands
{
    public class HelpCommand : BaseCommand
    {
        public HelpCommand(IRepository repository)
            : base(repository, UserRole.Everyone)
        {
            HelpText = "I think you figured this out already...";
        }

        public IEnumerable<IBotCommand> AllCommands { get; set; }

        public override CommandUsage Process(IChatClient chatClient, CommandReceivedEventArgs eventArgs)
        {
            if (eventArgs.Arguments.Count == 0)
            {
                ShowAvailableCommands(chatClient, eventArgs.ChatUser);
                return CommandUsage(eventArgs);
            }

            string argOne = eventArgs.Arguments?.ElementAtOrDefault(0);

            if (argOne == "?")
            {
                chatClient.SendMessage(
                    $"Use !help to see available commands. To request help for a specific command just type !help [commandname] example: !help hangman");
                return CommandUsage(eventArgs);
            }

            if (argOne == "↑, ↑, ↓, ↓, ←, →, ←, →, B, A, start, select")
            {
                chatClient.SendMessage("Please be sure to drink your ovaltine.");
                return CommandUsage(eventArgs);
            }

            bool isVerboseMode = false;
            if (argOne == "-v")
            {
                isVerboseMode = true;
                argOne = eventArgs.Arguments?.ElementAtOrDefault(1);
            }

            IBotCommand requestedCommand = AllCommands.SingleOrDefault(x => x.ShouldExecute(argOne));

            if (requestedCommand != null)
            {
                if (isVerboseMode)
                {
                    chatClient.SendMessage(requestedCommand.FullHelpText);
                }
                else
                {
                    chatClient.SendMessage(requestedCommand.HelpText);
                }
            }

            return CommandUsage(eventArgs);
        }

        private void ShowAvailableCommands(IChatClient chatClient, ChatUser chatUser)
        {
            var commands = AllCommands.Where(chatUser.CanUserRunCommand).Select(x => $"!{x.PrimaryCommandText}");
            string stringOfCommands = string.Join(", ", commands);

            string message =
                $"These are the commands that {chatUser.DisplayName} is allowed to run: ({stringOfCommands})";
            chatClient.SendMessage(message);
        }
    }
}
