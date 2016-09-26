using System;

namespace Blackjack
{
    public class Deck
    {
        private readonly Randomer _random;
        private readonly Writer _writer;

        public Deck(Randomer random, Writer writer)
        {
            _random = random;
            _writer = writer;
        }

        public Hand DealPlayerHand()
        {
            var yourHand = GetNewHand();
            _writer.WriteLine(
                $"Your cards are {yourHand.GetCardName(0)} and {yourHand.GetCardName(1)}");
            return yourHand;
        }

        private Hand GetNewHand()
        {
            var hand = new Hand(_random);
            hand.Deal();
            return hand;
        }

        public Hand DealDealerHand()
        {
            var dealerHand = GetNewHand();
            _writer.WriteLine(
                $"The dealer is showing {dealerHand.GetCardName(0)}.");
            return dealerHand;
        }

        public void HandleDealerDraw(Hand dealerHand)
        {
            _writer.WriteLine(Environment.NewLine +
                              $"The dealer flips their other card over. It's {dealerHand.GetCardName(1)}.");
            if (dealerHand.GetHandScore() < 17)
            {
                var newCard = dealerHand.Draw();
                _writer.WriteLine($"The dealer adds another card to their hand. It's {Card.GetNameOf(newCard)}.");
            }
        }
    }
}