using System.Collections.Generic;

namespace Blackjack
{
    public class Hand
    {
        private const int WinningThreshold = 21;
        public List<int> _cards = new List<int>();

        public int AddCard(int card)
        {
            _cards.Add(card);
            return card;
        }

        public int GetHandScore()
        {
            var total = 0;
            var aces = 0;
            foreach (var card in _cards)
            {
                if (Card.IsAce(card))
                    aces++;
                total += GetCardValue(card);
            }

            while ((total > WinningThreshold) && (aces > 0))
            {
                total -= Card.HighAceValue - Card.LowAceValue;
                aces--;
            }
            return total;
        }

        private int GetCardValue(int card)
        {
            if (Card.IsFace(card))
                return Card.FaceCardValue;
            if (Card.IsAce(card))
                return Card.HighAceValue;
            return card;
        }

        public string GetCardName(int i)
        {
            return Card.GetNameOf(_cards[i]);
        }

        public bool Busted()
        {
            return GetHandScore() > WinningThreshold;
        }
    }
}