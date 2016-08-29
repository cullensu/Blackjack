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
            _cards.Add(DrawCard(_randomer));
            _cards.Add(DrawCard(_randomer));
        }

        public static int DrawCard(Randomer random)
        {
            var newCard = random.Next(1, 14);
            return newCard;
        }

        public int GetHandScore()
        {
            return GetCardValue(_cards[0]) + GetCardValue(_cards[1]);
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