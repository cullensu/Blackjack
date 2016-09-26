using System;

namespace Blackjack
{
    public class Wager
    {
        private const int MinBet = 10;
        private const int MaxBet = 100;
        private const float WinRatio = 1.5f;
        private readonly Writer _writer;
        private int _wager;

        public Wager(Writer writer)
        {
            _writer = writer;
        }

        public int LoseAmount()
        {
            return _wager;
        }

        public int WinAmount()
        {
            return (int) (_wager*WinRatio);
        }

        public void ReadWager(string amount)
        {
            _wager = 25;
            int dummy;
            if (int.TryParse(amount, out dummy))
                _wager = dummy;
            _wager = Math.Max(MinBet, _wager);
            _wager = Math.Min(MaxBet, _wager);

            _writer.WriteLine($"This hand costs you ${_wager}.");
        }

        public void Prompt()
        {
            _writer.WriteLine($"Bet an amount between ${MinBet} and ${MaxBet}.");
        }
    }
}