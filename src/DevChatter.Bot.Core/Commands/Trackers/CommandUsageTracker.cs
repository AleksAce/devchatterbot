using DevChatter.Bot.Core.Events;
using DevChatter.Bot.Core.Events.Args;
using DevChatter.Bot.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevChatter.Bot.Core.Commands.Trackers
{
    public class CommandUsageTracker : ICommandUsageTracker
    {
        private readonly CommandHandlerSettings _settings;

        private readonly IList<CommandUsage> _userCommandUsages = new List<CommandUsage>();

        public CommandUsageTracker(CommandHandlerSettings settings)
        {
            _settings = settings;
        }

        public void PurgeExpiredUserCommandCooldowns(DateTimeOffset currentTime)
        {
            var expiredCooldowns = new List<CommandUsage>();

            foreach (var cooldownPair in _userCommandUsages)
            {
                var elapsedTime = currentTime - cooldownPair.TimeInvoked;
                if (elapsedTime.TotalSeconds >= _settings.GlobalCommandCooldown)
                {
                    expiredCooldowns.Add(cooldownPair);
                }
            }

            foreach (var user in expiredCooldowns)
            {
                _userCommandUsages.Remove(user);
            }
        }

        public List<CommandUsage> GetByUserDisplayName(string displayName)
        {
            return _userCommandUsages.Where(x => x.DisplayName.EqualsIns(displayName)).ToList();
        }

        public List<CommandUsage> GetByUserDisplayNameAndCommandOperation(string displayName, CommandReceivedEventArgs eventArgs)
        {
            throw new NotImplementedException();
            //return GetByUserDisplayName(displayName).Where(x => x).ToList();
        }

        public void RecordUsage(CommandUsage commandUsage)
        {
            _userCommandUsages.Add(commandUsage);
        }
    }
}
