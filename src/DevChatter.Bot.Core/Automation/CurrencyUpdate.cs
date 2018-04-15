using DevChatter.Bot.Core.Events;
using DevChatter.Bot.Core.Util;
using System;

namespace DevChatter.Bot.Core.Automation
{
    public class CurrencyUpdate : IIntervalAction
    {
        private readonly int _intervalInMinutes;
        private readonly ICurrencyGenerator _currencyGenerator;
        private readonly IClock _clock;
        private DateTime _nextRunTime;

        public CurrencyUpdate(int intervalInMinutes, ICurrencyGenerator currencyGenerator, IClock clock)
        {
            _intervalInMinutes = intervalInMinutes;
            _currencyGenerator = currencyGenerator;
            _clock = clock;
            SetNextRunTime();
        }

        public bool IsTimeToRun() => _clock.Now >= _nextRunTime;

        public void Invoke()
        {
            _currencyGenerator.UpdateCurrency();
            SetNextRunTime();
        }

        private void SetNextRunTime()
        {
            _nextRunTime = _clock.Now.AddMinutes(_intervalInMinutes);
        }
    }
}
