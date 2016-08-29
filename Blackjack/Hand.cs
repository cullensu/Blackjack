using System.Collections.Generic;

namespace Blackjack
{
    public class Hand
    {
        private const int FaceCardValue = 10;
        private const int HighAceValue = 11;
        private const int LowAceValue = 1;
        private const int WinningThreshold = 21;
        private readonly Randomer _randomer;
        public List<int> _cards = new List<int>();

        public Hand(Randomer random)
        {
            _randomer = random;
        }

        public void Deal()
        {
            Draw();
            Draw();
        }

        public int Draw()
        {
            var newCard = _randomer.Next(1, 14);
            _cards.Add(newCard);
            return newCard;
        }

        public int GetHandScore()
        {
            var total = 0;
            var aces = 0;
            foreach (var card in _cards)
            {
                if (IsAce(card))
                    aces++;
                total += GetCardValue(card);
            }

            while (total > WinningThreshold && aces > 0)
            {
                total -= HighAceValue - LowAceValue;
                aces--;
            }
            return total;
        }

        private int GetCardValue(int card)
        {
            if (IsFace(card))
                return FaceCardValue;
            if (IsAce(card))
                return HighAceValue;
            return card;
        }

        private static bool IsFace(int card)
        {
            return card > FaceCardValue;
        }

        private static bool IsAce(int card)
        {
            return card == LowAceValue;
        }

        public string GetCardName(int i)
        {
            return GetNameOf(_cards[i]);
        }

        public static string GetNameOf(int card)
        {
            if (IsAce(card))
                return "Ace";
            if (card == 11)
                return "Jack";
            if (card == 12)
                return "Queen";
            if (card == 13)
                return "King";
            return card.ToString();
        }
    }
}