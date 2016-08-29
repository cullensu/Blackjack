using System.Collections.Generic;

namespace Blackjack
{
    public class Hand
    {
        private readonly Randomer _randomer;
        public List<int> _cards = new List<int>();

        public Hand(Randomer random)
        {
            _randomer = random;
        }

        public void Deal()
        {
            DrawCard();
            DrawCard();
        }

        public int DrawCard()
        {
            var newCard = _randomer.Next(1, 14);
            _cards.Add(newCard);
            return newCard;
        }

        public int GetHandScore()
        {
            var total = 0;
            foreach (var card in _cards)
            {
                total += GetCardValue(card);
            }
            return total;
        }

        public static int GetCardValue(int card)
        {
            if (card > 10)
                return 10;
            if (card == 1)
                return 11;
            return card;
        }

        public string GetCardName(int i)
        {
            return GetNameOf(_cards[i]);
        }

        public static string GetNameOf(int card)
        {
            if (card == 1)
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