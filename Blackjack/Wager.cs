namespace Blackjack
{
    public class Wager
    {
        private readonly Writer _writer;

        public Wager(Writer writer)
        {
            _writer = writer;
        }

        public int GetWager()
        {
            var wager = 25;
            _writer.WriteLine($"This hand costs you ${wager}.");
            return wager;
        }
    }
}