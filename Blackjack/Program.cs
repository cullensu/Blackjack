using System;

namespace Blackjack
{
    public class Writer
    {
        public virtual void WriteLine(string value = "")
        {
            Console.WriteLine(value);
        }
    }

    public class Input
    {
        public virtual string NextInput()
        {
            return Console.ReadKey().KeyChar.ToString();
        }
    }

    public class Randomer
    {
        private readonly Random _random = new Random();

        public virtual int Next(int min, int max)
        {
            return _random.Next(min, max);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var program = new Program();
            program.Game(new Randomer(), new Writer(), new Input());
        }

        public void Game(Randomer random, Writer writer, Input input)
        {
            var money = 500;
            writer.WriteLine("Welcome to blackjack. You have $500. Each hand costs $25. You win at $1000.");

            while (money > 0)
            {
                var yourHand = GetNewHand(random);

                writer.WriteLine($"Your cards are {GetCardName(yourHand.Item1)} and {GetCardName(yourHand.Item2)}");

                var dealerHand = GetNewHand(random);
                writer.WriteLine($"The dealer is showing a {GetCardName(dealerHand.Item1)}. Do you (h)it or (s)tay?");

                var inputString = input.NextInput();
                writer.WriteLine();
                while (inputString != "h" && inputString != "s")
                {
                    writer.WriteLine("Do you (h)it or (s)tay?");
                    inputString = input.NextInput();
                    writer.WriteLine();
                }

                if (inputString == "s")
                {
                    writer.WriteLine(Environment.NewLine +
                                     $"The dealer flips their other card over. It's a {GetCardName(dealerHand.Item2)}.");
                }
                var newCard = 0;
                if (inputString == "h")
                {
                    newCard = DrawCard(random);
                    var n = "";
                    if (newCard == 1)
                    {
                        n = "n";
                    }
                    writer.WriteLine($"The dealer slides another card to you. It's a{n} {GetCardName(newCard)}.");
                }

                var yourCards = GetCardValue(yourHand.Item1) + GetCardValue(yourHand.Item2) + GetCardValue(newCard);
                var dealersCards = GetCardValue(dealerHand.Item1) + GetCardValue(dealerHand.Item2);

                if (dealersCards < 17)
                {
                    newCard = DrawCard(random);
                    var n = "";
                    if (newCard == 1)
                    {
                        n = "n";
                    }
                    writer.WriteLine($"The dealer adds another card to their hand. It's a{n} {GetCardName(newCard)}.");
                    dealersCards += newCard;
                }

                if (yourCards < dealersCards || yourCards > 21)
                {
                    money -= 25;
                    var loseMessage = yourCards > 21 ? "You busted!" : "You lost!";
                    writer.WriteLine(
                        $"You had {yourCards} and dealer had {dealersCards}. {loseMessage} You now have ${money} (-$25)");
                }
                else if (yourCards == dealersCards)
                {
                    writer.WriteLine(
                        $"You had {yourCards} and dealer had {dealersCards}. It's a push! You now have ${money} (+$0))");
                }
                else
                {
                    money += 25;
                    writer.WriteLine(
                        $"You had {yourCards} and dealer had {dealersCards}. You won! You now have ${money} (+$25).");
                }
                if (money >= 1000)
                {
                    writer.WriteLine("You win!");
                    input.NextInput();
                    return;
                }
            }

            writer.WriteLine("You lose.");
            input.NextInput();
        }

        private static int DrawCard(Randomer random)
        {
            var newCard = random.Next(1, 14);
            return newCard;
        }

        private static int GetCardValue(int card)
        {
            if (card > 10)
                return 10;
            return card;
        }

        private static string GetCardName(int card)
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

        private static Tuple<int, int> GetNewHand(Randomer random)
        {
            var num1 = DrawCard(random);
            var num2 = DrawCard(random);
            return new Tuple<int, int>(num1, num2);
        }
    }
}