namespace Blackjack
{
    public class Card
    {
        public const int FaceCardValue = 10;
        public const int HighAceValue = 11;
        public const int LowAceValue = 1;

        public static string GetNameOf(int card)
        {
            if (IsAce(card))
                return "an Ace";
            if (card == 11)
                return "a Jack";
            if (card == 12)
                return "a Queen";
            if (card == 13)
                return "a King";
            return "a " + card;
        }

        public static bool IsFace(int card)
        {
            return card > FaceCardValue;
        }

        public static bool IsAce(int card)
        {
            return card == LowAceValue;
        }
    }
}