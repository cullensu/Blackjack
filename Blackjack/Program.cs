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

                writer.WriteLine(
                    $"Your cards are {yourHand.GetCardName(0)} and {yourHand.GetCardName(1)}");

                var dealerHand = GetNewHand(random);
                writer.WriteLine(
                    $"The dealer is showing a {dealerHand.GetCardName(0)}. Do you (h)it or (s)tay?");

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
                                     $"The dealer flips their other card over. It's a {dealerHand.GetCardName(1)}.");
                }
                var newCard = 0;
                if (inputString == "h")
                {
                    newCard = Hand.DrawCard(random);
                    var n = "";
                    if (newCard == 1)
                    {
                        n = "n";
                    }
                    writer.WriteLine($"The dealer slides another card to you. It's a{n} {Hand.GetNameOf(newCard)}.");
                }

                var yourCards = yourHand.GetHandScore() + Hand.GetCardValue(newCard);
                var dealersCards = dealerHand.GetHandScore();

                if (dealersCards < 17)
                {
                    newCard = Hand.DrawCard(random);
                    var n = "";
                    if (newCard == 1)
                    {
                        n = "n";
                    }
                    writer.WriteLine($"The dealer adds another card to their hand. It's a{n} {Hand.GetNameOf(newCard)}.");
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

        private static Hand GetNewHand(Randomer random)
        {
            return new Hand(random);
        }
    }
}