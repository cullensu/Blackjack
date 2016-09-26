namespace Blackjack
{
    public class Game
    {
        private readonly Deck _deck;
        private readonly Input _input;
        private readonly Writer _writer;

        public Game(Writer writer, Input input, Randomer random)
        {
            _writer = writer;
            _input = input;
            _deck = new Deck(random, _writer);
        }

        public void HandlePlayerDraw(Hand yourHand)
        {
            var inputString = "";

            do
            {
                inputString = "";
                while ((inputString != "h") && (inputString != "s"))
                {
                    _writer.WriteLine("Do you (h)it or (s)tay?");
                    inputString = _input.NextKey();
                    _writer.WriteLine();
                }

                if (inputString == "h")
                {
                    var newCard = yourHand.AddCard(_deck.DrawCard());
                    _writer.WriteLine(
                        $"The dealer slides another card to you. It's {Card.GetNameOf(newCard)}. Your total is: {yourHand.GetHandScore()}");
                }
            } while ((inputString != "s") && !yourHand.Busted());
        }

        public void PlayHand(Wager wager, Wallet wallet)
        {
            wager.Prompt();
            wager.ReadWager(_input.NextInput());

            var yourHand = _deck.DealPlayerHand();
            var dealerHand = _deck.DealDealerHand();

            HandlePlayerDraw(yourHand);
            _deck.HandleDealerDraw(dealerHand);

            var diffMoney = DecideWinner(yourHand, dealerHand, wager);
            wallet.Settle(diffMoney);
        }

        private int DecideWinner(Hand yourHand, Hand dealerHand, Wager wager)
        {
            var diffMoney = 0;
            var yourScore = yourHand.GetHandScore();
            var dealScore = dealerHand.GetHandScore();
            if ((yourScore < dealScore) || yourHand.Busted())
            {
                var loseAmount = wager.LoseAmount();
                diffMoney -= loseAmount;
                var loseMessage = yourHand.Busted() ? "You busted!" : "You lost!";
                _writer.WriteLine(
                    $"You had {yourScore} and dealer had {dealScore}. {loseMessage}.");
            }
            else if (yourScore == dealScore)
            {
                _writer.WriteLine(
                    $"You had {yourScore} and dealer had {dealScore}. It's a push!");
            }
            else
            {
                var winAmount = wager.WinAmount();
                diffMoney += winAmount;
                _writer.WriteLine(
                    $"You had {yourScore} and dealer had {dealScore}. You won!");
            }
            return diffMoney;
        }
    }
}