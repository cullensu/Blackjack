using System;

namespace Blackjack
{
    public class Hand
    {
        public Tuple<int, int> _cards;

        public Hand(Randomer random)
        {
            var num1 = DrawCard(random);
            var num2 = DrawCard(random);
            _cards = new Tuple<int, int>(num1, num2);
        }

        public static int DrawCard(Randomer random)
        {
            var newCard = random.Next(1, 14);
            return newCard;
        }

        public int GetHandScore()
        {
            return GetCardValue(_cards.Item1) + GetCardValue(_cards.Item2);
        }

        public static int GetCardValue(int card)
        {
            if (card > 10)
                return 10;
            return card;
        }

        public string GetCardName(int i)
        {
            if (i == 0)
            {
                return GetNameOf(_cards.Item1);
            }
            return GetNameOf(_cards.Item2);
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