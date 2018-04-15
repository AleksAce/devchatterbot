using DevChatter.Bot.Core.Commands.Trackers;
using DevChatter.Bot.Core.Data;
using DevChatter.Bot.Core.Data.Model;
using DevChatter.Bot.Core.Data.Specifications;
using DevChatter.Bot.Core.Events.Args;
using DevChatter.Bot.Core.Extensions;
using DevChatter.Bot.Core.Systems.Chat;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevChatter.Bot.Core.Commands
{
    public abstract class BaseCommand : IBotCommand
    {
        protected readonly IRepository Repository;
        private readonly bool _isEnabled;
        public UserRole RoleRequired { get; }
        public string PrimaryCommandText => CommandWords.FirstOrDefault();
        public IList<string> CommandWords { get; private set; }
        public string HelpText { get; protected set; }
        public virtual string FullHelpText => HelpText;

        protected BaseCommand(IRepository repository, UserRole roleRequired)
            : this(repository, roleRequired, true)
        {
        }

        protected BaseCommand(IRepository repository, UserRole roleRequired, bool isEnabled)
        {
            Repository = repository;
            RoleRequired = roleRequired;
            _isEnabled = isEnabled;
            CommandWords = RefreshCommandWords();
        }

        private List<string> RefreshCommandWords()
        {
            return Repository
                       .List(CommandWordPolicy.ByType(GetType()))
                       ?.OrderByDescending(x => x.IsPrimary)
                       .Select(word => word.CommandWord)
                       .ToList() ?? new List<string>();
        }

        public void NotifyWordsModified() => CommandWords = RefreshCommandWords();

        public bool ShouldExecute(string commandText) => _isEnabled && CommandWords.Any(x => x.EqualsIns(commandText));

        public abstract CommandUsage Process(IChatClient chatClient, CommandReceivedEventArgs eventArgs);

        protected CommandUsage CommandUsage(CommandReceivedEventArgs eventArgs)
        {
            return new CommandUsage(eventArgs?.ChatUser.DisplayName, DateTimeOffset.UtcNow, this);
        }
    }
}
