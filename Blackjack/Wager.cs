using System;

namespace Blackjack
{
    public class Wager
    {
        private const int MinBet = 10;

        public const int MaxBet = 100;
        private readonly Input _input;
        private readonly Writer _writer;

        public Wager(Writer writer, Input input)
        {
            _writer = writer;
            _input = input;
        }

        public int GetWager()
        {
            _writer.WriteLine($"Bet an amount between ${MinBet} and ${MaxBet}.");

            var wager = 25;
            int dummy;
            if (int.TryParse(_input.NextInput(), out dummy))
                wager = dummy;
            wager = Math.Max(MinBet, wager);
            wager = Math.Min(MaxBet, wager);

            _writer.WriteLine($"This hand costs you ${wager}.");
            return wager;
        }
    }
}